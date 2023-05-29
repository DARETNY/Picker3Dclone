using UnityEngine;

namespace Market
{
    [CreateAssetMenu(fileName = "market", menuName = "items", order = 0)]
    public class MarketItems : ScriptableObject
    {
        
        public int price;
        public int itemdID;
    }
}