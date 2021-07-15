using System.Collections;
using UnityEngine;

namespace SpaceShooterV3.Scripts.Player.Movement
{
    public class BarrelRoll : MonoBehaviour
    {
        private bool _isActive = false;

        [SerializeField]
        private float _barrelRollSpeed = 0.75f;

        private Vector3 _startEulerAngles;

        private float _endEulerAngleZ,
            _currentAngleZ;

        private float _elapsedTime = 0f;

        private void OnEnable()
        {
            PlayerInputController.onBarrelRollInput += CalculateBarrelRoll;
        }

        private void OnDisable()
        {
            PlayerInputController.onBarrelRollInput -= CalculateBarrelRoll;
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
    }
}

