using UnityEngine;

namespace SpaceShooterV3.Scripts.AI.Steering.Behaviors
{
    [System.Serializable]
    public class Seek
    {
        private Vector3 _seekForce;

        public bool useArrival = false;

        [SerializeField]
        private float _arrivalRadius = 10f;
        private float _arrivalDistance;

        public Seek(Vector3 force, bool arrival, float radius, float distance)
        {
            this._seekForce = force;
            this.useArrival = arrival;
            this._arrivalRadius = radius;
            this._arrivalDistance = distance;
        }

        public Vector3 CalculateSeek(Vector3 agentPos, Vector3 targetPos, Vector3 desiredVel, Vector3 agentVel, float speed)
        {
            if (useArrival)
            {
                desiredVel = targetPos - agentPos;

                _arrivalDistance = desiredVel.magnitude;

                desiredVel.Normalize();

                if (_arrivalDistance < _arrivalRadius)
                {
                    desiredVel *= _arrivalDistance / _arrivalRadius * speed;
                }
                else
                {
                    desiredVel *= speed;
                }

                _seekForce = desiredVel - agentVel;

                return _seekForce;
            }
            else
            {
                desiredVel = (targetPos - agentPos).normalized;
                desiredVel *= speed;

                _seekForce = desiredVel - agentVel;

                return _seekForce;
            }
        }
    }
}

