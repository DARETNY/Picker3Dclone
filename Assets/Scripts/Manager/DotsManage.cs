using DG.Tweening;
using UnityEngine;

namespace Manager
{
    public class DotsManage : MonoBehaviour
    {
        [SerializeField] private Transform[] platformUp;
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
        }


        public void UILeftRİghtEffect(Transform transform)
        {
            transform.DOLocalMove(transform.localPosition + new Vector3(0, 500, 0), 1)
                    .SetLoops(-1, LoopType.Yoyo) // Yoyo döngüsü ile hareketi sürekli yap
                    .SetEase(Ease.InOutSine);
        }

    }
}