using System.Collections.Generic;
using ColossalFramework;
using UnityEngine;
using ColossalFramework.IO;

namespace GameSpeedMod
{
    public class GameSpeedManager : Singleton<GameSpeedManager>
    {
        public class Data : IDataContainer
        {
            public void Serialize(DataSerializer s)
            {
                GameSpeedManager m = Singleton<GameSpeedManager>.instance;
                s.WriteInt32(m.values.GameSpeedIndex);
                s.WriteBool(m.values.IsMilestonePopulationThreshholdUnscaled);
                s.WriteBool(m.values.NoReward);
                s.WriteBool(m.values.IsHardMode);

                s.WriteFloat(m.advertisingCampaignDaysLeft);
                string[] builtMonumentsArray = new string[m.BuiltMonuments.Count];
                m.BuiltMonuments.CopyTo(builtMonumentsArray);
                s.WriteUniqueStringArray(builtMonumentsArray);
            }

            public void Deserialize(DataSerializer s)
            {
                GameSpeedManager m = Singleton<GameSpeedManager>.instance;
                m.values.GameSpeedIndex = s.ReadInt32();
                m.values.IsMilestonePopulationThreshholdUnscaled = s.ReadBool();
                m.values.NoReward = s.ReadBool();
                m.values.IsHardMode = s.ReadBool();

                m.advertisingCampaignDaysLeft = s.ReadFloat();
                string[] builtMonumentsArray = s.ReadUniqueStringArray();
                m.BuiltMonuments = new HashSet<string>(builtMonumentsArray);
            }

            public void AfterDeserialize(DataSerializer s)
            {
                Debug.Log(">>> GameSpeedMod data loaded.");
            }
        }

        //protected const int framesPerDay = 585; // See m_timePerFrame from SimulationManager.Awake()

        public string[] GameSpeeds = new string[] { "Normal", "Slow", "Epic", "Marathon", "1001 Nights" };

        public GameSpeedOptionsSerializable values = new GameSpeedOptionsSerializable();
        public GameSpeedParameters Parameters;

        public int StartAdvertisingCampaignDays = 7;
        private float advertisingCampaignDaysLeft = 0;
        public HashSet<string> BuiltMonuments = new HashSet<string>();

        public int GetDemandRestorePercent()
        {
            if (advertisingCampaignDaysLeft <= 0)
            {
                return 0;
            }

            if (advertisingCampaignDaysLeft > 3)
            {
                return 100;
            }

            return (int)(advertisingCampaignDaysLeft * 100 / 3);
        }

        public void StartAdvertisingCampaign(string reason)
        {
            if (advertisingCampaignDaysLeft < 0)
            {
                advertisingCampaignDaysLeft = 0;
            }

            Debug.Log("Started advertising campaign: because " + reason);
            advertisingCampaignDaysLeft += StartAdvertisingCampaignDays;
        }

        public void OnAfterSimulationFrame()
        {
            // Do not count down when there is no people
            if (Helper.GetPopulation() == 0) return;

            if (advertisingCampaignDaysLeft > 0)
            {
                advertisingCampaignDaysLeft -= (float)SimulationManager.instance.m_timePerFrame.TotalDays;
            }
        }

        public int CampaignDaysLeft
        {
            get
            {
                return (int)advertisingCampaignDaysLeft;
            }
        }

        private GameSpeedManager()
        {
            values = GameSpeedOptionsSerializable.CreateFromFile();

            if (values == null)
            {
                values = new GameSpeedOptionsSerializable();
            }

            Parameters = new GameSpeedParameters(values.GameSpeedIndex);
        }

        public void AfterOptionChanged()
        {
            Loans.ResetLoans();
            Parks.ResetVisitorsLevelupRequirement();
            Industries.ResetProductionLevelupRequirement();

            Parameters = new GameSpeedParameters(values.GameSpeedIndex);

            Loans.SetLoans();
            Parks.SetVisitorsLevelupRequirement();
            Industries.SetProductionLevelupRequirement();
            Prefabs.SetBldPrefabs();
            TimeFlow.SetTimeFlow();
            values.Save();
        }

        public void OnMonumentBuilt(string name)
        {
            if (BuiltMonuments.Add(name))
            {
                StartAdvertisingCampaign(name + " monument was built.");
            }
        }

        private int getFramesPerDay()
        {
            return (int)(SimulationManager.instance.m_timePerFrame.Ticks / 10000000);
        }
    }
}
