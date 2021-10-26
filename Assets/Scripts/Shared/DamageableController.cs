using UnityEngine;
using SpaceShooterV3.Scripts.Interfaces;
using SpaceShooterV3.Scripts.PropertyAttributes;
using SpaceShooterV3.Scripts.AI.Agents.Enemies;
using SpaceShooterV3.Scripts.Weapons;

namespace SpaceShooterV3.Scripts.Shared
{
    public class DamageableController : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private float _maxHealth;
        [SerializeField, ReadOnly]
        private float _currentHealth;
        public float Health { get { return _currentHealth; } }

        [SerializeField]
        private float _maxArmor;
        [SerializeField, ReadOnly]
        private float _currentArmor;
        public float Armor { get { return _currentArmor; } }

        private float _damageDelta;

        private void OnEnable()
        {
            ResetStatus();
        }

        private void ResetStatus()
        {
            _currentHealth = _maxHealth;
            _currentArmor = _maxArmor;
        }

        public void DamageReceived(float damageAmount)
        {
            CalculateDamage(damageAmount);
        }

        private void CalculateDamage(float damageAmount)
        {
            if (_currentArmor > 0f)
            {
                _currentArmor -= damageAmount;

                _damageDelta = _currentHealth - _currentArmor;

                if (_currentArmor < 0f)
                {
                    _currentArmor = 0f;
                }
            }
            else
            {
                if (_currentArmor != 0)
                {
                    _currentArmor = 0f;
                }

                _damageDelta = _maxHealth;
            }

            _currentHealth -= (_damageDelta / _maxHealth) * damageAmount;

            if (_currentHealth <= 0f)
            {
                _currentHealth = 0f;

                OnObjDestroyed(this.gameObject);
            }
        }

        private void OnObjDestroyed(GameObject destroyedObj)
        {
            Debug.Log(destroyedObj.name + " is destroyed!");
            destroyedObj.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamager damager))
            {
                DamageReceived(damager.DamageAmount);
            
                var tag = other.tag;

                switch (tag)
                {
                    case "MainFire":
                        other.gameObject.SetActive(false);
                        break;

                    // Allow for other weapon characteristics/lifespans
                    /*  
                    case "SecondaryFire":
                        other.gameObject.SetActive(false);
                        break;
                    */

                    default:
                        break;
                }
            }
        }
    }
}

