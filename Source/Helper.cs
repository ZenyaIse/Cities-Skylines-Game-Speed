using System;
using System.Collections.Generic;
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

        public static IEnumerable<T> PrefabBuildingAIs<T>() where T : class
        {
            int prefabsCount = PrefabCollection<BuildingInfo>.PrefabCount();
            for (uint i = 0; i < prefabsCount; i++)
            {
                BuildingInfo bi = PrefabCollection<BuildingInfo>.GetPrefab(i);
                if (bi == null || bi.m_buildingAI == null) continue;

                if (bi.m_buildingAI as T != null)
                {
                    yield return bi.m_buildingAI as T;
                }
            }
        }
    }
}
