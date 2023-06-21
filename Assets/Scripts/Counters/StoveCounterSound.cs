using System;
using UnityEngine;

namespace Counters
{
    [RequireComponent(typeof(AudioSource))]
    public class StoveCounterSound : MonoBehaviour
    {
        [SerializeField] private StoveCounter stoveCounter;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            stoveCounter.OnStateChanged += StoveCounterOnStateChanged;
        }

        private void StoveCounterOnStateChanged(StoveCounter.State state)
        {
            if (state == StoveCounter.State.Frying || state == StoveCounter.State.Fried)
            {
                _audioSource.Play();
            }
            else
            {
                _audioSource.Pause();
            }
        }
    }
}