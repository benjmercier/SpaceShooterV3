using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooterV3.Scripts.Enemies
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        protected Vector3 _spawnPos;
        protected Vector3 _targetPos;

        protected Vector2 _minMaxZ = new Vector2(-10f, 40f);
        protected Vector2 _minMaxX = new Vector2(-33.5f, 33.5f);

        protected float _randomX;

        protected bool _targetDetected = false;

        public static Action<GameObject, Vector3> onSetTargetPos;

        protected virtual void Awake()
        {
            
        }

        protected virtual void Start()
        {
            //_steeringTarget = Vector3.back;
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

        protected virtual void OnTriggerEnter(Collider other)
        {
            var tag = other.tag;
            
            switch (tag)
            {
                case "Player":
                    Destroy(this.gameObject);
                    break;

                case "MainFire":
                    other.gameObject.SetActive(false);
                    Destroy(this.gameObject);
                    break;

                default:
                    break;
            }
        }

        
    }
}

