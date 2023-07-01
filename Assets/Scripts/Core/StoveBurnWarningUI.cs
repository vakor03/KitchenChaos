using System;
using Counters;
using TMPro;
using UnityEngine;

namespace Core
{
    public class StoveBurnWarningUI : MonoBehaviour
    {
        [SerializeField] private StoveCounter stoveCounter;

        private void Start()
        {
            stoveCounter.OnProgressChanged += StoveCounterOnProgressChanged;
            
            Hide();
        }

        private void StoveCounterOnProgressChanged(float progressNormalized)
        {
            float burnShowProgressAmount = .5f;
            bool show = stoveCounter.IsFried() && progressNormalized >= burnShowProgressAmount;

            if (show)
            {
                Show(); 
            }
            else
            {
                Hide();
            }
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}