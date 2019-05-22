﻿using ColossalFramework;

namespace GameSpeedMod
{
    public static class Parks
    {
        private static int[] visitorLevelupRequirement_orig = null;

        public static void SetVisitorsLevelupRequirement()
        {
            GameSpeedManager gsm = Singleton<GameSpeedManager>.instance;
            DistrictManager dm = Singleton<DistrictManager>.instance;

            if (dm.m_properties == null || dm.m_properties.m_parkProperties == null) return;

            //System.Text.StringBuilder sb = new System.Text.StringBuilder("m_parkProperties: ");
            int infosCount = dm.m_properties.m_parkProperties.m_parkLevelInfo.Length;
            visitorLevelupRequirement_orig = new int[infosCount];
            for (int i = 0; i < infosCount; i++)
            {
                //sb.Append(dm.m_properties.m_parkProperties.m_parkLevelInfo[i].m_visitorLevelupRequirement.ToString() + ",");
                visitorLevelupRequirement_orig[i] = dm.m_properties.m_parkProperties.m_parkLevelInfo[i].m_visitorLevelupRequirement;
                dm.m_properties.m_parkProperties.m_parkLevelInfo[i].m_visitorLevelupRequirement = visitorLevelupRequirement_orig[i] * gsm.Parameters.LevelupRequirement / 100;
            }
            //Original values: 0,0,0,0,0,0,0,500,2500,5000,10000,0,0,500,2500,5000,10000,0,0,500,2500,5000,10000,0,0,500,2500,5000,10000,0
            //Debug.Log(sb.ToString());
        }

        public static void ResetVisitorsLevelupRequirement()
        {
            if (visitorLevelupRequirement_orig == null) return;

            DistrictManager dm = Singleton<DistrictManager>.instance;
            for (int i = 0; i < dm.m_properties.m_parkProperties.m_parkLevelInfo.Length; i++)
            {
                dm.m_properties.m_parkProperties.m_parkLevelInfo[i].m_visitorLevelupRequirement = visitorLevelupRequirement_orig[i];
            }

            visitorLevelupRequirement_orig = null;
        }
    }
}
