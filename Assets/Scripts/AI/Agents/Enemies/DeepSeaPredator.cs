using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooterV3.Scripts.AI.Agents.Enemies
{
    public class DeepSeaPredator : BaseEnemy
    {
        





        /*
        protected void CalculateSteering()
        {
            CalculateFleeing(_targetDetected);

            _desiredVelocity *= _maxSpeed;

            _steeringVelocity = _desiredVelocity - _currentVelocity;
            _steeringVelocity = Vector3.ClampMagnitude(_steeringVelocity, _maxForce);

            _currentVelocity += _steeringVelocity;
            _currentVelocity = Vector3.ClampMagnitude(_currentVelocity, _maxSpeed);

            transform.position += _currentVelocity * Time.deltaTime;
            //transform.forward += _currentVelocity * Time.deltaTime;
        }

        private void CalculateBarrelRoll(float axis)
        {
            if (!_isActive)
            {
                _isActive = true;

                StartCoroutine(BarrelRollRoutine(axis));
            }
        }

        private IEnumerator BarrelRollRoutine(float axis)
        {
            _startEulerAngles = transform.eulerAngles;

            if (axis > 0)
            {
                _endEulerAngleZ = _startEulerAngles.z - 360f; // -360 z = roll left
            }
            else
            {
                _endEulerAngleZ = _startEulerAngles.z + 360f; // +360 z = roll right
            }

            _elapsedTime = 0f;

            while (_elapsedTime < _barrelRollSpeed)
            {
                _elapsedTime += Time.deltaTime;
                _currentAngleZ = Mathf.Lerp(_startEulerAngles.z, _endEulerAngleZ, _elapsedTime / _barrelRollSpeed) % 360f;

                transform.eulerAngles = new Vector3(_startEulerAngles.x, _startEulerAngles.y, _currentAngleZ);

                yield return null;
            }

            _isActive = false;
        }

        private void CalculateFleeing(bool targetDetected)
        {
            if (targetDetected)
            {
                _desiredVelocity = (transform.position - _targetObj.transform.position).normalized;

                CalculateBarrelRoll(-_desiredVelocity.x);
            }
            else
            {
                _targetDirection = transform.position + Vector3.back;// _steeringTarget;

                _desiredVelocity = (_targetDirection - transform.position).normalized;
            }
        }
        */
    }
}

