using System;
using Manager;
using UnityEngine;

namespace Typeof
{
    public abstract class BaseObstacle : MonoBehaviour
    {
        [SerializeField] private bool createObjectUponFall;

        private Rigidbody _rb;
        private bool IsActive;
        private bool _fallenObjectsCreated;
        public static event EventHandler OnAnyObjectTaken;
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
                if (createObjectUponFall && !_fallenObjectsCreated)
                {
                    OnAnyObjectTaken?.Invoke(this,EventArgs.Empty);
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