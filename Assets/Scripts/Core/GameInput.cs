#region

using System;
using UnityEngine;
using UnityEngine.InputSystem;

#endregion

namespace Core
{
    public class GameInput : MonoBehaviour
    {
        private PlayerInputActions _playerInputActions;

        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();
            _playerInputActions.Player.Interact.performed += InteractOnPerformed;
            _playerInputActions.Player.InteractAlternate.performed += InteractAlternateOnPerformed;
        }

        public event EventHandler OnInteractAction;

        public event EventHandler OnAlternateInteractAction;

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