using System.Collections;
using Manager;
using UnityEngine;

public class BallCheck : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CubePeaces"))
        {
            GameObject fallenobjects = other.gameObject;
           
            if (fallenobjects != null && fallenobjects.TryGetComponent<ObjectsArray>(out var objectsArray))
            {
                StartCoroutine(WaitAndSpawn(fallenobjects, objectsArray.PoolObjectType));
               
            }
            GameManager.Instance.ballCount++;
            //todo:test amaclı olarak yapıldı daha sonra ayarlanıcak
        }
    }

    private IEnumerator WaitAndSpawn(GameObject fallenobjects, PoolObjectType poolObjectType)
    {
        yield return new WaitForSeconds(2f);

         // PoolManager.Instance.GetObjectsToPool("cubes", fallenobjects);
        LevelManager.Instance.ReturnObjectToPool(fallenobjects, poolObjectType);


    }


}