using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace GameSpeedMod   
{
    public class GameSpeedOptionsSerializable
    {
        public int Version = 0;
        public int GameSpeedIndex = 0;
        public bool IsMilestonePopulationThreshholdUnscaled = false;
        public bool NoReward = false;
        public bool IsHardMode = false;

        private const string optionsFileName = "GameSpeed.xml";

        public void Save()
        {
            XmlSerializer ser = new XmlSerializer(typeof(GameSpeedOptionsSerializable));
            try
            {
                TextWriter writer = new StreamWriter(getOptionsFilePath());
                ser.Serialize(writer, this);
                writer.Close();
                Debug.Log("Game Speed Mod: Options file is saved.");
            }
            catch
            {
                Debug.Log("Game Speed Mod: Could not write options file.");
            }
        }

        public static GameSpeedOptionsSerializable CreateFromFile()
        {
            string path = getOptionsFilePath();

            if (!File.Exists(path)) return null;

            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(GameSpeedOptionsSerializable));
                TextReader reader = new StreamReader(path);
                GameSpeedOptionsSerializable instance = (GameSpeedOptionsSerializable)ser.Deserialize(reader);
                reader.Close();

                return instance;
            }
            catch
            {
                Debug.Log("Game Speed Mod: Error reading options file.");
                return null;
            }
        }

        private static string getOptionsFilePath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Colossal Order\\Cities_Skylines\\" + optionsFileName;
        }
    }
}