using ColossalFramework;

namespace GameSpeedMod
{
    public static class Landscaping
    {
        private static int costOrig = 0;

        public static void SetLanscapingCost()
        {
            if (costOrig == 0 && Singleton<TerrainManager>.exists && Singleton<TerrainManager>.instance.m_properties != null)
            {
                costOrig = Singleton<TerrainManager>.instance.m_properties.m_dirtPrice;
                int newCost = costOrig * Singleton<GameSpeedManager>.instance.Parameters.ConstructionCostMultiplier;
                Singleton<TerrainManager>.instance.m_properties.m_dirtPrice = newCost;
                ModLogger.Add("Lanscaping", "Cost", costOrig, newCost);
            }
        }

        public static void ResetLanscapingCost()
        {
            if (costOrig > 0 && Singleton<TerrainManager>.exists && Singleton<TerrainManager>.instance.m_properties != null)
            {
                Singleton<TerrainManager>.instance.m_properties.m_dirtPrice = costOrig;
                costOrig = 0;
                ModLogger.Add("Reset lanscaping cost");
            }
        }
    }
}
