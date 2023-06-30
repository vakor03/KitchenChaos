using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class GamePauseUI : MonoBehaviour
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button optionsButton;

        private void Awake()
        {
            resumeButton.onClick.AddListener(() => { GameManager.Instance.ToggleGamePause(); });
            mainMenuButton.onClick.AddListener(() => { Loader.LoadScene(Loader.Scene.MainMenu); });
            optionsButton.onClick.AddListener(() => { OptionsUI.Instance.Show();});
        }

        private void Start()
        {
            GameManager.Instance.OnGamePaused += GameManagerOnGamePaused;
            GameManager.Instance.OnGameUnpaused += GameManagerOnGameUnpaused;
            
            Hide();
        }

        private void GameManagerOnGamePaused()
        {
            Show();
        }

        private void GameManagerOnGameUnpaused()
        {
            Hide();
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }
    }
}