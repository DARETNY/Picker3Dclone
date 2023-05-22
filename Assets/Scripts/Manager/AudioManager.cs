using System;
using scritableObject;
using Typeof;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Manager
{


    public class AudioManager : MonoBehaviour
    {


        [SerializeField] private AudioSo audioClipRef;
        [Range(0, 5)] [SerializeField] private float volume = 5;
        [SerializeField] private bool enable3dsound ;
        private  Vector3 _desiredpos;
        
        
        private void OnEnable()
        {
            // hangi objelerden ses alıcaz eventhandler düzenle
            BaseObstacle.OnAnyObjectTaken += BaseObstacleOnOnAnyObjectTaken;
            Helicopter.OnObjectsSpawn += HelicopterOnOnObjectsSpawn;
            BallCheck.Onobjectsfall += BallCheckOnOnobjectsfall;
            ControlPoint.Instance.OnLevelfaild += InstanceOnOnLevelfaild;
            EndPoint.Onnextlevel += InstanceOnOnnextlevel;
            GameManager.Instance.dotsManage.Onstagepass += InstanceOnOnstagepass;
        }


        private void _Playsound(AudioClip audioclip, Vector3 pos, float vol = 1)
        {
           
            if (enable3dsound)
            {
                _desiredpos = Camera.main!.transform.position;
                AudioSource.PlayClipAtPoint(audioclip, _desiredpos, vol * volume);
            }
            else
            {
                _desiredpos = pos;
                AudioSource.PlayClipAtPoint(audioclip, _desiredpos, vol * volume);
            }


        }
        // ReSharper disable Unity.PerformanceAnalysis
        private void PlaySound(AudioClip[] audioClipsarray, Vector3 pos, float vol = 1)
        {
            _Playsound(audioClipsarray[Random.Range(0, audioClipsarray.Length)], pos, vol);

        }


        #region objectoSound
         
        private void BaseObstacleOnOnAnyObjectTaken(object sender, EventArgs e)
        {
            BaseObstacle obstacles = sender as BaseObstacle;
            PlaySound(audioClipRef.getObjects, obstacles.transform.position);
        }
        private void HelicopterOnOnObjectsSpawn(object sender, EventArgs e)
        {
            Helicopter helicopter = sender as Helicopter;
            PlaySound(audioClipRef.onHelicopterSpawn, helicopter.transform.position);
        }
        private void BallCheckOnOnobjectsfall(object sender, EventArgs e)
        {
            BallCheck ballCheck = sender as BallCheck;
            PlaySound(audioClipRef.onObjectsFall, ballCheck.transform.position);
        }
      
        private void InstanceOnOnLevelfaild(object sender, EventArgs e)
        {
            ControlPoint controlPoint = ControlPoint.Instance;
            PlaySound(audioClipRef.onstagefail, controlPoint.transform.position);
        }
        private void InstanceOnOnstagepass(object sender, EventArgs e)
        {
            ControlPoint controlPoint = ControlPoint.Instance;
            PlaySound(audioClipRef.onstagepass, controlPoint.transform.position);
        }
        private void InstanceOnOnnextlevel(object sender, EventArgs e)
        {
            EndPoint endPoint = sender as EndPoint;

            PlaySound(audioClipRef.onnextlevel, endPoint.transform.position);
        }

        #endregion

    }
}