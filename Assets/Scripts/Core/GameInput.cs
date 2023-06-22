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

        private void Awake()
        {
            Instance = this;
            
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();
            _playerInputActions.Player.Interact.performed += InteractOnPerformed;
            _playerInputActions.Player.InteractAlternate.performed += InteractAlternateOnPerformed;
            _playerInputActions.Player.Pause.performed += PauseOnPerformed;
        }

        private void PauseOnPerformed(InputAction.CallbackContext obj)
        {
            OnPauseToggled?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler OnInteractAction;

        public event EventHandler OnAlternateInteractAction;
        public event EventHandler OnPauseToggled;

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
    }
}