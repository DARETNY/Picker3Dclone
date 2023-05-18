using Manager;

namespace Typeof
{
    public class BrokenSphare : BaseObstacle
    {
       

       
       protected override void Count(int added)
        {
            for (int i = 0; i < added; i++)
            {
                //LevelManager.Instance.GetObjectFromPool(LevelManager.Instance.brokenSphere, this.transform.position);
                LevelManager.Instance.GetFromPool(transform.position,
                                                  LevelManager.Instance.brokenSphere);
            }
        }
    }
}