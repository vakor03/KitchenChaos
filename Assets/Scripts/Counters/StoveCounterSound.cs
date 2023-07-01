#region

using System;
using Core;
using UnityEngine;

#endregion

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
            stoveCounter.OnProgressChanged += StoveCounterOnProgressChanged;
        }

        private void StoveCounterOnProgressChanged(float progress)
        {
            float burnShowProgressAmount = .5f;
            _playWarningSound = stoveCounter.IsFried() && progress > burnShowProgressAmount;
        }

        private bool _playWarningSound;

        private float _warningSoundTimer;

        private void Update()
        {
            if (_playWarningSound)
            {
                _warningSoundTimer -= Time.deltaTime;
                if (_warningSoundTimer <= 0f)
                {
                    float warningSoundTimerMax = .2f;
                    _warningSoundTimer = warningSoundTimerMax;
                    SoundManager.Instance.PlayWarningSound(transform.position);
                }
            }
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