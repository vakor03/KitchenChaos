using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class OptionsUI : MonoBehaviour
    {
        public static OptionsUI Instance { get; private set; }
        [SerializeField] private Button soundEffectsButton;
        [SerializeField] private Button musicButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private TextMeshProUGUI soundEffectsText;
        [SerializeField] private TextMeshProUGUI musicEffectsText;

        [Header("Buttons")] [SerializeField] private Button moveUpButton;
        [SerializeField] private Button moveDownButton;
        [SerializeField] private Button moveLeftButton;
        [SerializeField] private Button moveRightButton;
        [SerializeField] private Button interactButton;
        [SerializeField] private Button interactAlternateButton;
        [SerializeField] private Button pauseButton;

        [Header("Buttons Text")] [SerializeField]
        private TextMeshProUGUI moveUpButtonText;

        [SerializeField] private TextMeshProUGUI moveDownButtonText;
        [SerializeField] private TextMeshProUGUI moveLeftButtonText;
        [SerializeField] private TextMeshProUGUI moveRightButtonText;
        [SerializeField] private TextMeshProUGUI interactButtonText;
        [SerializeField] private TextMeshProUGUI interactAlternateButtonText;
        [SerializeField] private TextMeshProUGUI pauseButtonText;

        private void Awake()
        {
            Instance = this;

            soundEffectsButton.onClick.AddListener(() =>
            {
                SoundManager.Instance.ChangeVolume();
                UpdateVisuals();
            });
            musicButton.onClick.AddListener(() =>
            {
                MusicManager.Instance.ChangeVolume();
                UpdateVisuals();
            });
            closeButton.onClick.AddListener(Hide);

            moveUpButton.onClick.AddListener(() =>
            {
                GameInput.Instance.RebindKey(GameInput.Binding.Move_Up);
                UpdateVisuals();
            });
            moveDownButton.onClick.AddListener(() => { GameInput.Instance.RebindKey(GameInput.Binding.Move_Down); });
            moveLeftButton.onClick.AddListener(() => { GameInput.Instance.RebindKey(GameInput.Binding.Move_Left); });
            moveRightButton.onClick.AddListener(() => { GameInput.Instance.RebindKey(GameInput.Binding.Move_Right); });
            interactButton.onClick.AddListener(() => { GameInput.Instance.RebindKey(GameInput.Binding.Interact); });
            interactAlternateButton.onClick.AddListener(() =>
            {
                GameInput.Instance.RebindKey(GameInput.Binding.Interact_Alternate);
            });
            pauseButton.onClick.AddListener(() => { GameInput.Instance.RebindKey(GameInput.Binding.Pause); });
        }

        private void Start()
        {
            UpdateVisuals();

            GameManager.Instance.OnGameUnpaused += GameManagerOnGameUnpaused;

            Hide();
        }

        private void GameManagerOnGameUnpaused()
        {
            Hide();
        }

        private void UpdateVisuals()
        {
            soundEffectsText.text = "Sound Effects : " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
            musicEffectsText.text = "Music : " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
            moveUpButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
            moveDownButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
            moveLeftButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
            moveRightButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
            interactButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
            interactAlternateButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_Alternate);
            pauseButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}