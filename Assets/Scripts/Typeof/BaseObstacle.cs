using Manager;
using UnityEngine;

namespace Typeof
{
    public abstract class BaseObstacle : MonoBehaviour
    {
        private Rigidbody _rb;
        private bool IsActive;

        protected void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        protected virtual void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                //this.gameObject.SetActive(true);
                Count(Mathf.CeilToInt(LevelManager.Instance.maxM / 8));
                
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