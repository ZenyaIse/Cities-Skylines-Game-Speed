using ICities;
using ColossalFramework;

namespace GameSpeedMod
{
    public class OilAndOreResource : ResourceExtensionBase
    {
        public override void OnAfterResourcesModified(int x, int z, NaturalResource type, int amount)
        {
            if ((type == NaturalResource.Oil || type == NaturalResource.Ore) && amount < 0)
            {
                int depletionRate = Singleton<GameSpeedManager>.instance.Parameters.OilOreDepletionRate;

                // Vanilla original rate (100%)
                if (depletionRate == 100) return;

                if (Singleton<SimulationManager>.instance.m_randomizer.Int32(100u) >= depletionRate)
                {
                    // From the vanilla original UnlimitedOilAndOre mod
                    resourceManager.SetResource(x, z, type, (byte)(resourceManager.GetResource(x, z, type) - amount), false);
                }
            }
        }
    }
}
