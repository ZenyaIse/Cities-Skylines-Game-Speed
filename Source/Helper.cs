using ColossalFramework;

namespace GameSpeedMod
{
    public static class Helper
    {
        public static int GetPopulation()
        {
            if (Singleton<DistrictManager>.exists)
            {
                return (int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_populationData.m_finalCount;
            }
            return 0;
        }
    }
}
