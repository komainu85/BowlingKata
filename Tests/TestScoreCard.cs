using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScoreCardRebuild;

namespace Tests
{
    public class TestScoreCard
    {
        public ScoreCard ScoreCard { get; set; }

        public TestScoreCard(string scoreCard)
        {
            ScoreCard = new ScoreCard(scoreCard);

        }

        public int Score()
        {
            return ScoreCard.GameScore;
        }

        public void ScoreEquals(int expected)
        {
             NUnit.Framework.Assert.AreEqual(Score(),expected);
        }
    }
}
