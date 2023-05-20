using System;
using scritableObject;
using Typeof;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Manager
{


    public class AudioManager : MonoBehaviour
    {


        public AudioManager Instance { get; private set; }
        [SerializeField] private AudioSo audioClipRef;
        [Range(0, 1)] private float _volume = 1;

        private void Awake()
        {
            Instance = this;


        }
        private void Start()
        {
            // hangi objelerden ses alıcaz eventhandler düzenle
            BaseObstacle.OnAnyObjectTaken += BaseObstacleOnOnAnyObjectTaken;
            Helicopter.OnObjectsSpawn += HelicopterOnOnObjectsSpawn;
        }
       
        private void _Playsound(AudioClip audioclip, Vector3 pos, float vol = 1)
        {
            AudioSource.PlayClipAtPoint(audioclip, pos, vol * _volume);
        }
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

        #endregion

    }
}