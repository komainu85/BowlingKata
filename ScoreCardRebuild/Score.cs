using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreCardRebuild.Entities
{
    public class Score
    {
        private int _finalScore;

        public void AddNextBallScore(int ballOne)
        {
            _finalScore += ballOne;
        }

        public void AddNextTwoBallScores(int ballOne, int ballTwo)
        {
            _finalScore += (ballOne + ballTwo);
        }

        public void AddBallScore(int score)
        {
            _finalScore += score;
        }

        public int GetFinalScore()
        {
            return _finalScore;
        }

    }
}
