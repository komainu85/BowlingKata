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
        public void OneRoll()
        {
            WhenScoreCard("1").ScoreEquals(1);
        }

        [Test]
        public void TwoRolls()
        {
            WhenScoreCard("1,1").ScoreEquals(2);
        }

        [Test]
        public void Strike()
        {
            WhenScoreCard("X").ScoreEquals(10);
        }

        [Test]
        public void TwoStrikes()
        {
            WhenScoreCard("X|X").ScoreEquals(30);
        }

        [Test]
        public void TwoSpares()
        {
            WhenScoreCard("1,/|1,/").ScoreEquals(21);
        }

        [Test]
        public void LastFrameSpareStrike()
        {
            WhenScoreCard("1,2|1,2|1,2|1,2|1,2|1,2|1,2|1,2|1,2|2,8,X").ScoreEquals(47);
        }

        [Test]
        public void LastFrameStrike()
        {
            WhenScoreCard("1,2|1,2|1,2|1,2|1,2|1,2|1,2|1,2|1,2|X,1,1").ScoreEquals(39);
        }

        [Test]
        public void LastFrameStrikeSpareAltertnativeNotation()
        {
            WhenScoreCard("1,2|1,2|1,2|1,2|1,2|1,2|1,2|1,2|1,2|X,2,/").ScoreEquals(47);
        }

        [Test]
        public void RandomSpare()
        {
            WhenScoreCard("1,2|1,2|1,/|1,2|1,2|1,2|1,2|1,2|1,2|1,2").ScoreEquals(38);
        }

        [Test]
        public void RandomStrike()
        {
            WhenScoreCard("1,2|1,2|X|1,2|1,2|1,2|1,2|1,2|1,2|1,2").ScoreEquals(40);
        }

        [Test]
        public void Bonusless()
        {
            WhenScoreCard("1,2|3,4|5,1|2,3|4,5|1,2|3,4|5,1|2,3|4,5").ScoreEquals(60);
        }

        [Test]
        public void Heartbreak()
        {
            WhenScoreCard("9|9|9|9|9|9|9|9|9|9").ScoreEquals(90);
        }

        [Test]
        public void Perfect()
        {
            WhenScoreCard("X|X|X|X|X|X|X|X|X|X,X,X").ScoreEquals(300);
        }

        [Test]
        public void AllSpares()
        {
            WhenScoreCard("5,/|5,/|5,/|5,/|5,/|5,/|5,/|5,/|5,/|5,/,5").ScoreEquals(150);
        }


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
