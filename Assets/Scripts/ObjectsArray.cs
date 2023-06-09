using System.Linq;
using Manager;
using Unity.Mathematics;
using UnityEngine;

public class ObjectsArray : MonoBehaviour
{
    [SerializeField] private PoolObjectType _poolObjectType;
    [SerializeField] protected Color objectColor;
    [SerializeField] protected bool randomColor;
  
    public PoolObjectType PoolObjectType => _poolObjectType;
    
    
    private MeshRenderer _meshRenderer;
    private Rigidbody _rb;
   
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    private void OnEnable()
    {
        PaintObject();
        ResetRb();
        _rb.constraints = RigidbodyConstraints.None;
        _rb.isKinematic = false;
    }

    private void OnDisable()
    {
        _rb.constraints = RigidbodyConstraints.FreezeAll;
        _rb.isKinematic = true;
        ResetRb();
    }

    private void ResetRb()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

    public Color GetObjectColor()
    {
        if (randomColor)
            return UnityEngine.Random.ColorHSV();
        else
            return objectColor;
    }

    private void FixedUpdate()
    {
        Vector3 pos = gameObject.transform.position;
        pos.x = Mathf.Clamp(pos.x, -12, 12);
        gameObject.transform.position = pos;
        if (math.distance(PlayerController.Instance.GetPosition(), gameObject.transform.position.z) > 100)
        {
            SendToPool();
        }
       
    }

    private void PaintObject()
    {
        _meshRenderer.materials.FirstOrDefault().color = GetObjectColor();
    }

    private void SendToPool()
    {
        LevelManager.Instance.ReturnObjectToPool(this.gameObject, _poolObjectType);
    }
}