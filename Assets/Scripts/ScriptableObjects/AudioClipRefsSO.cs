#region

using UnityEngine;

#endregion

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class AudioClipRefsSO : ScriptableObject
    {
        public AudioClip[] chop;
        public AudioClip[] deliveryFail;
        public AudioClip[] deliverySuccess;
        public AudioClip[] footstep;
        public AudioClip[] itemDrop;
        public AudioClip[] itemPickUp;
        public AudioClip panSizzleLoop;
        public AudioClip[] trash;
        public AudioClip[] warning;
    }
}