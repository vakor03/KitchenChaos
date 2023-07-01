#region

using Counters;
using PlayerLogic;
using ScriptableObjects;
using UnityEngine;

#endregion

namespace Core
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }
        [SerializeField] private AudioClipRefsSO audioClipRefsSO;

        private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
        private void Awake()
        {
            Instance = this;

            _currentVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
        }

        private void Start()
        {
            DeliveryManager.Instance.OnRecipeSuccess += DeliveryManagerOnRecipeSuccess;
            DeliveryManager.Instance.OnRecipeFailed += DeliveryManagerOnRecipeFailed;
            CuttingCounter.OnAnyCut += CuttingCounterOnAnyCut;
            TrashCounter.OnAnyTrash += TrashCounterOnAnyTrash;
            Player.Instance!.OnPickedSomething += PlayerOnPickedSomething;
            BaseCounter.OnAnyObjectPlacedHere += BaseCounterOnAnyObjectPlacedHere;
        }

        private void BaseCounterOnAnyObjectPlacedHere(BaseCounter obj)
        {
            PlaySound(audioClipRefsSO.itemDrop, obj.transform.position);
        }

        private void PlayerOnPickedSomething()
        {
            PlaySound(audioClipRefsSO.itemPickUp, Player.Instance!.transform.position);
        }

        private void TrashCounterOnAnyTrash(TrashCounter trashCounter)
        {
            PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
        }

        private void CuttingCounterOnAnyCut(CuttingCounter cuttingCounter)
        {
            PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
        }

        private void DeliveryManagerOnRecipeFailed()
        {
            DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
            PlaySound(audioClipRefsSO.deliveryFail, deliveryCounter.transform.position);
        }

        private void DeliveryManagerOnRecipeSuccess()
        {
            DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
            PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
        }

        private void PlaySound(AudioClip clip, Vector3 point, float volumeMultiplier = 1f)
        {
            AudioSource.PlayClipAtPoint(clip, point, volumeMultiplier * _currentVolume);
        }

        private void PlaySound(AudioClip[] clipArray, Vector3 point, float volumeMultiplier = 1f)
        {
            PlaySound(clipArray[Random.Range(0, clipArray.Length)], point, volumeMultiplier);
        }

        public void PlayFootstepSounds(Vector3 position)
        {
            PlaySound(audioClipRefsSO.footstep, position);
        }

        public void PlayCountdownSound()
        {
            PlaySound(audioClipRefsSO.warning[1], Vector3.zero);
        }

        public void PlayWarningSound(Vector3 position)
        {
            PlaySound(audioClipRefsSO.warning[1], position);
        }

        private float _currentVolume = 1f;

        public void ChangeVolume()
        {
            _currentVolume += 0.1f;
            if (_currentVolume>1f)
            {
                _currentVolume = 0f;
            }
            
            PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, _currentVolume);
            PlayerPrefs.Save(); // Do not really need it
        }

        public float GetVolume()
        {
            return _currentVolume;
        }
    }
}