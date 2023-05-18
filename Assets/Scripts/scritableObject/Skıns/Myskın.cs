using UnityEngine;

namespace scritableObject.Skıns
{
    [CreateAssetMenu(fileName = "currentskın", menuName = "Skıns", order = 0)]
    public class Myskın : ScriptableObject
    {
        public int price;
        public bool isUnlock;
        public new string name;
        public GameObject gameObject;
    }
}