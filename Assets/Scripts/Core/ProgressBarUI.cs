#region

using System;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Core
{
    public class ProgressBarUI : MonoBehaviour
    {
        [SerializeField] private CuttingCounter cuttingCounter;
        [SerializeField] private Image barImage;

        private void Start()
        {
            cuttingCounter.OnProgressChanged += CuttingCounterOnProgressChanged;

            barImage.fillAmount = 0f;

            Hide();
        }

        private void CuttingCounterOnProgressChanged(float fillAmountNormalized)
        {
            barImage.fillAmount = fillAmountNormalized;

            if (fillAmountNormalized == 0f || Math.Abs(fillAmountNormalized - 1f) < MathHelper.FLOAT_COMPARING_TOLERANCE)
            {
                Hide();
            }
            else
            {
                Show();
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