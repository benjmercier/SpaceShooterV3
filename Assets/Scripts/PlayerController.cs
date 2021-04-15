using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SpaceShooterV3.InputActions;

namespace SpaceShooterV3.Scripts
{
    public class PlayerController : MonoBehaviour, PlayerInputAction.IPlayerActions
    {
        private Vector2 _moveInput;
        private Vector3 _moveDirection;
        private Vector3 _targetDirection;

        // Steering
        [SerializeField]
        private float _maxSpeed = 15f;
        [Range(0f, 5f), SerializeField]
        private float _maxForce = 0.25f;
        

        private Vector3 _currentVelocity;
        private Vector3 _desiredVelocity;
        private Vector3 _steeringVelocity;

        [SerializeField]
        private Rigidbody _rigidbody;

        // Rotation
        [SerializeField]
        private float _bankingSpeed = 5f;
        private float _maxBankingRot = 45f;
        private Quaternion _bankingRotation;

        private void Start()
        {
            if (_rigidbody == null)
            {
                if (TryGetComponent(out Rigidbody rigidbody))
                {
                    _rigidbody = rigidbody;
                }
            }
        }

        private void FixedUpdate()
        {
            CalculateSteering(_moveDirection);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
            
            _moveDirection = new Vector3(_moveInput.x, 0f, _moveInput.y);
        }

        private void CalculateSteering(Vector3 direction)
        {
            _targetDirection = transform.position + direction;

            CalculateBanking(direction.x);

            _desiredVelocity = (_targetDirection - transform.position).normalized;
            _desiredVelocity *= _maxSpeed;

            _steeringVelocity = _desiredVelocity - _currentVelocity;
            _steeringVelocity = Vector3.ClampMagnitude(_steeringVelocity, _maxForce);
            _steeringVelocity /= _rigidbody.mass;

            _currentVelocity += _steeringVelocity;
            _currentVelocity = Vector3.ClampMagnitude(_currentVelocity, _maxSpeed);

            transform.Translate(_currentVelocity * Time.deltaTime, Space.World);
        }

        private void CalculateBanking(float axis)
        {
            if (axis > 0) // bank left
            {
                _bankingRotation = Quaternion.Euler(0f, 0f, -_maxBankingRot);
            }
            else if (axis < 0) // bank right
            {
                _bankingRotation = Quaternion.Euler(0f, 0f, _maxBankingRot);
            }
            else // default
            {
                _bankingRotation = Quaternion.Euler(Vector3.zero);
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, _bankingRotation, _bankingSpeed * Time.deltaTime);
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            
        }
    }
}

