using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ScoreCardRebuild;

namespace Tests
{
    [TestFixture]
    public class ScoreCardTests
    {
        [Test]
        public void PerfectGame()
        {
            WhenScoreCard("X|X|X|X|X|X|X|X|X|X,X,X").ScoreEquals(300);
        }

        [Test]
        public void CleanSheet()
        {
            WhenScoreCard("5,/|5,/|5,/|5,/|5,/|5,/|5,/|5,/|5,/|5,/,5").ScoreEquals(150);
        }

        [Test]
        public void Dutch200()
        {
            WhenScoreCard("3,/|X|3,/|X|3,/|X|3,/|X|3,/|X,3,/")
                .ScoreEquals(200);
        }

        [Test]
        public void AverageGame()
        {
            WhenScoreCard("2,3|7,2|X|4,2|7,2|X|3,/|3,2|X|X,8,2")
                .ScoreEquals(133);
        }

        [Test]
        public void NoStrikes()
        {
            WhenScoreCard("1,2|8,1|3,2|3,2|3,4|9,0|3,4|1,3|4,3|4,3")
                .ScoreEquals(63);
        }

        private static TestScoreCard WhenScoreCard(string scoreCard)
        {
            return new TestScoreCard(scoreCard);
        }
    }
}
