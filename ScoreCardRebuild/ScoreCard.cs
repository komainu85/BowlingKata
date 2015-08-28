using System;
using System.Collections.Generic;
using System.Linq;
using ScoreCardRebuild.Entities;

namespace ScoreCardRebuild
{
    public class ScoreCard
    {
        public int MaxFrameScore = 10;

        private List<Frame> _frames = new List<Frame>();

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

                Frame frame = new Frame(balls, i + 1 == framesString.Length);
                _frames.Add(frame);
            }
        }

        public int GameScore()
        {
            Score gameScore = new Score();

            for (int i = 0; i < _frames.Count; i++)
            {
                if (_frames[i].IsStrike())
                {
                    gameScore.AddBallScore(10);
                    gameScore.AddNextTwoBallScores(SecondBallScore(i), ThirdBallScore(i));
                }
                else if (_frames[i].IsSpare())
                {
                    gameScore.AddBallScore(10);
                    gameScore.AddNextBallScore(SecondBallScore(i));
                }
                else
                {
                    gameScore.AddBallScore(_frames[i].GetBallScore(0));

                    if (_frames[i].BallCount() > 1)
                    {
                        gameScore.AddBallScore(_frames[i].GetBallScore(1));
                    }
                }
            }
            return gameScore.GetFinalScore();
        }

        public int SecondBallScore(int frameIndex)
        {
            int ballScore = 0;

            if (_frames[frameIndex].IsFinalFrame() && _frames[frameIndex].IsSpare())
            {
                ballScore = _frames[frameIndex].GetBallScore(2);
            }
            else if (_frames[frameIndex].IsFinalFrame())
            {
                ballScore = _frames[frameIndex].GetBallScore(1);
            }
            else if (IsThereNextAFrame(frameIndex))
            {
                ballScore = _frames[frameIndex + 1].GetBallScore(0);
            }

            return ballScore;
        }

        public int ThirdBallScore(int frameIndex)
        {
            int ballScore = 0;

            if (_frames[frameIndex].IsFinalFrame())
            {
                ballScore = _frames[frameIndex].GetBallScore(2);
            }
            else
            {
                if (IsThereNextAFrame(frameIndex))
                {
                    if (_frames[frameIndex + 1].IsFinalFrame())
                    {
                        ballScore = _frames[frameIndex + 1].GetBallScore(2);
                    }
                   else if (_frames[frameIndex + 1].IsStrike())
                    {
                        ballScore = _frames[frameIndex + 2].GetBallScore(0);
                    }
                    else if (_frames[frameIndex + 1].BallCount() > 1)
                    {
                        ballScore = _frames[frameIndex + 1].GetBallScore(1);
                    }
                }

            }

            return ballScore;
        }

        private bool IsThereNextAFrame(int frameIndex)
        {
            return _frames.Count() >= frameIndex + 2;
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

