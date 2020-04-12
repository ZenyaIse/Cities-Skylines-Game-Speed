using ICities;
using ColossalFramework;

namespace GameSpeedMod
{
    public class Economy : EconomyExtensionBase
    {
        public override int OnAddResource(EconomyResource resource, int amount, Service service, SubService subService, Level level)
        {
            if (resource == EconomyResource.RewardAmount)
            {
                if (Singleton<GameSpeedManager>.instance.values.IsHardMode) return 0;
            }

            return amount;
        }

        public override int OnGetConstructionCost(int originalConstructionCost, Service service, SubService subService, Level level)
        {
            GameSpeedManager gs = Singleton<GameSpeedManager>.instance;
            return originalConstructionCost * gs.Parameters.ConstructionCostMultiplier;
        }

        public override int OnGetMaintenanceCost(int originalMaintenanceCost, Service service, SubService subService, Level level)
        {
            if (Singleton<GameSpeedManager>.instance.values.IsHardMode)
                return originalMaintenanceCost * 5 / 4;
            else
                return originalMaintenanceCost;
        }
    }
}
