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
    [SerializeField] private Button Button;

    private void Start()
    {
        endpoint.SetActive(false);
        virtualCamera.Follow = targetObject;
        virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

    }


    private void StopFollowing()
    {
        virtualCamera.Follow = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            StopFollowing();

            endpoint.SetActive(true);


            Button.onClick.AddListener(() =>
            {
                AddMoneyToBank();

                Bring();


            });

        }
    }
    private  void AddMoneyToBank()
    {

        GameManager.Instance.money += 50;
        GameManager.Instance.saveManager.score = GameManager.Instance.money;
        GameManager.Instance.saveManager.Save();
    }
    private void Bring()
    {

        GameManager.Instance.gamestate = GameManager.GameState.Start;
        SceneManager.LoadScene(sceneBuildIndex: 0);
       
    }
}