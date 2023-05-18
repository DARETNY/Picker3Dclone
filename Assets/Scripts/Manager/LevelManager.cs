using System.Collections.Generic;
using System.Linq;
using scritableObject;
using UnityEngine;

namespace Manager
{
    public enum PoolObjectType
    {
        Cube = 1,
        Triangle = 2,
        Sphere = 3,
    }
    
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private List<Level> levels;
        [SerializeField] private Transform platformspawnPoint1, platformspawnPoint2, platformspawnPoint3;
        public static List<int> Platforms = new List<int>();

        public ObjectsArray brokencube, brokenSphere, brokenTriangle;

        public static LevelManager Instance { get; private set; }
        [HideInInspector] public int maxM;
        private Dictionary<string, List<GameObject>> _pooler = new();

        private void Awake()
        {
            Instance = this;
            //fsadfsadfdsfsdfas
            
            
            // Erdem hehehe
        }

        private void Start()
        {
            GameManager.Instance.ballCount = 0;
            GameManager.Instance.currentPlatform = 0;
            Platforms.Clear();
            AddStage(GameManager.Instance.currentLevel % levels.Count);
            maxM = Platforms.Max();

            if (GameManager.Instance.gamestate == GameManager.GameState.Empty)
            {
                if (GameManager.Instance.currentLevel % 2 == 0)
                {
                    InitializeObjectPool(brokenSphere.gameObject, PoolObjectType.Sphere);

                }
                else if (GameManager.Instance.currentLevel % 3 == 0)
                {
                    InitializeObjectPool(brokencube.gameObject, PoolObjectType.Cube);

                }
                else
                {
                    InitializeObjectPool(brokenTriangle.gameObject, PoolObjectType.Triangle);

                }


            }

            //todo: objelerim layerlara gore InitializeObjectPool dan cek gereksiz yere var olmayan objeleri startan hemen yaratma


        }

        public GameObject GetFromPool(Vector3 position, ObjectsArray prefab)
        {
            var key = prefab.PoolObjectType.ToString();
            var color = prefab.GetObjectColor();
            
            if (_pooler.TryGetValue(key, out var value) && value.Count > 0)
            {
                Debug.Log($"Spawned: {key}");
                
                var obj = value.FirstOrDefault();
                obj.transform.position = position;
                obj.SetActive(true);
                value.Remove(obj);

                return obj;
            }


            
            Debug.Log($"Instantiated: {key}");
            
            var instantiatedObj = Instantiate(prefab.gameObject, position, Quaternion.identity);
            instantiatedObj.SetActive(true);
            
            return instantiatedObj;
        }

        public void ReturnObjectToPool(GameObject obj, PoolObjectType poolObjectType)
        {
            var key = poolObjectType.ToString();
            
            obj.SetActive(false);
            
            if (_pooler.TryGetValue(key, out var value) && value.Contains(obj))
                return;
            
            Debug.Log($"Despawned: {key}");

            
            obj.transform.position = Vector3.zero;
            if (value is not null)
            {
                value.Add(obj);
            }
            else
            {
                var poolist = new List<GameObject>();
                poolist.Add(obj);
                _pooler.Add(key, poolist);
            }

        }

        void AddStage(int thisLevel)
        {
            ObjectSpawner(thisLevel);
            Platforms.Add(levels[thisLevel].platform1);
            Platforms.Add(levels[thisLevel].platfrom2);
            Platforms.Add(levels[thisLevel].platfrom3);
        }

        void ObjectSpawner(int level)
        {
            Instantiate(levels[level].platfromPrefab1, platformspawnPoint1.transform);
            Instantiate(levels[level].platfromPrefab2, platformspawnPoint2.transform);
            Instantiate(levels[level].platfromPrefab3, platformspawnPoint3.transform);
        }

        void InitializeObjectPool(GameObject prefab, PoolObjectType poolObjectType)
        {
            for (int i = 0; i < maxM; i++)
            {
                GameObject obj = Instantiate(prefab, transform, true);
                ReturnObjectToPool(obj, poolObjectType);
            }
        }
    }
}