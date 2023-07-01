#region

using System;
using TMPro;
using UnityEngine;

#endregion

namespace Core
{
    [RequireComponent(typeof(Animator))]
    public class GameStartCountdownUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countdownText;

        private Animator _animator;
        private int _previousCountdownNumber;
        private static readonly int NumberPopup = Animator.StringToHash(NUMBER_POPUP);
        private const string NUMBER_POPUP = "NumberPopup";


        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            GameManager.Instance.OnStateChanged += GameManagerOnStateChanged;

            Hide();
        }

        private void Update()
        {
            if (!GameManager.Instance.IsCountdownToStartActive)
            {
                return;
            }
            int countdownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountdownToStartTimer());
            countdownText.text = countdownNumber.ToString();
            if (countdownNumber != _previousCountdownNumber)
            {
                _animator.SetTrigger(NumberPopup);
                SoundManager.Instance.PlayCountdownSound();
                _previousCountdownNumber = countdownNumber;
            }
        }

        private void GameManagerOnStateChanged()
        {
            if (GameManager.Instance.IsCountdownToStartActive)
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
            countdownText.gameObject.SetActive(true);
        }

        private void Hide()
        {
            countdownText.gameObject.SetActive(false);
        }
    }
}