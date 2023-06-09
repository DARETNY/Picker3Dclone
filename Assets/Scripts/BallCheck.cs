using System;
using System.Collections;
using Manager;
using UnityEngine;

public class BallCheck : MonoBehaviour
{
    public static event EventHandler Onobjectsfall;
    [SerializeField] private ParticleSystem particaleffect;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CubePeaces"))
        {
            GameObject fallenobjects = other.gameObject;
            
            Onobjectsfall?.Invoke(this,EventArgs.Empty);

            particaleffect.Play();
            if (fallenobjects != null && fallenobjects.TryGetComponent<ObjectsArray>(out var objectsArray))
            {
                StartCoroutine(WaitAndSpawn(fallenobjects, objectsArray.PoolObjectType));
               
            }
            GameManager.Instance.ballCount++;
           
        }
    }

    private IEnumerator WaitAndSpawn(GameObject fallenobjects, PoolObjectType poolObjectType)
    {
        yield return new WaitForSeconds(2f);
        

         // PoolManager.Instance.GetObjectsToPool("cubes", fallenobjects);
        LevelManager.Instance.ReturnObjectToPool(fallenobjects, poolObjectType);


    }


}