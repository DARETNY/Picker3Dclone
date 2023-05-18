using Manager;
using Unity.Mathematics;
using UnityEngine;

public class ObjectsArray : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        Vector3 pos = gameObject.transform.position;
        pos.x = Mathf.Clamp(pos.x, -12, 12);
        gameObject.transform.position = pos;
        if (math.distance(PlayerController.Instance.GetPosition(), gameObject.transform.position.z) > 100)
        {
            gameObject.SetActive(false);

        }


    }
    private void OnDisable()
    {
        LevelManager.Instance.ReturnObjectToPool(this.gameObject);
    }
}