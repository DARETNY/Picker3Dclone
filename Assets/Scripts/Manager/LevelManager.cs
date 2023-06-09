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
        [SerializeField] private Renderer[] planes;

        public ObjectsArray brokencube, brokenSphere, brokenTriangle;

        public static LevelManager Instance { get; private set; }
        [HideInInspector] public int maxM;
        private Dictionary<string, List<GameObject>> _pooler = new();

        private void Awake()
        {
            Instance = this;

        }

        private void Start()
        {
            GameManager.Instance.ballCount = 0;
            GameManager.Instance.currentPlatform = 0;
            Platforms.Clear();
            AddStage(GameManager.Instance.currentLevel % levels.Count);
            maxM = Platforms.Max();


            foreach (Renderer matcolor in planes)
            {


                if (GameManager.Instance.gamestate == GameManager.GameState.Empty)
                {
                    if (GameManager.Instance.currentLevel % 5 == 0)
                    {
                        InitializeObjectPool(brokenSphere.gameObject, PoolObjectType.Sphere);
                        matcolor!.material.color = Color.green;


                    }
                    if (GameManager.Instance.currentLevel % 3 == 0)
                    {
                        InitializeObjectPool(brokencube.gameObject, PoolObjectType.Cube);
                        matcolor!.material.color = Color.red;
                    }
                    else
                    {
                        InitializeObjectPool(brokenTriangle.gameObject, PoolObjectType.Triangle);
                        matcolor!.material.color = Color.blue;
                    }


                }

            }

            //todo: objelerim layerlara gore InitializeObjectPool dan cek gereksiz yere var olmayan objeleri startan hemen yaratma


        }

        public GameObject GetFromPool(Vector3 position, ObjectsArray prefab)
        {
            var key = prefab.PoolObjectType.ToString();
            prefab.GetObjectColor();

            if (_pooler.TryGetValue(key, out var value) && value.Count > 0)
            {


                var obj = value.FirstOrDefault();
                obj.transform.position = position;
                obj.SetActive(true);
                value.Remove(obj);
                return obj;
            }


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

        private void AddStage(int thisLevel)
        {
            ObjectSpawner(thisLevel);
            Platforms.Add(levels[thisLevel].platform1);
            Platforms.Add(levels[thisLevel].platfrom2);
            Platforms.Add(levels[thisLevel].platfrom3);
        }

        private void ObjectSpawner(int level)
        {
            Instantiate(levels[level].platfromPrefab1, platformspawnPoint1.transform);
            Instantiate(levels[level].platfromPrefab2, platformspawnPoint2.transform);
            Instantiate(levels[level].platfromPrefab3, platformspawnPoint3.transform);
        }

        private void InitializeObjectPool(GameObject prefab, PoolObjectType poolObjectType)
        {
            for (int i = 0; i < maxM; i++)
            {
                GameObject obj = Instantiate(prefab, transform, true);
                ReturnObjectToPool(obj, poolObjectType);
            }
        }
    }
}