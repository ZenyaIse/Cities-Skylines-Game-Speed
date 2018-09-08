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

                s.WriteInt32(m.DemandRestorePercent);
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

                m.DemandRestorePercent = s.ReadInt32();
                string[] builtMonumentsArray = s.ReadUniqueStringArray();
                m.BuiltMonuments = new HashSet<string>(builtMonumentsArray);

                Debug.Log(">>> GameSpeedMod data loaded.");
            }

            public void AfterDeserialize(DataSerializer s)
            {
                // Empty
            }
        }

        public string[] GameSpeeds = new string[] { "Normal", "Slow", "Epic", "Marathon", "1001 Nights" };

        public GameSpeedOptionsSerializable values = new GameSpeedOptionsSerializable();
        public GameSpeedParameters Parameters;

        public int DemandRestorePercent = 0;
        public HashSet<string> BuiltMonuments = new HashSet<string>();


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
                DemandRestorePercent = 150;
            }
        }
    }
}
