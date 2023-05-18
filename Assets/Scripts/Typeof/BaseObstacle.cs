using Manager;
using UnityEngine;

namespace Typeof
{
    public abstract class BaseObstacle : MonoBehaviour
    {
        [SerializeField] private bool _createObjectUponFall;

        private Rigidbody _rb;
        private bool IsActive;
        private bool _fallenObjectsCreated;

        protected void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _fallenObjectsCreated = false;
        }
        
        protected virtual void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                //this.gameObject.SetActive(true);
                if (_createObjectUponFall && !_fallenObjectsCreated)
                {
                    _fallenObjectsCreated = true;
                    Count(Mathf.CeilToInt(LevelManager.Instance.maxM / 8));
                    gameObject.SetActive(false);
                }
            }
        }
        protected internal void ApplyGravity(bool enable)
        {
            if (_rb is not null)
                _rb.useGravity = enable;
        }
        public bool SetActive(bool isActive)
        {
            IsActive = isActive;
            gameObject.SetActive(isActive);

            return IsActive;
        }

        protected abstract void Count(int added);


        protected void ResetRigidbody()
        {
            if (_rb is not null)
            {
                _rb.velocity = Vector3.zero;
                _rb.angularVelocity = Vector3.zero;
            }
        }
    }
}