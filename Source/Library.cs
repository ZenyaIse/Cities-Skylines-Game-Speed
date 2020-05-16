using ColossalFramework;
using System.Collections.Generic;

namespace GameSpeedMod
{
    public static class Library
    {
        private static float percentageChanceElementaryEducation_orig = float.NaN;
        private static float percentageChanceHighschoolEducation_orig = float.NaN;
        private static float percentageChanceUniversityEducation_orig = float.NaN;

        public static void Init()
        {
            if (!float.IsNaN(percentageChanceElementaryEducation_orig)) return; // Already set

            float k = Singleton<GameSpeedManager>.instance.Parameters.TimeFlowMultiplier_x10 * 0.1f;

            foreach (LibraryAI libAI in LibraryBuildings())
            {
                percentageChanceElementaryEducation_orig = libAI.m_percentageChanceElementaryEducation;
                percentageChanceHighschoolEducation_orig = libAI.m_percentageChanceHighschoolEducation;
                percentageChanceUniversityEducation_orig = libAI.m_percentageChanceUniversityEducation;
                libAI.m_percentageChanceElementaryEducation = percentageChanceElementaryEducation_orig / k;
                libAI.m_percentageChanceHighschoolEducation = percentageChanceHighschoolEducation_orig / k;
                libAI.m_percentageChanceUniversityEducation = percentageChanceUniversityEducation_orig / k;

                ModLogger.Add("PercentageChanceEducation for " + libAI.name,
                    "Elementary", percentageChanceElementaryEducation_orig, libAI.m_percentageChanceElementaryEducation,
                    "Highschool", percentageChanceHighschoolEducation_orig, libAI.m_percentageChanceHighschoolEducation,
                    "University", percentageChanceUniversityEducation_orig, libAI.m_percentageChanceUniversityEducation
                    );
            }
        }

        public static void Reset()
        {
            if (float.IsNaN(percentageChanceElementaryEducation_orig)) return; // Not set

            foreach (LibraryAI libAI in LibraryBuildings())
            {
                libAI.m_percentageChanceElementaryEducation = percentageChanceElementaryEducation_orig;
                libAI.m_percentageChanceHighschoolEducation = percentageChanceHighschoolEducation_orig;
                libAI.m_percentageChanceUniversityEducation = percentageChanceUniversityEducation_orig;

                ModLogger.Add("Reset percentageChanceEducation for " + libAI.name);
            }

            percentageChanceElementaryEducation_orig = float.NaN;
            percentageChanceHighschoolEducation_orig = float.NaN;
            percentageChanceUniversityEducation_orig = float.NaN;
        }

        private static IEnumerable<LibraryAI> LibraryBuildings()
        {
            int prefabsCount = PrefabCollection<BuildingInfo>.PrefabCount();
            for (uint i = 0; i < prefabsCount; i++)
            {
                BuildingInfo bi = PrefabCollection<BuildingInfo>.GetPrefab(i);
                if (bi == null) continue;

                LibraryAI libAI = bi.m_buildingAI as LibraryAI;
                if (libAI == null) continue;

                yield return libAI;
            }
        }
    }
}
