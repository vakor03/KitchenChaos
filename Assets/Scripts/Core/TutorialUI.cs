using System;
using TMPro;
using UnityEngine;

namespace Core
{
    public class TutorialUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI keyMoveUpText;
        [SerializeField] private TextMeshProUGUI keyMoveDownText;
        [SerializeField] private TextMeshProUGUI keyMoveLeftText;
        [SerializeField] private TextMeshProUGUI keyMoveRightText;
        [SerializeField] private TextMeshProUGUI keyInteractText;
        [SerializeField] private TextMeshProUGUI keyInteractAltText;
        [SerializeField] private TextMeshProUGUI keyPauseText;

        private void Start()
        {
            GameInput.Instance.OnBindingRebind += GameInputOnOnBindingRebind;
            GameManager.Instance.OnStateChanged += GameManagerOnOnStateChanged;
            
            UpdateVisuals();
            
            Show();
        }

        private void GameManagerOnOnStateChanged()
        {
            if (GameManager.Instance.IsCountdownToStartActive)
            {
                Hide();
            }
        }

        private void GameInputOnOnBindingRebind(object sender, EventArgs e)
        {
            UpdateVisuals();
        }

        private void UpdateVisuals()
        {
            keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up); 
            keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
            keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
            keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
            keyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
            keyInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_Alternate);
            keyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
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