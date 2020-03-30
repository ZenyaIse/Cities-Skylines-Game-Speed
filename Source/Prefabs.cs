using UnityEngine;
using ColossalFramework;

namespace GameSpeedMod
{
    public static class Prefabs
    {
        private const int constructionTimeVanilla = 30;

        public static void SetBldPrefabs()
        {
            int m = Singleton<GameSpeedManager>.instance.Parameters.ConstructionTimeMultiplier;

            int prebabsCount = PrefabCollection<BuildingInfo>.PrefabCount();
            for (uint i = 0; i < prebabsCount; i++)
            {
                BuildingInfo bi = PrefabCollection<BuildingInfo>.GetPrefab(i);
                if (bi == null) continue;

                PrivateBuildingAI ai = bi.m_buildingAI as PrivateBuildingAI;
                if (ai == null) continue;

                ai.m_constructionTime = constructionTimeVanilla * m;
            }

            Debug.Log("GameSpeedMod: increased construction time for all private buildings in " + m.ToString() + " times");
        }
    }
}
