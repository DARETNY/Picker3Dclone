using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public SaveSystem saveManager;

        [HideInInspector] public GameState gamestate;
        [HideInInspector] public int ballCount;
        [HideInInspector] public int currentPlatform;
        [HideInInspector] public int currentLevel,money;

        public DotsManage dotsManage;
        private void Awake()
        {
            Instance = this;
            gamestate = GameState.Empty;
            ballCount = 0;
            currentLevel = 0;
            currentPlatform = 0;

        }


        public enum GameState
        {
            Empty,
            Start,
            Control,
            Nextlevel,
            End
        }


    }
}