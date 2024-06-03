using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Domain
{
    public static class DataSaver
    {
        public static int LoadIntFromJson(string key, int defaultValue)
        {
            var data = LoadJsonData();
            if (data.ContainsKey(key) && int.TryParse(data[key], out int value))
            {
                return value;
            }

            return defaultValue;
        }

        public static void SaveIntToJson(string key, int value)
        {
            Dictionary<string, string> data = LoadJsonData();
            data[key] = value.ToString();
            SaveJsonData(data);
        }

        public static long LoadLongFromJson(string key, long defaultValue)
        {
            Dictionary<string, string> data = LoadJsonData();
            if (data.ContainsKey(key) && long.TryParse(data[key], out long value))
            {
                return value;
            }

            return defaultValue;
        }

        public static void SaveLongToJson(string key, long value)
        {
            Dictionary<string, string> data = LoadJsonData();
            data[key] = value.ToString();
            SaveJsonData(data);
        }

        private static Dictionary<string, string> LoadJsonData()
        {
            if (!File.Exists(GameConstants.SaveGameDataFilePath))
            {
                return new Dictionary<string, string>();
            }
            var json = File.ReadAllText(Path.Combine(GameConstants.SaveGameDataFilePath));
            var serializableData = JsonUtility.FromJson<SerializableDictionary>(json);
            return serializableData.ToDictionary();
        }
        private static void SaveJsonData(Dictionary<string, string> data)
        {
            SerializableDictionary serializableData = new SerializableDictionary();
            serializableData.FromDictionary(data);
            string json = JsonUtility.ToJson(serializableData);
            File.WriteAllText(Path.Combine(GameConstants.SaveGameDataFilePath), json);
        }
    }
}