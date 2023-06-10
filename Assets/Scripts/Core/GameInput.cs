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
            _playerInputActions.Player.Interact.performed += InteractOnStarted;
        }

        public event EventHandler OnInteractAction;

        public Vector2 GetMovementVectorNormalized()
        {
            Vector2 inputVectorNormalized = _playerInputActions.Player.Move.ReadValue<Vector2>();

            return inputVectorNormalized;
        }

        private void InteractOnStarted(InputAction.CallbackContext obj)
        {
            OnInteractAction?.Invoke(this, EventArgs.Empty);
        }
    }
}