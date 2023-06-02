using System;
using DG.Tweening;
using UnityEngine;

namespace Manager
{
    public class DotsManage : MonoBehaviour
    {
        [SerializeField] private Transform[] platformUp;
        [SerializeField] private GameObject particaleff;
        [SerializeField] private Transform[] baricades = new Transform[2];
        [SerializeField] private RectTransform moneyTomoney;
        

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

            baricades[GameManager.Instance.currentPlatform].transform.DORotate(Vector3.forward * -90, 1);


            Onstagepass?.Invoke(this, EventArgs.Empty);
            Onstagedone(platformUp[GameManager.Instance.currentPlatform].transform);
        }


        private void Onstagedone(Transform loc)
        {
            var x = Instantiate(particaleff, transform, true);
            x.transform.position += loc.position;
            x.SetActive(true);


        }

        #region UIDotween

        //todo:Ui lar icin dotweenle anımler yapılcak 
        public void Shaker(GameObject shakerGameObject)
        {
            shakerGameObject.transform.DOShakeScale(2, Vector3.one, 3);
        }
        public void MoneyMover(GameObject moverGameObject)
        {
            for (int i = 0; i < moverGameObject.transform.childCount; i++)
            {
                RectTransform pos = moverGameObject.transform.GetChild(i).GetComponent<RectTransform>();

                pos.DOLocalRotate(Vector3.zero, 1);

                pos.DOAnchorPos(moneyTomoney.anchoredPosition, 2);
            }
        }

        #endregion

    }
}