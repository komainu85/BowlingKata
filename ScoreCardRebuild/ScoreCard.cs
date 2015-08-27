using System;
using System.Collections.Generic;
using System.Linq;
using ScoreCardRebuild.Entities;

namespace ScoreCardRebuild
{
    public class ScoreCard
    {
        public int MaxScore = 10;
        readonly FrameManager _frameManager = new FrameManager();

        public List<Frame> Frames { get; set; } = new List<Frame>();

        public ScoreCard(string scoreCard)
        {
            string[] framesString = scoreCard.Split('|');

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

                Frame frame = _frameManager.CreateFrame(i + 1, balls);
                Frames.Add(frame);
            }
        }

        public int GameScore()
        {
            int frameScore = 0;

            for (int i = 0; i < Frames.Count; i++)
            {
                if (Frames[i].Strike)
                {
                    List<Ball> balls = GetBalls(Frames, i, 2, 1);
                    frameScore += 10 + GetBallsScore(balls);
                }
                else if (Frames[i].Spare)
                {
                    List<Ball> balls = GetBalls(Frames, i, 1, 2);
                    frameScore += 10 + GetBallsScore(balls);
                }
                else
                {
                    frameScore += Frames[i].Balls[0].Pins + Frames[i].Balls[1].Pins;
                }
            }

            return frameScore;
        }

        public List<Ball> GetBalls(List<Frame> frames, int frameIndex, int ballCount, int offset)
        {
            List<Ball> balls = new List<Ball>();

            for (int i = 0; i < ballCount; i++)
            {
                if (frames[frameIndex].Balls.Count >= i + 1 + offset)
                {
                    Ball ball = frames[frameIndex].Balls[i + offset];
                    balls.Add(ball);
                }
                else
                {
                    frameIndex += 1;

                    Ball ball = frames[frameIndex].Balls[0];
                    balls.Add(ball);
                    offset = 0;
                }
            }

            return balls;
        }

        public int GetBallsScore(List<Ball> balls)
        {
            return balls.Sum(x => x.Pins);
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
    }
}

