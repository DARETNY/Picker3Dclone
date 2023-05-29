using System;
using System.Collections;
using UnityEngine;
using Cinemachine;
using Manager;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndPoint : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private GameObject endpoint;
    [SerializeField] private Button collectMoneyButton;
    [SerializeField] private GameObject partical;
    [SerializeField] private GameObject coins;
    
    private bool _isclicked = false;
    public static event EventHandler Onnextlevel;
    private void Start()
    {
        endpoint.SetActive(false);
        coins.SetActive(false);
        virtualCamera.Follow = targetObject;
        virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

    }


    private void StopFollowing()
    {
        virtualCamera.Follow = null;
        virtualCamera.LookAt = null;
        Onnextlevel?.Invoke(this, EventArgs.Empty);
        ParticalPop();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            StopFollowing();

            endpoint.SetActive(true);
            coins.SetActive(true);
            GameManager.Instance.dotsManage.Shaker(endpoint);


            collectMoneyButton.onClick.AddListener(() =>
            {
                _isclicked = true;
                AddMoneyToBank();
                GameManager.Instance.dotsManage.MoneyMover(coins);
                Clicked();


                StartCoroutine(Bring());


            });
           

        }
    }
    private void AddMoneyToBank()
    {

        GameManager.Instance.money += 50;
        GameManager.Instance.saveManager.score = GameManager.Instance.money;
        GameManager.Instance.saveManager.Save();
    }

    private IEnumerator Bring()
    {
        yield return new WaitForSeconds(2);
        GameManager.Instance.gamestate = GameManager.GameState.Start;
        SceneManager.LoadScene(sceneBuildIndex: 0);

    }
    private void ParticalPop()
    {
        var x = Instantiate(partical, transform, true);
        x.transform.position += Camera.main.transform.position + Camera.main.transform.forward * 20;
    
    }
    private void Clicked()
    {
        if (!_isclicked)
        {
            collectMoneyButton.interactable = false;
        }
    }

    
}