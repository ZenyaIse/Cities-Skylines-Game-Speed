using ColossalFramework;
using ICities;

namespace GameSpeedMod
{
    public class Threading : ThreadingExtensionBase
    {
        private const int refreshCount = 70; // Two weeks campaign
        private int count = refreshCount;

        public override void OnAfterSimulationFrame()
        {
            if (count-- > 0) return;
            count = refreshCount;

            if (Singleton<GameSpeedManager>.instance.DemandRestorePercent > 0)
            {
                Singleton<GameSpeedManager>.instance.DemandRestorePercent--;
            }
        }
    }
}
