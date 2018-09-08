using ICities;
using ColossalFramework;

namespace GameSpeedMod
{
    public class Areas : AreasExtensionBase
    {
        public override int OnGetAreaPrice(uint ore, uint oil, uint forest, uint fertility, uint water, bool road, bool train, bool ship, bool plane, float landFlatness, int originalPrice)
        {
            GameSpeedManager gs = Singleton<GameSpeedManager>.instance;

            return originalPrice * gs.Parameters.AreaCostMultiplier;
        }
    }
}
