using System;
using ColossalFramework;
using ICities;

namespace GameSpeedMod
{
    public class Milestones : MilestonesExtensionBase
    {
        public override int OnGetPopulationTarget(int originalTarget, int scaledTarget)
        {
            GameSpeedManager gs = Singleton<GameSpeedManager>.instance;

            return (int)Math.Round((gs.values.IsMilestonePopulationThreshholdUnscaled ? originalTarget : scaledTarget) * gs.Parameters.MilestonePopulationThreshholdMultiplier);
        }
    }
}
