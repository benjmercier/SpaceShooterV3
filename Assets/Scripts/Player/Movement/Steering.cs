using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooterV3.Scripts.Player.Movement
{
    public class Steering : MonoBehaviour
    {
        private Vector3 _targetDirection;

        [SerializeField]
        private float _maxSpeed = 35f;
        [SerializeField, Range(0f, 5f)]
        private float _maxForce = 2f;
        
        private Vector3 _desiredVelocity;
        private Vector3 _steeringVelocity;
        private Vector3 _currentVelocity;

        [SerializeField]
        private Rigidbody _rigidbody;

        private void Start()
        {
            if (_rigidbody == null)
            {
                if (TryGetComponent(out Rigidbody rigidbody))
                {
                    _rigidbody = rigidbody;
                }
                else
                {
                    Debug.Log("Steering::Start()::_rigidbody is NULL.");
                }
            }
        }

        private void OnEnable()
        {
            PlayerInputController.onMoveInput += CalculateSteering;
        }

        private void OnDisable()
        {
            PlayerInputController.onMoveInput -= CalculateSteering;
        }

        private void CalculateSteering(Vector3 direction)
        {
            _targetDirection = transform.position + direction;

            _desiredVelocity = (_targetDirection - transform.position).normalized;
            _desiredVelocity *= _maxSpeed;

            _steeringVelocity = _desiredVelocity - _currentVelocity;
            _steeringVelocity = Vector3.ClampMagnitude(_steeringVelocity, _maxForce);
            _steeringVelocity /= _rigidbody.mass;

            _currentVelocity += _steeringVelocity;
            _currentVelocity = Vector3.ClampMagnitude(_currentVelocity, _maxSpeed);

            transform.Translate(_currentVelocity * Time.deltaTime, Space.World);
        }
    }
}

