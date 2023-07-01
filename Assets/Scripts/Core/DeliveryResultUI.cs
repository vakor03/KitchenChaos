using System;
using Counters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    [RequireComponent(typeof(Animator))]
    public class DeliveryResultUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private Image iconImage;
        [SerializeField] private Image backgroundImage;

        [SerializeField] private Sprite successSprite;
        [SerializeField] private Sprite failedSprite;
        [SerializeField] private Color successColor;
        [SerializeField] private Color failedColor;

        private const string POPUP = "Popup";
        private Animator _animator;
        private static readonly int Popup = Animator.StringToHash(POPUP);

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
           
            DeliveryManager.Instance.OnRecipeSuccess += DeliveryManagerOnRecipeSuccess;
            DeliveryManager.Instance.OnRecipeFailed += DeliveryManagerOnRecipeFailed;

            gameObject.SetActive(false);
        }

        private void DeliveryManagerOnRecipeSuccess()
        {
            gameObject.SetActive(true);
            _animator.SetTrigger(Popup);

            messageText.text = "DELIVERY\nSuccess";
            iconImage.sprite = successSprite;
            backgroundImage.color = successColor;
        }

        private void DeliveryManagerOnRecipeFailed()
        {
            gameObject.SetActive(true);
            _animator.SetTrigger(Popup);

            messageText.text = "DELIVERY\nFailed";
            iconImage.sprite = failedSprite;
            backgroundImage.color = failedColor;
        }
        
        
    }
}