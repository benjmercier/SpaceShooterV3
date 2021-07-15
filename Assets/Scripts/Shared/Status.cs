using UnityEngine;
using SpaceShooterV3.Scripts.Interfaces;

namespace SpaceShooterV3.Scripts.Shared
{
    public class Status : MonoBehaviour, IDamageable
    {
        public float Health { get; }
        public float Armor { get; }

        public void Damage()
        {
            
        }
    }
}

