using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreCardRebuild
{
    public class Bowling
    {
        public void Play()
        {
            var scores = Console.ReadLine();

            var scoreCard = new ScoreCard(scores);

            Console.WriteLine(scoreCard.GameScore);

            Console.ReadKey();
        }


    }
}
