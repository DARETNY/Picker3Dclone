using Manager;
using UnityEngine;

namespace Typeof
{
    public class BrokenCube : BaseObstacle
    {


        protected override void Count(int added)
        {
            Debug.Log(added);
            for (int i = 0; i < added; i++)
            {
                //LevelManager.Instance.GetObjectFromPool(LevelManager.Instance.brokencube, this.transform.position);
                LevelManager.Instance.GetFromPool(transform.position,
                                                  LevelManager.Instance.brokencube);
            }
        }


    }
}