using UnityEngine;

namespace Manager
{
    public class ParticalManager : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] partical;
        
        private void Start()
        {
            foreach (ParticleSystem pr in partical)
            {
                pr.gameObject.SetActive(false);
            }
        }
    }
}