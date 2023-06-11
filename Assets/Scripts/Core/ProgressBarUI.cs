#region

using System;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Core
{
    public class ProgressBarUI : MonoBehaviour
    {
        [SerializeField] private GameObject hasProgressGameObject;
        [SerializeField] private Image barImage;

        private IHasProgress _hasProgress;

        private void Start()
        {
            _hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
            if (_hasProgress is null)
            {
                Debug.LogError(
                    $"GameObject {hasProgressGameObject} doesn't have {typeof(IHasProgress)} interface implemented");
            }
            
            _hasProgress!.OnProgressChanged += IHasProgressOnProgressChanged;

            barImage.fillAmount = 0f;

            Hide();
        }

        private void IHasProgressOnProgressChanged(float fillAmountNormalized)
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