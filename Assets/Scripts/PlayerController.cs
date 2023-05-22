using System;
using Manager;
using scritableObject;
using Typeof;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private MagnetType _magnetType;
    [SerializeField] private Rigidbody PlayerRb;
    public event EventHandler OnsizeUpTextOnStop;
    public event EventHandler OnsizeUpTextOnMove;

    private float _speed;
    private bool _ismoving = true;

    private RegionController _regionController;
    public static PlayerController Instance { get; private set; }
    [Range(0, 100)] [SerializeField] private int deticatedDistance = 20;
    private BaseObstacle[] _baseObstacles;

    private void Awake()
    {

        Instance = this;
        _regionController = FindObjectOfType<RegionController>();
        _speed = _magnetType.speed;


    }

    private void FixedUpdate()
    {

        if (GameManager.Instance.gamestate == GameManager.GameState.Start ||
            GameManager.Instance.gamestate == GameManager.GameState.Nextlevel)
        {
            Movment();
            OnsizeUpTextOnMove?.Invoke(this, EventArgs.Empty);
        }
        else if (GameManager.Instance.gamestate == GameManager.GameState.Control)
        {
            Stop();
            if (GameManager.Instance.ballCount >= LevelManager.Platforms[GameManager.Instance.currentPlatform])
            {
                OnsizeUpTextOnStop?.Invoke(this, EventArgs.Empty);


            }

        }
        CheckDistance();
       

    }


    private void Stop()
    {

        PlayerRb.velocity = Vector3.zero;
        PlayerRb.angularVelocity = Vector3.zero;
        _ismoving = false;
    }
    private void Movment()
    {

        PlayerRb.velocity = new Vector3(PlayerRb.velocity.x, PlayerRb.velocity.y, _speed);
        _regionController.MoveBy(PlayerRb);
        GetPosition();

        _ismoving = true;
    }
    public bool IsMoving()
    {

        return _ismoving;
    }

    public float GetPosition()
    {


        return gameObject.transform.position.z;
    }


    #region ObstacleCheck

    //todo:brokenCube ve helicopter classını tek bir yerden calıştır gereksiz update kullanma burdan calıştır

    private void CheckDistance()
    {

        _baseObstacles = FindObjectsOfType<BaseObstacle>();
        foreach (BaseObstacle obstacle in _baseObstacles)
        {
            if (obstacle.SetActive(true))
            {


                if (math.distance(GetPosition(), obstacle.transform.position.z) < deticatedDistance)
                {
                    obstacle.ApplyGravity(true);
                }
                else
                {
                    LevelManager.Instance.brokencube.gameObject.SetActive(false);
                    LevelManager.Instance.brokenSphere.gameObject.SetActive(false);
                    LevelManager.Instance.brokenTriangle.gameObject.SetActive(false);
                }

            }

        }

        #endregion

    }
}