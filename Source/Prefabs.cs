using ColossalFramework;

namespace GameSpeedMod
{
    public static class Prefabs
    {
        private const int constructionTimeVanilla = 30;

        public static void SetBldPrefabs()
        {
            int m = Singleton<GameSpeedManager>.instance.Parameters.ConstructionTimeMultiplier;
            if (setBldPrefabs(m))
            {
                ModLogger.Add("All private buildings", "construction time", constructionTimeVanilla, constructionTimeVanilla * m);
            }
        }

        public static void ResetBldPrefabs()
        {
            if (setBldPrefabs(1))
            {
                ModLogger.Add("Reset all private buildings construction time", constructionTimeVanilla);
            }
        }

        private static bool setBldPrefabs(int constructionTimeMultiplier)
        {
            int count = 0;

            int prebabsCount = PrefabCollection<BuildingInfo>.PrefabCount();
            for (uint i = 0; i < prebabsCount; i++)
            {
                BuildingInfo bi = PrefabCollection<BuildingInfo>.GetPrefab(i);
                if (bi == null) continue;

                PrivateBuildingAI ai = bi.m_buildingAI as PrivateBuildingAI;
                if (ai == null) continue;

                ai.m_constructionTime = constructionTimeVanilla * constructionTimeMultiplier;
                count++;
            }

            return count > 0;
        }
    }
}
