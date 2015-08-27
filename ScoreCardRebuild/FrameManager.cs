using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScoreCardRebuild.Entities;

namespace ScoreCardRebuild
{
    public class FrameManager
    {
        private const int MaxFrames = 10;
        private const int MaxFrameScore = 10;

        public Frame CreateFrame(int frameNumber, List<Ball> balls)
        {
            Frame frame = new Frame(frameNumber);

            frame.FinalFrame = frame.FrameNumber == MaxFrames;
            frame.Balls = balls;
            frame.Strike = IsStrike(balls);
            frame.Spare = ISpare(balls);

            return frame;
        }

        private bool IsStrike(IEnumerable<Ball> balls)
        {
            return balls.First().Pins == MaxFrameScore;
        }

        private bool ISpare(List<Ball> balls)
        {
            bool spare = false;

            if (balls.Count > 1)
            {
                if ((balls[0].Pins + balls[1].Pins) == MaxFrameScore)
                {
                    spare = true;
                }
            }

            return spare;
        }


    }
}
