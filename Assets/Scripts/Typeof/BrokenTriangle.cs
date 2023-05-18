using Manager;

namespace Typeof
{
    public class BrokenTriangle : BaseObstacle
    {

        override protected void Count(int added)
        {
            for (int i = 0; i < added; i++)
            {
                //LevelManager.Instance.GetObjectFromPool(LevelManager.Instance.brokenTriangle, transform.position);
                LevelManager.Instance.GetFromPool(transform.position,
                                                  LevelManager.Instance.brokenTriangle);
            }
        }
    }
}