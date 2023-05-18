using System.Collections.Generic;
using System.Linq;
using scritableObject;
using UnityEngine;

namespace Manager
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private List<Level> levels;
        [SerializeField] private Transform platformspawnPoint1, platformspawnPoint2, platformspawnPoint3;
        public static List<int> Platforms = new List<int>();

        private Queue<GameObject> _objectPool = new Queue<GameObject>();
        public GameObject brokencube, brokenSphere, brokenTriangle;

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

            if (GameManager.Instance.gamestate == GameManager.GameState.Empty)
            {
                if (GameManager.Instance.currentLevel % 2 == 0)
                {
                    InitializeObjectPool(brokenSphere.gameObject);

                }
                else if (GameManager.Instance.currentLevel % 3 == 0)
                {
                    InitializeObjectPool(brokencube.gameObject);

                }
                else
                {
                    InitializeObjectPool(brokenTriangle.gameObject);

                }


            }

            //todo: objelerim layerlara gore InitializeObjectPool dan cek gereksiz yere var olmayan objeleri startan hemen yaratma


        }

        #region tester

        public GameObject Getfrompool(Vector3 position, GameObject prefab)
        {
            var key = prefab.name;
            if (_pooler.TryGetValue(key, out var value) && value.Count > 0)
            {
                var obj = value.FirstOrDefault();
                obj.transform.position = position;
                obj.SetActive(true);
                value.Remove(obj);
                return obj;
            }


            return Instantiate(prefab);


        }

        public void ReturntoPool(GameObject obj)
        {
            var key = obj.name;
            if (_pooler.TryGetValue(key, out var value) && value.Contains(obj))
                return;
            obj.SetActive(false);
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

        #endregion


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

        void InitializeObjectPool(GameObject prefab)
        {
            for (int i = 0; i < maxM; i++)
            {
                GameObject obj = Instantiate(prefab, transform, true);
                obj.SetActive(false);
              ReturntoPool(obj);
            }
        }

        public GameObject GetObjectFromPool(GameObject obj, Vector3 position)
        {
            if (_objectPool.Count > 0)
            {
                obj = _objectPool.Dequeue();
                obj.transform.position = position;
                obj.SetActive(true);
                return obj;
            }

            return null;
        }

        public void ReturnObjectToPool(GameObject obj)
        {
            ReturntoPool(obj);
            return;
            if (_objectPool.Contains(obj))
                return;
            obj.SetActive(false);
            obj.transform.position = Vector3.zero;
            _objectPool.Enqueue(obj);
        }
    }
}