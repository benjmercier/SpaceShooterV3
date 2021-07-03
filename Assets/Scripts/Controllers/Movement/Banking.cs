using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooterV3.Scripts.Controllers.Movement
{
    public class Banking : MonoBehaviour
    {
        [SerializeField]
        private float _bankingSpeed = 5f;
        [SerializeField]
        private float _maxBankingRot = 45f;

        private float _xAxisDirection;

        private Quaternion _defaultRotation,
            _bankingRotation,
            _bankLeft,
            _bankRight;

        private void Start()
        {
            _defaultRotation = transform.rotation;

            _bankLeft = Quaternion.Euler(0f, 0f, -_maxBankingRot);
            _bankRight = Quaternion.Euler(0f, 0f, _maxBankingRot);
        }

        private void OnEnable()
        {
            PlayerInputController.onMoveInput += CalculateBanking;
        }

        private void OnDisable()
        {
            PlayerInputController.onMoveInput -= CalculateBanking;
        }

        private void CalculateBanking(Vector3 direction)
        {
            _xAxisDirection = direction.x;

            if (_xAxisDirection > 0) // bank left
            {
                _bankingRotation = _bankLeft;
            }
            else if (_xAxisDirection < 0) // bank right
            {
                _bankingRotation = _bankRight;
            }
            else // default
            {
                _bankingRotation = _defaultRotation;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, _bankingRotation, _bankingSpeed * Time.deltaTime);
        }
    }
}

