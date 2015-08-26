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
            WhenScoreCard("3,/|3,/|3,/|3,/|3,/|3,/|3,/|3,/|3,/|3,/,X").ScoreEquals(137);
        }

        [Test]
        public void Dutch200()
        {
            WhenScoreCard("3,/|X|3,/|X|3,/|X|3,/|X|3,/|X,3,/")
                .ScoreEquals(200);
        }

        private static TestScoreCard WhenScoreCard(string scoreCard)
        {
            return new TestScoreCard(scoreCard);
        }
    }
}
