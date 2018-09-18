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

                s.WriteInt32(m.demandRestoreFrameCounter);
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

                m.demandRestoreFrameCounter = s.ReadInt32();
                string[] builtMonumentsArray = s.ReadUniqueStringArray();
                m.BuiltMonuments = new HashSet<string>(builtMonumentsArray);
            }

            public void AfterDeserialize(DataSerializer s)
            {
                Debug.Log(">>> GameSpeedMod data loaded.");
            }
        }

        protected const int framesPerDay = 585; // See m_timePerFrame from SimulationManager.Awake()

        public string[] GameSpeeds = new string[] { "Normal", "Slow", "Epic", "Marathon", "1001 Nights" };

        public GameSpeedOptionsSerializable values = new GameSpeedOptionsSerializable();
        public GameSpeedParameters Parameters;

        private int demandRestoreFrameCounter = 0;
        public HashSet<string> BuiltMonuments = new HashSet<string>();

        public int GetDemandRestorePercent()
        {
            int demandRestorePercent = demandRestoreFrameCounter * 100 / (framesPerDay * 14);

            if (demandRestorePercent > 100) return 100;

            return demandRestorePercent;
        }

        public void StartAdvertisingCampaign()
        {
            demandRestoreFrameCounter += framesPerDay * 28; // Four weeks campaign
        }

        public void OnAfterSimulationFrame()
        {
            if (demandRestoreFrameCounter > 0)
            {
                demandRestoreFrameCounter--;
            }
        }

        public int CampaignDaysLeft
        {
            get
            {
                return demandRestoreFrameCounter / framesPerDay;
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
            Parameters = new GameSpeedParameters(values.GameSpeedIndex);
            values.Save();
        }

        public void OnMonumentBuilt(string name)
        {
            if (BuiltMonuments.Add(name))
            {
                StartAdvertisingCampaign();
            }
        }
    }
}
