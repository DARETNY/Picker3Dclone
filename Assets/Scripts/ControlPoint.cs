using System;
using System.Collections;
using DG.Tweening;
using Manager;
using UnityEngine;
using UnityEngine.UI;

public class ControlPoint : MonoBehaviour
{
    // eventhandler kullanılarak stagelerin durumu kontrol edilebilir

    [SerializeField] private float _forceAdd = 50;
    private float _waittimer;
    [SerializeField] private float waitterTİmer;
    [SerializeField] private Image stage;
    [SerializeField] private GameObject particalonstagepass;
    public Image nitro;
    

    public event EventHandler OnLevelfaild;

    public static ControlPoint Instance { get; private set; }
    private void Awake()
    {
        stage.GetComponent<Image>().color = new Color();
        Instance = this;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.gamestate = GameManager.GameState.Control;


            StartCoroutine(Wait());

        }

    }
    private void NitroLoad()
    {
        nitro.fillAmount += 1/3f;
    }

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb && other.CompareTag("CubePeaces"))
        {
            if (GameManager.Instance.gamestate == GameManager.GameState.Control)
            {
                rb.AddForce(Vector3.forward * _forceAdd);


            }
            else
            {
                rb.AddForce(Vector3.back * _forceAdd);

            }

        }
    }


    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitterTİmer);
        if (!PlayerController.Instance.IsMoving())
        {
            GameManager.Instance.gamestate = GameManager.GameState.Start;
            BridgeController(GameManager.Instance);

        }

    }
    private void BridgeController(GameManager gameManager)
    {
        if (gameManager.ballCount >= LevelManager.Platforms[gameManager.currentPlatform] &&
            GameManager.Instance.currentPlatform + 1 < LevelManager.Platforms.Count)

        {

            NitroLoad();
            stage.color = Color.green;
            gameManager.ballCount = 0;
            gameManager.dotsManage.PlatformMove();
            PlayerController.Instance.gameObject.transform.DOBlendableScaleBy(new Vector3(.2f, 0, 0), 2);
            gameManager.currentPlatform++;

            gameManager.gamestate = GameManager.GameState.Start;


        }
        else if (gameManager.ballCount >= LevelManager.Platforms[gameManager.currentPlatform] &&
                 gameManager.currentPlatform + 1 >= LevelManager.Platforms.Count)
        {

            NitroLoad();
            stage.color = Color.green;
            gameManager.dotsManage.PlatformMove();
            gameManager.currentLevel++;
            gameManager.saveManager.level = gameManager.currentLevel;

            gameManager.saveManager.Save();

            gameManager.gamestate = GameManager.GameState.Nextlevel;

        }
        else
        {


            stage.color = Color.red;
            gameManager.gamestate = GameManager.GameState.End;
            OnLevelfaild?.Invoke(this, EventArgs.Empty);

        }

    }


}