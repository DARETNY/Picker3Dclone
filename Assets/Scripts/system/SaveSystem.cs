using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Manager;
using UnityEngine;

namespace system
{
    public class SaveSystem : MonoBehaviour
    {

        public int level = 0;
        public int score = 0;


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
            bf.Serialize(file, data);
            file.Close();
        }


    }

    [Serializable]
    internal class PlayerData
    {
        public int Level;
        public int score;


    }
}