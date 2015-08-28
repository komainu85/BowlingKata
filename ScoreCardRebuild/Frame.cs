using System.Collections.Generic;
using System.Linq;
using System.Runtime;

namespace ScoreCardRebuild.Entities
{
    public class Frame
    {
        private IReadOnlyList<int> _ballScores;
        private bool _finalFrame;

        public Frame(IReadOnlyList<int> ballScores, bool finalFrame)
        {
            _ballScores = ballScores;
            _finalFrame = finalFrame;
        }

        public bool IsStrike()
        {
            return _ballScores.First() == 10;
        }

        public bool IsSpare()
        {
            bool spare = false;

            if (_ballScores.Count > 1)
            {
                if ((_ballScores[0] + _ballScores[1]) == 10)
                {
                    spare = true;
                }
            }

            return spare;
        }

        public bool IsFinalFrame()
        {
            return _finalFrame;
        }

        public int GetBallScore(int ballIndex)
        {
            int ballScore = 0;

            if (BallCount() >= ballIndex + 1)
            {

                ballScore = _ballScores[ballIndex];
            }

            return ballScore;
        }

        public int BallCount()
        {
            return _ballScores.Count();
        }

    }
}