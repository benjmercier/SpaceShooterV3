using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooterV3.Scripts.AISteering.Behaviors
{
    [System.Serializable]
    public class ObstacleAvoidance
    {
        private Vector3 _avoidanceForce;
        private Vector3 _ahead,
            _aheadHalf;

        [SerializeField]
        private float _maxAvoidanceForce = 10f;
        private float _dynamicView;

        

        public Vector3 CalculateAvoidance(Vector3 agentPos, GameObject target, Vector3 agentVel, float speed)
        {
            _dynamicView = agentVel.magnitude / speed;

            _ahead = agentPos + agentVel.normalized * _dynamicView;
            _aheadHalf = _ahead * 0.5f;

            _avoidanceForce = Vector3.zero;

            if (target != null)
            {
                _avoidanceForce.x = agentPos.x - target.transform.position.x;
                Debug.Log(_avoidanceForce.x);
                //_avoidanceForce.Normalize();

                _avoidanceForce *= _maxAvoidanceForce;
            }

            return _avoidanceForce;
        }

        
    }
}

