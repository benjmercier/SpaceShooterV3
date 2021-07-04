using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SpaceShooterV3.InputActions;

namespace SpaceShooterV3.Scripts.Controllers
{
    public class PlayerInputController : MonoBehaviour, PlayerInputAction.IPlayerActions
    {
        private Vector2 _moveInput;
        private Vector3 _inputDirection;

        // Events
        public static Action<Vector3> onMoveInput;
        public static Action<float> onBarrelRollInput;
        public static Action onFireInput;

        private void Start()
        {
            
        }

        private void FixedUpdate()
        {
            OnMoveInput(_inputDirection);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
            
            _inputDirection = new Vector3(_moveInput.x, 0f, _moveInput.y);
        }

        private void OnMoveInput(Vector3 direction)
        {
            onMoveInput?.Invoke(direction);
        }

        public void OnBarrelRoll(InputAction.CallbackContext context)
        {
            if (context.performed && _inputDirection.x != 0)
            {
                OnBarrelRollInput(_inputDirection.x);
            }
        }

        private void OnBarrelRollInput(float axis)
        {
            onBarrelRollInput?.Invoke(axis);
        }

        public void OnLook(InputAction.CallbackContext context)
        {

        }

        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnFireInput();
            }
        }

        private void OnFireInput()
        {
            onFireInput?.Invoke();
        }
    }
}

