using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooterV3.Scripts.Controllers.Firing
{
    public class MainFire : MonoBehaviour
    {
        private GameObject _weaponFired;

        [SerializeField]
        private Transform _firingPos;

        [SerializeField]
        private float _fireRate = 0.1f;
        private float _nextFire = -1f;

        public static Func<int, GameObject> onRequestFromPool;

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
            if (Time.time > _nextFire)
            {
                _nextFire = Time.time + _fireRate;

                _weaponFired = OnRequestFromPool(0);
                _weaponFired.transform.position = _firingPos.position;
            }            
        }

        private GameObject OnRequestFromPool(int poolIndex)
        {
            return onRequestFromPool?.Invoke(poolIndex);
        }
    }
}

