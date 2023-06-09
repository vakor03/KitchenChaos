using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core
{
    public class GameInput : MonoBehaviour
    {
        private PlayerInputActions _playerInputActions;
        public event EventHandler OnInteractAction;

        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();
            _playerInputActions.Player.Interact.performed += InteractOnStarted;
        }

        private void InteractOnStarted(InputAction.CallbackContext obj)
        {
            OnInteractAction?.Invoke(this, EventArgs.Empty);
        }

        public Vector2 GetMovementVectorNormalized()
        {
            Vector2 inputVectorNormalized = _playerInputActions.Player.Move.ReadValue<Vector2>();

            return inputVectorNormalized;
        }
    }
}