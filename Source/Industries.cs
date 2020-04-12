using ColossalFramework;

namespace GameSpeedMod
{
    public static class Industries
    {
        private static int[] industryLevelInfo_orig = null;

        public static void SetProductionLevelupRequirement()
        {
            GameSpeedManager gsm = Singleton<GameSpeedManager>.instance;
            DistrictManager dm = Singleton<DistrictManager>.instance;

            if (dm.m_properties == null || dm.m_properties.m_parkProperties == null) return;

            //System.Text.StringBuilder sb = new System.Text.StringBuilder("m_productionLevelupRequirement: ");
            int infosCount = dm.m_properties.m_parkProperties.m_industryLevelInfo.Length;
            industryLevelInfo_orig = new int[infosCount];
            for (int i = 0; i < infosCount; i++)
            {
                //sb.Append(dm.m_properties.m_parkProperties.m_industryLevelInfo[i].m_productionLevelupRequirement.ToString() + ",");
                //Original values: 0,500000,1500000,4500000,13500000,0
                int oldValue = dm.m_properties.m_parkProperties.m_industryLevelInfo[i].m_productionLevelupRequirement;
                int newValue = oldValue / 100 * gsm.Parameters.LevelupRequirement;

                industryLevelInfo_orig[i] = oldValue;
                dm.m_properties.m_parkProperties.m_industryLevelInfo[i].m_productionLevelupRequirement = newValue;

                Logger.Add(dm.m_properties.m_parkProperties.name, "productionLevelupRequirement" + i.ToString(), oldValue / 1000, newValue / 1000);
            }
            //Debug.Log(sb.ToString());
        }

        public static void ResetProductionLevelupRequirement()
        {
            if (industryLevelInfo_orig == null) return;

            DistrictManager dm = Singleton<DistrictManager>.instance;
            for (int i = 0; i < dm.m_properties.m_parkProperties.m_industryLevelInfo.Length; i++)
            {
                dm.m_properties.m_parkProperties.m_industryLevelInfo[i].m_productionLevelupRequirement = industryLevelInfo_orig[i];
            }

            industryLevelInfo_orig = null;

            Logger.Add("Reset productionLevelupRequirement");
        }
    }
}
