using UnityEngine;

namespace scritableObject
{
    [CreateAssetMenu(fileName = "sounds", menuName = "AudioSo", order = 0)]
    public class AudioSo : ScriptableObject
    {
        public AudioClip[] getObjects;
        public AudioClip[] onHelicopterSpawn;
        public AudioClip[] onObjectsFall;
        public AudioClip[] onstagepass;
        public AudioClip[] onstagefail;
        public AudioClip[] onnextlevel;
        

    }
}