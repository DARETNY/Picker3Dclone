using Manager;
using Unity.Mathematics;
using UnityEngine;

namespace Typeof
{
    public class Helicopter : MonoBehaviour
    {
        private int _objectCount = 0;
        
        private void FixedUpdate()
        {
            CheckDistance();
        }
        private void CheckDistance()
        {
            if (math.distance(PlayerController.Instance.GetPosition(), this.gameObject.transform.position.z) < 10)
            {
              
               
                CountofObjects();
                Makechase();

            }
            if (_objectCount >= LevelManager.Instance.maxM)
            {
                gameObject.SetActive(false);
            }

        }
        private void CountofObjects()
        {

            //LevelManager.Instance.GetObjectFromPool(LevelManager.Instance.brokenSphere, this.transform.position);
            LevelManager.Instance.GetFromPool(transform.position,
                                              LevelManager.Instance.brokenSphere);
            _objectCount++;


        }

        private void Makechase()
        {
            float xPosition = Mathf.Sin(Time.time * 2) * 3;
            xPosition = Mathf.Lerp(-12, 12, (xPosition + 12) / (12 * 2));
            Vector3 newPosition = new Vector3(xPosition, transform.position.y, transform.position.z + 1f);
            transform.position = newPosition;

        }
    }
}