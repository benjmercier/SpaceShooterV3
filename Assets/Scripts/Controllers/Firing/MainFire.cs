using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooterV3.Scripts.Controllers.Firing
{
    public class MainFire : MonoBehaviour
    {
        [SerializeField]
        private GameObject _weaponPrefab;

        [SerializeField]
        private Transform _firingPos;

        private void Start()
        {
            
        }

        private void OnEnable()
        {
            PlayerInputController.onFireInput += CalculateMainFire;
        }

        private void OnDisable()
        {
            PlayerInputController.onFireInput -= CalculateMainFire;
        }

        private void CalculateMainFire()
        {
            Instantiate(_weaponPrefab, _firingPos.position, Quaternion.identity);
        }
    }
}

