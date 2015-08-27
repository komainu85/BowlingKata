using System.Collections.Generic;
using System.Linq;
using System.Runtime;

namespace ScoreCardRebuild.Entities
{
    public class Frame
    {
        private List<int> _ballScores;
        private int _frameNumber;

        public Frame(List<int> ballScores, int frameNumber)
        {
            _ballScores = ballScores;
            _frameNumber = frameNumber;
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
            return _frameNumber == 10;
        }

        public int GetBallScore(int ballIndex)
        {
            return _ballScores[ballIndex];
        }
    }
}