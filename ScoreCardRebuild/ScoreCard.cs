using System;
using System.Collections.Generic;
using System.Linq;
using ScoreCardRebuild.Entities;

namespace ScoreCardRebuild
{
    public class ScoreCard
    {
        public int MaxFrameScore = 10;

        public List<Frame> Frames { get; set; } = new List<Frame>();

        public ScoreCard(string scoreCard)
        {
            string[] framesString = scoreCard.Split('|');

            for (int i = 0; i < framesString.Length; i++)
            {
                List<int> balls = new List<int>();

                string[] ballsString = framesString[i].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                int previousBallScore = 0;

                foreach (var ball in ballsString)
                {
                    var score = ParseBallInput(ball, previousBallScore);

                    balls.Add(score);
                    previousBallScore = score;
                }

                Frame frame = new Frame(balls, i + 1);
                Frames.Add(frame);
            }
        }

        public int GameScore()
        {
            Score gameScore = new Score();

            for (int i = 0; i < Frames.Count; i++)
            {
                if (Frames[i].IsStrike())
                {
                    gameScore.AddBallScore(10);
                    gameScore.AddNextTwoBallScores(SecondBallScore(i), ThirdBallScore(i));
                }
                else if (Frames[i].IsSpare())
                {
                    gameScore.AddBallScore(10);
                    gameScore.AddNextBallScore(SecondBallScore(i));
                }
                else
                {
                    gameScore.AddBallScore(Frames[i].GetBallScore(0));
                    gameScore.AddBallScore(Frames[i].GetBallScore(1));
                }
            }
            return gameScore.GetFinalScore();
        }

        public int SecondBallScore(int frameIndex)
        {
            int ballScore = 0;

            if (Frames[frameIndex].IsFinalFrame())
            {
                ballScore = Frames[frameIndex].GetBallScore(1);
            }
            else
            {
                ballScore = Frames[frameIndex + 1].GetBallScore(0);
            }

            return ballScore;

        }

        public int ThirdBallScore(int frameIndex)
        {
            int ballScore = 0;

            if (Frames[frameIndex].IsFinalFrame())
            {
                ballScore = Frames[frameIndex].GetBallScore(2);
            }
            else
            {
                if (Frames[frameIndex + 1].IsStrike())
                {
                    ballScore = 10;
                }
                else
                {
                    ballScore = Frames[frameIndex+1].GetBallScore(1);
                }

            }

            return ballScore;
        }



        private int ParseBallInput(string ball, int previousBallScore)
        {
            int score;

            if (ball == "X")
            {
                score = MaxFrameScore;
            }
            else if (ball == "/")
            {
                score = MaxFrameScore - previousBallScore;
            }
            else
            {
                int.TryParse(ball, out score);
            }
            return score;
        }
    }
}

