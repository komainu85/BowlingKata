using System.Collections.Generic;
using System.Linq;

namespace ScoreCardRebuild.Entities
{
    public class Frame
    {
        public List<Ball> Balls { get; set; } = new List<Ball>();

        public bool Strike => Balls.Any() && Balls.First().Pins == 10;

        public bool Spare => Balls.Count > 1 && Balls[0].Pins + Balls[1].Pins == 10;

        public bool FinalFrame => FrameNumber == 10;

        public int FrameNumber { get; set; }

        public Frame(int frameNumber)
        {
            FrameNumber = frameNumber;
        }

    }
}