using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooterV3.Scripts.Interfaces;

namespace SpaceShooterV3.Scripts.Shared
{
    public class DamagerController : MonoBehaviour, IDamager
    {
        [SerializeField]
        private float _damageAmount;
        public float DamageAmount { get { return _damageAmount; } }
    }
}

