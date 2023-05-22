using System;
using Manager;
using UnityEngine;

namespace Typeof
{
    public abstract class BaseObstacle : MonoBehaviour
    {
        [SerializeField] private bool createObjectUponFall;

        private Rigidbody _rb;
        private bool _ısActive;
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
            _ısActive = isActive;
            gameObject.SetActive(isActive);

            return _ısActive;
        }

        protected abstract void Count(int added);


        
    }
}