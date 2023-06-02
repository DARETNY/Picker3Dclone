using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Manager;
using Market.items;
using UnityEngine;

namespace system
{
    public class SaveSystem : MonoBehaviour
    {

        public int level = 0;
        public int score = 0;
        public İtemsSo[] items;

        private void Awake()
        {
            Load();
            

        }

        private void Start()
        {
            GameManager.Instance.currentLevel = level;
            GameManager.Instance.saveManager = this;
            GameManager.Instance.money = score;
        }


        private void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/playerInfo.data"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.data", FileMode.Open);
                PlayerData data = (PlayerData)bf.Deserialize(file);
                level = data.Level;
                score = data.score;
                items = data.items;

                if (data.items!=null)
                {
                   
                    foreach (var item in items)
                    {
                        item.isPurchaed = true;
                    } 
                    MarketSystem.spawnedObject.SetActive(true);
                }
                file.Close();

            }
        }
       

        public void Save()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.data");
            PlayerData data = new PlayerData();
            data.Level = level;
            data.score = score;
            data.items = items;
            bf.Serialize(file, data);
            file.Close();
        }


    }

    [Serializable]
    internal class PlayerData
    {
        public int Level;
        public int score;
        public İtemsSo[] items;


    }
}