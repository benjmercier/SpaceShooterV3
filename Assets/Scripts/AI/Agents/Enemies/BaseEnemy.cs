using System;
using UnityEngine;
using SpaceShooterV3.Scripts.Interfaces;

namespace SpaceShooterV3.Scripts.AI.Agents.Enemies
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        protected Vector3 _spawnPos;
        protected Vector3 _targetPos;

        protected Vector2 _minMaxZ = new Vector2(-15f, 45f);
        protected Vector2 _minMaxX = new Vector2(-33.5f, 33.5f);

        protected float _randomX;

        protected bool _targetDetected = false;

        public static Action<GameObject, Vector3> onSetTargetPos;
        public static Action<GameObject, float> onDamageReceived;

        protected virtual void Awake()
        {
            
        }

        protected virtual void Start()
        {
            
        }

        protected void OnEnable()
        {
            SpawnEnemy();
        }

        protected void OnDisable()
        {
            
        }

        protected virtual void Update()
        {
            CheckIfInBounds(transform.position.z);
        }

        private void SpawnEnemy()
        {
            _randomX = UnityEngine.Random.Range(_minMaxX.x, _minMaxX.y);

            _spawnPos = new Vector3(_randomX, 0, _minMaxZ.y);

            OnSetTargetPos();

            transform.position = _spawnPos;
        }

        private void OnSetTargetPos()
        {
            _targetPos = new Vector3(_spawnPos.x, 0f, _minMaxZ.x);

            onSetTargetPos?.Invoke(this.gameObject, _targetPos);
        }

        private void CheckIfInBounds(float zPos)
        {
            if (zPos < _minMaxZ.x)
            {
                SpawnEnemy();
            }
        }

        private void OnDamageReceived(GameObject damagedObj, float damageAmount)
        {
            onDamageReceived?.Invoke(damagedObj, damageAmount);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            var tag = other.tag;
            
            switch (tag)
            {
                case "Player":
                    //Destroy(this.gameObject);
                    break;

                case "MainFire":
                    other.gameObject.SetActive(false);

                    OnDamageReceived(this.gameObject, 15f);
                    
                    break;

                default:
                    break;
            }
        }
    }
}

