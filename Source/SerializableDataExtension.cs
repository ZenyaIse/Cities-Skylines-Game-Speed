using System;
using System.IO;
using ColossalFramework.IO;
using ICities;
using UnityEngine;

namespace GameSpeedMod
{
    public class SerializableDataExtension : ISerializableDataExtension
    {
        public const string DataID = "GameSpeedMod";
        public const uint DataVersion = 0;
        private ISerializableData serializedData;

        public void OnCreated(ISerializableData serializedData)
        {
            this.serializedData = serializedData;
        }

        public void OnReleased()
        {
            serializedData = null;
        }

        public void OnLoadData()
        {
            try
            {
                byte[] data = serializedData.LoadData(DataID);

                if (data == null)
                {
                    Debug.Log("GameSpeedMod: No saved data ");
                    return;
                }

                using (var stream = new MemoryStream(data))
                {
                    DataSerializer.Deserialize<GameSpeedManager.Data>(stream, DataSerializer.Mode.Memory);
                }
            }
            catch (Exception ex)
            {
                Debug.Log("GameSpeedMod: load error: " + ex.Message);
            }
        }

        public void OnSaveData()
        {
            try
            {
                byte[] data;

                using (var stream = new MemoryStream())
                {
                    DataSerializer.Serialize(stream, DataSerializer.Mode.Memory, DataVersion, new GameSpeedManager.Data());
                    data = stream.ToArray();
                }

                serializedData.SaveData(DataID, data);
            }
            catch (Exception ex)
            {
                Debug.Log("GameSpeedMod: save error: " + ex.Message);
            }
        }
    }
}
