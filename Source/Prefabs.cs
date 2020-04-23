using ColossalFramework;
using System.Collections.Generic;
using UnityEngine;

namespace GameSpeedMod
{
    public static class Prefabs
    {
        private const int constructionTimeVanilla = 30;
        private static Dictionary<string, object> origValues = new Dictionary<string, object>();

        public static void SetBldPrefabs()
        {
            int m = Singleton<GameSpeedManager>.instance.Parameters.ConstructionTimeMultiplier;
            if (setConstructionTime(m))
            {
                ModLogger.Add("All private buildings", "construction time", constructionTimeVanilla, constructionTimeVanilla * m);
            }

            updateCemetries(true);
        }

        public static void ResetBldPrefabs()
        {
            if (setConstructionTime(1))
            {
                ModLogger.Add("Reset all private buildings construction time", constructionTimeVanilla);
            }

            updateCemetries(false);
        }

        private static bool setConstructionTime(int constructionTimeMultiplier)
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

        private static void updateCemetries(bool isSet)
        {
            int t10 = Singleton<GameSpeedManager>.instance.Parameters.TimeFlowMultiplier_x10;

            // Decrease Hearse capacity
            int vehiclePrefabsCount = PrefabCollection<VehicleInfo>.PrefabCount();
            for (uint i = 0; i < vehiclePrefabsCount; i++)
            {
                VehicleInfo vi = PrefabCollection<VehicleInfo>.GetPrefab(i);
                if (vi == null) continue;

                if (vi.m_vehicleAI as HearseAI == null) continue;

                string vehicleName = vi.m_vehicleAI.name;

                string key = vehicleName + "_corpseCapacity";
                if (isSet)
                {
                    int prevValue = ((HearseAI)vi.m_vehicleAI).m_corpseCapacity;
                    int newValue = Mathf.Max(1, prevValue * 10 / t10);
                    origValues[key] = prevValue;
                    ModLogger.Add(vehicleName, "corpseCapacity", prevValue, newValue);
                    ((HearseAI)vi.m_vehicleAI).m_corpseCapacity = newValue;
                }
                else if (origValues.ContainsKey(key))
                {
                    ModLogger.Add(vehicleName, "Reset corpseCapacity");
                    ((HearseAI)vi.m_vehicleAI).m_corpseCapacity = (int)origValues[key];
                    origValues.Remove(key);
                }
            }

            // Decrease Cemetery and Crematory capacities
            int bldPrefabsCount = PrefabCollection<BuildingInfo>.PrefabCount();
            for (uint i = 0; i < bldPrefabsCount; i++)
            {
                BuildingInfo bi = PrefabCollection<BuildingInfo>.GetPrefab(i);
                if (bi == null) continue;

                if (bi.m_buildingAI as CemeteryAI == null) continue;

                string bldName = ((PlayerBuildingAI)bi.m_buildingAI).name;

                string key = bldName + "_graveCount";
                if (isSet)
                {
                    int prevValue = ((CemeteryAI)bi.m_buildingAI).m_graveCount;
                    int newValue = prevValue * 10 / t10;
                    origValues[key] = prevValue;
                    ModLogger.Add(bldName, "graveCount", prevValue, newValue);
                    ((CemeteryAI)bi.m_buildingAI).m_graveCount = newValue;
                }
                else if (origValues.ContainsKey(key))
                {
                    ModLogger.Add(bldName, "Reset graveCount");
                    ((CemeteryAI)bi.m_buildingAI).m_graveCount = (int)origValues[key];
                    origValues.Remove(key);
                }

                key = bldName + "_corpseCapacity";
                if (isSet)
                {
                    int prevValue = ((CemeteryAI)bi.m_buildingAI).m_corpseCapacity;
                    int newValue = prevValue * 10 / t10;
                    origValues[key] = prevValue;
                    ModLogger.Add(bldName, "corpseCapacity", prevValue, newValue);
                    ((CemeteryAI)bi.m_buildingAI).m_corpseCapacity = newValue;
                }
                else if (origValues.ContainsKey(key))
                {
                    ModLogger.Add(bldName, "Reset corpseCapacity");
                    ((CemeteryAI)bi.m_buildingAI).m_corpseCapacity = (int)origValues[key];
                    origValues.Remove(key);
                }
            }
        }
    }
}
