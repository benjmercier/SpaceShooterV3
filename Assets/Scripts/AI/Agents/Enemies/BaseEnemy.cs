using System;
using UnityEngine;
using SpaceShooterV3.Scripts.Interfaces;
using SpaceShooterV3.Scripts.Managers;

namespace SpaceShooterV3.Scripts.AI.Agents.Enemies
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        protected Vector3 _spawnPos;
        protected Vector3 _targetPos;

        protected Vector2 _minMaxZ;// = new Vector2(-15f, 45f);
        //protected Vector2 _minMaxX = new Vector2(-33.5f, 33.5f);

        protected float _randomX;

        protected bool _targetDetected = false;

        public static Action<GameObject, Vector3> onSetTargetPos;

        protected virtual void Awake()
        {
            
        }

        protected virtual void Start()
        {
            _minMaxZ = new Vector2(BoundaryManager.Instance.CalculateLowerBounds().z,
               BoundaryManager.Instance.CalculateUpperBounds().z);

            //SpawnEnemy();
        }

        protected void OnEnable()
        {
           
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
            _spawnPos = BoundaryManager.Instance.CalculateUpperBounds();
            
            OnSetTargetPos(_spawnPos.x);

            transform.position =  _spawnPos;
        }

        private void OnSetTargetPos(float xAxisPos)
        {
            _targetPos = new Vector3(xAxisPos, 0f, BoundaryManager.Instance.CalculateLowerBounds().z);

            onSetTargetPos?.Invoke(this.gameObject, _targetPos);
        }

        private void CheckIfInBounds(float zPos)
        {            
            if (zPos < _minMaxZ.x)
            {
                SpawnEnemy();
            }
        }
    }
}

