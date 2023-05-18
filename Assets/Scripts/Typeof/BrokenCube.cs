using Manager;

namespace Typeof
{
    public class BrokenCube : BaseObstacle
    {


        override protected void Count(int added)
        {
            for (int i = 0; i < added; i++)
            {
                //LevelManager.Instance.GetObjectFromPool(LevelManager.Instance.brokencube, this.transform.position);
                LevelManager.Instance.Getfrompool(transform.position,
                                                  LevelManager.Instance.brokencube);
            }
        }


    }
}