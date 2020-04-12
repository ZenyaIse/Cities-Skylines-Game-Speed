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
                s.WriteBool(m.values.IsHardMode);
            }

            public void Deserialize(DataSerializer s)
            {
                GameSpeedManager m = Singleton<GameSpeedManager>.instance;
                m.values.GameSpeedIndex = s.ReadInt32();
                m.values.IsMilestonePopulationThreshholdUnscaled = s.ReadBool();
                m.values.IsHardMode = s.ReadBool();
            }

            public void AfterDeserialize(DataSerializer s)
            {
                Debug.Log(">>> GameSpeedMod data loaded.");
            }
        }

        public string[] GameSpeeds = new string[] { "Normal", "Slow", "Epic", "Marathon", "1001 Nights" };

        public GameSpeedOptionsSerializable values = new GameSpeedOptionsSerializable();
        public GameSpeedParameters Parameters;

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
    }
}
