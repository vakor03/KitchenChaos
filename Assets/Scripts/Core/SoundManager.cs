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
        public static SoundManager Instance;
        [SerializeField] private AudioClipRefsSO audioClipRefsSO;

        private void Awake()
        {
            Instance = this;
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

        private void PlaySound(AudioClip clip, Vector3 point, float volume = 1f)
        {
            AudioSource.PlayClipAtPoint(clip, point, volume);
        }

        private void PlaySound(AudioClip[] clipArray, Vector3 point, float volume = 1f)
        {
            PlaySound(clipArray[Random.Range(0, clipArray.Length)], point, volume);
        }

        public void PlayFootstepSounds(Vector3 position)
        {
            PlaySound(audioClipRefsSO.footstep, position);
        }
    }
}