using UnityEngine;

namespace Market.items
{
    [CreateAssetMenu(fileName = "item", menuName = "Market İtems", order = 0)]
    public class İtemsSo : ScriptableObject
    {
        public int price;
        public GameObject prefab;
        public int ıtemID;

    }
}