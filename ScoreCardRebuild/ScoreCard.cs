using System;
using System.Collections.Generic;
using System.Linq;
using ScoreCardRebuild.Entities;

namespace ScoreCardRebuild
{
    public class ScoreCard
    {
        public int MaxScore = 10;

        private int _score;

        public int Score
        {
            get
            {
                if (_score == 0)
                {
                    foreach (var frame in Frames)
                    {
                        if (frame.Strike)
                        {
                            var frameTotal = CalculateStrikeFrameTotal(frame);
                            _score += frameTotal;
                        }
                        else if (frame.Spare)
                        {
                            var frameTotal = CalculateSpareFrameTotal(frame);
                            _score += frameTotal;
                        }
                        else
                        {
                            _score += frame.Balls.First().Pins + frame.Balls.Last().Pins;
                        }
                    }
                }

                return _score;
            }

        }

        public List<Frame> Frames { get; set; } = new List<Frame>();

        public ScoreCard(string scoreCard)
        {
            string[] framesSring = scoreCard.Split('|');

            for (int i = 0; i < framesSring.Length; i++)
            {
                Frame frame = new Frame(i + 1);

                string[] balls = framesSring[i].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                int previousBallScore = 0;

                foreach (var ball in balls)
                {
                    var score = GetBallScore(ball, previousBallScore);

                    frame.Balls.Add(new Ball(score));
                    previousBallScore = score;
                }

                Frames.Add(frame);
            }
        }

        private int GetBallScore(string ball, int previousBall)
        {
            int score;

            if (ball == "X")
            {
                score = 10;
            }
            else if (ball == "/")
            {
                score = 10 - previousBall;
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
                    firstBall = nextFrame.Balls[0].Pins;
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
                firstBall = frame.Balls[1].Pins;
                secondBall = frame.Balls[2].Pins;
            }
            else
            {
                Frame nextFrame = GetNextFrame(frame);

                if (nextFrame != null)
                {
                    firstBall = nextFrame.Balls.First().Pins;

                    if (nextFrame.Strike && nextFrame.FinalFrame)
                    {
                        secondBall = nextFrame.Balls[0].Pins;
                    }
                    else if (nextFrame.Strike)
                    {
                        Frame nextNextFrame = GetNextFrame(nextFrame);
                        if (nextNextFrame != null)
                        {
                            secondBall = nextNextFrame.Balls[0].Pins;
                        }
                    }
                    else
                    {
                        secondBall = nextFrame.Balls[1].Pins;
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
    }
}

