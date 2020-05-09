using System;
using ColossalFramework;
using ICities;

namespace GameSpeedMod
{
    public class Milestones : MilestonesExtensionBase
    {
        public override int OnGetPopulationTarget(int originalTarget, int scaledTarget)
        {
            if (!LoadingExtension.IsLeveLoaded) return 999999;

            GameSpeedManager gs = Singleton<GameSpeedManager>.instance;
            int value;

            if (gs.values.IsMilestonePopulationThresholdUnscaled)
            {
                value = (int)Math.Round(originalTarget * gs.Parameters.MilestonePopulationThreshholdMultiplier);
            }
            else
            {
                value = (int)Math.Round(scaledTarget * gs.Parameters.MilestonePopulationThreshholdMultiplier);
            }

            return value;
        }
    }
}
