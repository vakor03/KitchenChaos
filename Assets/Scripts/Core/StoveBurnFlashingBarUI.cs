using System;
using Counters;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Animator))]
    public class StoveBurnFlashingBarUI : MonoBehaviour
    {
        [SerializeField] private StoveCounter stoveCounter;
        private const string IS_FLASHING = "IsFlashing";
        private Animator _animator;
        private static readonly int IsFlashing = Animator.StringToHash(IS_FLASHING);

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            stoveCounter.OnProgressChanged += StoveCounterOnProgressChanged;
        }

        private void StoveCounterOnProgressChanged(float progressNormalized)
        {
            float burnShowProgressAmount = .5f;
            bool show = stoveCounter.IsFried() && progressNormalized >= burnShowProgressAmount;

            _animator.SetBool(IsFlashing, show);
        }
    }
}