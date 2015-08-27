using System;
using System.Collections.Generic;
using System.Linq;
using ScoreCardRebuild.Entities;

namespace ScoreCardRebuild
{
    public class ScoreCard
    {
        public int MaxScore = 10;

        public List<Frame> Frames { get; set; } = new List<Frame>();

        private int _gameScore;

        public int GameScore
        {
            get
            {
                if (_gameScore == 0)
                {
                    foreach (var frame in Frames)
                    {
                        if (frame.Strike)
                        {
                            var frameTotal = CalculateStrikeFrameTotal(frame);
                            _gameScore += frameTotal;
                        }
                        else if (frame.Spare)
                        {
                            var frameTotal = CalculateSpareFrameTotal(frame);
                            _gameScore += frameTotal;
                        }
                        else
                        {
                            _gameScore += frame.Balls.First().Pins + frame.Balls.Last().Pins;
                        }
                    }
                }

                return _gameScore;
            }

        }

        public ScoreCard(string scoreCard)
        {
            string[] framesString = scoreCard.Split('|');

            FrameManager frameManager = new FrameManager();

            for (int i = 0; i < framesString.Length; i++)
            {
                List<Ball> balls = new List<Ball>();

                string[] ballsString = framesString[i].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                int previousBallScore = 0;

                foreach (var ball in ballsString)
                {
                    var score = ParseBallInput(ball, previousBallScore);

                    balls.Add(new Ball(score));
                    previousBallScore = score;
                }

                Frame frame = frameManager.CreateFrame(i + 1, balls);
                Frames.Add(frame);
            }
        }

        private int ParseBallInput(string ball, int previousBallScore)
        {
            int score;

            if (ball == "X")
            {
                score = MaxScore;
            }
            else if (ball == "/")
            {
                score = MaxScore - previousBallScore;
            }
            else
            {
                int.TryParse(ball, out score);
            }
            return score;
        }

        private int CalculateSpareFrameTotal(Frame frame)
        {
            int firstBall = 0;
            var frameTotal = 0;

            if (frame.FinalFrame)
            {
                firstBall = frame.Balls.Last().Pins;
            }
            else
            {
                Frame nextFrame = GetNextFrame(frame);

                if (nextFrame != null)
                {
                    firstBall = GetBallScore(nextFrame, 0);
                }
            }

            frameTotal = MaxScore + firstBall;
            return frameTotal;
        }

        private int CalculateStrikeFrameTotal(Frame frame)
        {
            int frameTotal = 0;

            int firstBall = 0;
            int secondBall = 0;

            if (frame.FinalFrame)
            {
                firstBall = GetBallScore(frame, 1);
                secondBall = GetBallScore(frame, 2);
            }
            else
            {
                Frame nextFrame = GetNextFrame(frame);

                if (nextFrame != null)
                {
                    firstBall = GetBallScore(nextFrame, 0);

                    if (nextFrame.Strike && nextFrame.FinalFrame)
                    {
                        secondBall = GetBallScore(nextFrame, 0);
                    }
                    else if (nextFrame.Strike)
                    {
                        Frame nextNextFrame = GetNextFrame(nextFrame);
                        if (nextNextFrame != null)
                        {
                            secondBall = GetBallScore(nextNextFrame, 0);
                        }
                    }
                    else
                    {
                        secondBall = GetBallScore(nextFrame, 1);
                    }
                }
            }

            frameTotal = MaxScore + firstBall + secondBall;
            return frameTotal;
        }

        private Frame GetNextFrame(Frame frame)
        {
            return Frames.FirstOrDefault(x => x.FrameNumber == frame.FrameNumber + 1);
        }

        private static int GetBallScore(Frame frame, int ballIndex)
        {
            return frame.Balls[ballIndex].Pins;
        }
    }
}

