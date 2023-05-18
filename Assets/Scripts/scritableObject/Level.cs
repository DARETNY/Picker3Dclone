using UnityEngine;

namespace scritableObject
{
    [CreateAssetMenu]
    public class Level : ScriptableObject
    {
        [Range(0, 100)]
        public int platform1, platfrom2, platfrom3;
        public GameObject platfromPrefab1, platfromPrefab2, platfromPrefab3;
    }
}