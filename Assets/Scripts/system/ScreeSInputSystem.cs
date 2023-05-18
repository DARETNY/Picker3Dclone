using UnityEngine;

public class ScreeSInputSystem : MonoBehaviour
{
    private float _lastFrameFingerPositionX;
    private float _moveFactorX;

    public float MoveX { get { return _moveFactorX; } }

   public void CatchTouchPos()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastFrameFingerPositionX = Input.mousePosition.x;
        }else if (Input.GetMouseButton(0))
        {
            _moveFactorX = Input.mousePosition.x - _lastFrameFingerPositionX;
            _lastFrameFingerPositionX = Input.mousePosition.x;
        }else if (Input.GetMouseButtonUp(0))
        {
            _moveFactorX = 0;
        }
        else
            _moveFactorX = 0;
    }
}