using UnityEngine;

namespace Market.items
{
    [CreateAssetMenu(fileName = "item", menuName = "Market İtems", order = 0)]
    public class İtemsSo : ScriptableObject
    {
        public new string name;
        public int price;
        public GameObject prefab;
        public bool isPurchaed;
        public Sprite Image;

    }
}