using System;
using UnityEngine;

public class RegionController : ScreeSInputSystem
{
    [SerializeField] private float _swerveSpeed;
    [SerializeField] private float _maxSwerveAmount;
    [SerializeField] private float _maxMove_x;
    private Rigidbody playerrb;

    public void MoveBy(Rigidbody rigidbody)
    {
        playerrb = rigidbody;
        CatchTouchPos();
        float deflectAmount = _swerveSpeed * Time.deltaTime * MoveX;
        deflectAmount = Math.Clamp(deflectAmount, -_maxSwerveAmount, _maxSwerveAmount);
        deflectAmount = EdgeCheck(deflectAmount);
        playerrb.velocity = new Vector3(deflectAmount, playerrb.velocity.y, playerrb.velocity.z);
    }

    public float EdgeCheck(float amount)
    {
        amount = LeftLimiter(amount);
        amount = RightLimiter(amount);
        return amount;
    }

    public float LeftLimiter(float amount)
    {
        float xPos = playerrb.transform.position.x;
        if (xPos <= -(_maxMove_x))
        {
            playerrb.velocity = new Vector3(0, playerrb.velocity.y, playerrb.velocity.z);
            amount = Math.Clamp(amount, .1f, _maxSwerveAmount);
        }
        return amount;
    }

    float RightLimiter(float amount)
    {
        float xPos = playerrb.transform.position.x;
        if (xPos >= _maxMove_x)
        {
            playerrb.velocity = new Vector3(0, playerrb.velocity.y, playerrb.velocity.z);
            amount = Mathf.Clamp(amount, -(_maxSwerveAmount), -.1f);
        }

        return amount;
    }
}