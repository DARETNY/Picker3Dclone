using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Manager.LevelManager;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject dragtostart, gameFaildUI, backGround, sizeUptext, skıns, shopPanel;
        [SerializeField] private Button startButton;
        [SerializeField] private Button retryButton;
        private bool _isToggle;

        [SerializeField] private TextMeshPro[] boardText;
        [SerializeField] private TextMeshProUGUI currentlevel, NextLevel;


        private void Start()
        {
          
            _isToggle = false;
            sizeUptext.SetActive(false);
            shopPanel.SetActive(false);
            if (GameManager.Instance.gamestate == GameManager.GameState.Empty)
            {
              
                skıns.SetActive(true);
                dragtostart.SetActive(true);
                GameManager.Instance.dotsManage.UILeftRİghtEffect(dragtostart.transform);

                gameFaildUI.gameObject.SetActive(false);
                PlayerController.Instance.OnsizeUpTextOnStop += SizeUpTextOnStop;
                PlayerController.Instance.OnsizeUpTextOnMove += InstanceOnOnsizeUpTextOnMove;
            }
           

        }
        private void OnDestroy()
        {
            PlayerController.Instance.OnsizeUpTextOnStop -= SizeUpTextOnStop;
            PlayerController.Instance.OnsizeUpTextOnMove -= InstanceOnOnsizeUpTextOnMove;
        }

        private void InstanceOnOnsizeUpTextOnMove(object sender, EventArgs e)
        {
            sizeUptext.SetActive(false);
        }
        private void SizeUpTextOnStop(object sender, EventArgs e)
        {

            //todo:auido clip ve vfx eklenecek
           this. sizeUptext.SetActive(true);
            sizeUptext.transform.DOBlendableScaleBy(new Vector3(.01f, .01f, .01f), 2);

        }
      

        public void Play()
        {
            skıns.SetActive(false);
            startButton.gameObject.SetActive(false);
            dragtostart.gameObject.SetActive(false);

            GameManager.Instance.gamestate = GameManager.GameState.Start;

        }
        public void Reset()
        {
            GameManager.Instance.gamestate = GameManager.GameState.Empty;
            retryButton.gameObject.SetActive(true);
            SceneManager.LoadSceneAsync(sceneBuildIndex: 0);
        }
        private void LateUpdate()
        {
            CurrentLevelIndex();

            if (GameManager.Instance.gamestate == GameManager.GameState.Control ||
                GameManager.Instance.gamestate == GameManager.GameState.Start)
            {
                //  todo: gold manager yap

                PlatformsTextUI();

            }
            else if (GameManager.Instance.gamestate == GameManager.GameState.End)
            {
                gameFaildUI.SetActive(true);
            }


        }

        public void Quıt()
        {
            Application.Quit();
            Debug.Log("game exit");
        }

        public void Skıns()
        {
            if (_isToggle)
            {
                shopPanel.SetActive(false);
                _isToggle = false;
            }
            else
            {
                shopPanel.SetActive(true);
                _isToggle = true;
            }
        }
        private void PlatformsTextUI()
        {
            boardText[GameManager.Instance.currentPlatform].text =
                    $@"{GameManager.Instance.ballCount}/ {Platforms[GameManager.Instance.currentPlatform]}";
        }


        private void CurrentLevelIndex()
        {
            currentlevel.text = (GameManager.Instance.currentLevel + 1).ToString();
            NextLevel.text = (GameManager.Instance.currentLevel + 2).ToString();

        }


       

    }
}