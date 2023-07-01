#region

using System;
using UnityEngine;
using UnityEngine.InputSystem;

#endregion

namespace Core
{
    public class GameInput : MonoBehaviour
    {
        public static GameInput Instance { get; private set; }
        private PlayerInputActions _playerInputActions;

        private const string PLAYER_PREFS_BINDINGS = "Bindings";

        public enum Binding
        {
            Move_Up,
            Move_Down,
            Move_Left,
            Move_Right,
            Interact,
            Interact_Alternate,
            Pause
        }

        private void Awake()
        {
            Instance = this;

            _playerInputActions = new PlayerInputActions();
            if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
            {
                _playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
            }

            _playerInputActions.Player.Enable();
            _playerInputActions.Player.Interact.performed += InteractOnPerformed;
            _playerInputActions.Player.InteractAlternate.performed += InteractAlternateOnPerformed;
            _playerInputActions.Player.Pause.performed += PauseOnPerformed;
        }

        private void OnDestroy()
        {
            _playerInputActions.Player.Interact.performed -= InteractOnPerformed;
            _playerInputActions.Player.InteractAlternate.performed -= InteractAlternateOnPerformed;
            _playerInputActions.Player.Pause.performed -= PauseOnPerformed;
            _playerInputActions.Dispose();
        }

        private void PauseOnPerformed(InputAction.CallbackContext obj)
        {
            OnPauseToggled?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler OnInteractAction;

        public event EventHandler OnAlternateInteractAction;
        public event EventHandler OnPauseToggled;
        public event EventHandler OnBindingRebind;

        public Vector2 GetMovementVectorNormalized()
        {
            Vector2 inputVectorNormalized = _playerInputActions.Player.Move.ReadValue<Vector2>();

            return inputVectorNormalized;
        }

        private void InteractOnPerformed(InputAction.CallbackContext obj)
        {
            OnInteractAction?.Invoke(this, EventArgs.Empty);
        }

        private void InteractAlternateOnPerformed(InputAction.CallbackContext obj)
        {
            OnAlternateInteractAction?.Invoke(this, EventArgs.Empty);
        }

        public string GetBindingText(Binding binding)
        {
            switch (binding)
            {
                case Binding.Move_Up:
                    return _playerInputActions.Player.Move.bindings[1].ToDisplayString();
                case Binding.Move_Down:
                    return _playerInputActions.Player.Move.bindings[2].ToDisplayString();
                case Binding.Move_Left:
                    return _playerInputActions.Player.Move.bindings[3].ToDisplayString();
                case Binding.Move_Right:
                    return _playerInputActions.Player.Move.bindings[4].ToDisplayString();
                case Binding.Interact:
                    return _playerInputActions.Player.Interact.bindings[0].ToDisplayString();
                case Binding.Interact_Alternate:
                    return _playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();
                case Binding.Pause:
                    return _playerInputActions.Player.Pause.bindings[0].ToDisplayString();
                default:
                    throw new ArgumentOutOfRangeException(nameof(binding), binding, null);
            }
        }

        public void RebindKey(Binding binding, Action onActionRebound)
        {
            _playerInputActions.Player.Disable();

            InputAction inputAction;
            int bindingIndex;

            switch (binding)
            {
                case Binding.Move_Up:
                    inputAction = _playerInputActions.Player.Move;
                    bindingIndex = 1;
                    break;
                case Binding.Move_Down:
                    inputAction = _playerInputActions.Player.Move;
                    bindingIndex = 2;
                    break;
                case Binding.Move_Left:
                    inputAction = _playerInputActions.Player.Move;
                    bindingIndex = 3;
                    break;
                case Binding.Move_Right:
                    inputAction = _playerInputActions.Player.Move;
                    bindingIndex = 4;
                    break;
                case Binding.Interact:
                    inputAction = _playerInputActions.Player.Interact;
                    bindingIndex = 0;
                    break;
                case Binding.Interact_Alternate:
                    inputAction = _playerInputActions.Player.InteractAlternate;
                    bindingIndex = 0;
                    break;
                case Binding.Pause:
                    inputAction = _playerInputActions.Player.Pause;
                    bindingIndex = 0;
                    break;
                default:
                    throw new ArgumentException();
            }

            inputAction.PerformInteractiveRebinding(bindingIndex)
                .OnComplete(callback =>
                {
                    callback.Dispose();
                    _playerInputActions.Player.Enable();
                    onActionRebound();

                    PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, _playerInputActions.SaveBindingOverridesAsJson());
                    PlayerPrefs.Save();
                }).Start();
            
            OnBindingRebind?.Invoke(this, EventArgs.Empty);
        }
    }
}