using System;
using DG.Tweening;
using UnityEngine;

namespace Manager
{
    public class DotsManage : MonoBehaviour
    {
        [SerializeField] private Transform[] platformUp;
        public event EventHandler Onstagepass;
        private void Start()
        {
            GameManager.Instance.dotsManage = this;

            DOTween.Init();
        }
        private void OnDestroy()
        {
            DOTween.KillAll();
        }


        public void PlatformMove()
        {
            platformUp[GameManager.Instance.currentPlatform].transform.DOMoveY(1, 1);
            Onstagepass?.Invoke(this,EventArgs.Empty);
        }


        public void UILeftRİghtEffect(Transform mover)
        {
           
      
            mover.DOLocalMove(new Vector3(500,0,0), 1)
                    .SetLoops(-1, LoopType.Yoyo) // Yoyo döngüsü ile hareketi sürekli yap
                    .SetEase(Ease.InOutSine);
        }

    }
}