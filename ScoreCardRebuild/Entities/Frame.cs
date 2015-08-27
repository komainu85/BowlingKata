using System.Collections.Generic;
using System.Linq;

namespace ScoreCardRebuild.Entities
{
    public class Frame
    {
        public List<Ball> Balls { get; set; } = new List<Ball>();

        public bool Strike { get; set; }

        public bool Spare { get; set; }

        public bool FinalFrame { get; set; }

        public int FrameNumber { get; set; }

        public Frame(int frameNumber)
        {
            FrameNumber = frameNumber;
        }

    }
}