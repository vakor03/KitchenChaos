using System;
using TMPro;
using UnityEngine;

namespace Core
{
    public class GameStartCountdownUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countdownText;

        private void Start()
        {
            GameManager.Instance.OnStateChanged += GameManagerOnStateChanged;
            
            Hide();
        }

        private void GameManagerOnStateChanged()
        {
            if (GameManager.Instance.IsCountdownTimerActive)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        private void Update()
        {
            countdownText.text = Mathf.Ceil(GameManager.Instance.GetCountdownToStartTimer()).ToString();
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