using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Observer;
using Level.Entity;
using UnityEngine;

namespace Domain
{
    [Serializable]
    public sealed class GameModel : Observable
    {
        private readonly string FarmString = "Farm";
        private readonly string FarmLevelString = "FarmLevel";
        private readonly string FarmProgressString = "FarmProgress";
        private readonly string IsPurchasedString = "IsPurchased";
        private readonly string LevelString = "Lvl";
        private readonly string CashString = "Cash";
        
        public int Cash = 500;
        public int Farm;
        public static GameModel Load(GameConfig config)
        {
            try
            {
                if (!File.Exists(GameConstants.SaveModelFilePath))
                {
                    var model = new GameModel();
                    model.Prepare(config);
                    return model;
                }

                var data = File.ReadAllText(GameConstants.SaveModelFilePath);
                var result = JsonUtility.FromJson<GameModel>(data);
                return result;
            }
            catch (Exception)
            {
                File.Delete(GameConstants.SaveModelFilePath);
                var model = new GameModel();
                model.Prepare(config);
                return model;
            }
        }



        void Prepare(GameConfig config)
        {
            Cash = config.DefaultCash;
            Farm = config.DefaultFarm;
        }

        public void Save()
        {
            string data = JsonUtility.ToJson(this);
            File.WriteAllText(GameConstants.SaveModelFilePath, data);
        }

        public void Remove()
        {
            if (File.Exists(GameConstants.SaveModelFilePath))
            {
                File.Delete(GameConstants.SaveModelFilePath);
            }
        }

        internal string GenerateEntityID(int farm, EntityType type, int number)
        {
            return FarmString + farm + type.ToString() + number;
        }

        internal int LoadLvl()
        {
            string key = FarmLevelString + Farm;
            return DataSaver.LoadIntFromJson(key, 1);
        }

        internal void SaveLvl(int lvl)
        {
            string key = FarmLevelString + Farm;
            DataSaver.SaveIntToJson(key, lvl);
        }
        
        internal int LoadProgress()
        {
            string key = FarmProgressString + Farm;
            return DataSaver.LoadIntFromJson(key, 0);
        }

        internal void SaveProgress(int progress)
        {
            string key = FarmProgressString + Farm;
            DataSaver.SaveIntToJson(key, progress);
        }

        internal bool LoadPlaceIsPurchased(string id)
        {
            string areaOneID = GenerateEntityID(Farm, EntityType.Area, 1);
            string roomZeroID = GenerateEntityID(Farm, EntityType.Plant, 0);

            string key = IsPurchasedString + id;
            int value = DataSaver.LoadIntFromJson(key, 0);
            return roomZeroID == id || areaOneID == id || value == 1;
        }

        internal void SavePlaceIsPurchased(string id)
        {
            string key = IsPurchasedString + id;
            DataSaver.SaveIntToJson(key, 1);
        }

        public void SavePlaceLvl(string id, int lvl)
        {
            string key = LevelString + id;
            DataSaver.SaveIntToJson(key, lvl);
        }

        public int LoadPlaceLvl(string id)
        {
            string key = LevelString + id;
            return DataSaver.LoadIntFromJson(key, 0);
        }

        public void SavePlaceCash(string id, long cash)
        {
            string key = CashString + id;
            DataSaver.SaveLongToJson(key, cash);
        }

        public long LoadPlaceCash(string id)
        {
            string key = CashString + id;
            return DataSaver.LoadLongFromJson(key, 0);
        }
        
 
    }
}