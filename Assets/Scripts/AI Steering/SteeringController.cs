using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooterV3.Scripts.Interfaces;
using SpaceShooterV3.Scripts.AISteering.Behaviors;
using SpaceShooterV3.Scripts.Attacking;
using SpaceShooterV3.Scripts.Enemies;

namespace SpaceShooterV3.Scripts.AISteering
{
    [System.Serializable]
    public class SteeringController : MonoBehaviour, IAutonomousAgent
    {
        public IAutonomousAgent agent;
        private GameObject _targetObj;
        private bool _targetDetected;

        private Vector3 _targetPos = new Vector3(0, 0, -15f);

        private Vector3 _steeringVelocity;
        private Vector3 _desiredVelocity;
        private Vector3 _agentVelocity;

        [SerializeField]
        private float _maxSpeed = 12.5f;
        [SerializeField, Range(0f, 5f)]
        private float _maxForce = 2f;

        [Header("Seek Behavior")]
        [SerializeField]
        private bool _useSeek = false;
        [SerializeField]
        private Seek _seek;

        [Header("Flee Behavior")]
        [SerializeField]
        private bool _useFlee = false;

        [Header("Obstacle Avoidance Behavior")]
        [SerializeField]
        private bool _useAvoidance = false;
        [SerializeField]
        private ObstacleAvoidance _avoidance;


        private bool _isActive = false;

        [SerializeField]
        private float _barrelRollSpeed = 0.75f;

        private Vector3 _startEulerAngles;

        private float _endEulerAngleZ,
            _currentAngleZ;

        private float _elapsedTime = 0f;

        private void Awake()
        {
            agent = this;
        }

        private void OnEnable()
        {
            ObjDetection.onDetectionRadiusTriggered += ObjDetected;
            BaseEnemy.onSetTargetPos += SetTargetPos;
        }

        private void OnDisable()
        {
            ObjDetection.onDetectionRadiusTriggered -= ObjDetected;
            BaseEnemy.onSetTargetPos -= SetTargetPos;
        }

        private void FixedUpdate()
        {
            if (_useSeek)
            {
                _steeringVelocity += _seek.CalculateSeek(transform.position, _targetPos, _desiredVelocity,
                        _agentVelocity, _maxSpeed);
            }

            if (_useFlee)
            {

            }
            
            if (_useAvoidance)
            {
                if (_targetDetected)
                {
                    _useSeek = false;
                    _steeringVelocity += _avoidance.CalculateAvoidance(transform.position, _targetObj, _agentVelocity, _maxSpeed);
                    CalculateBarrelRoll(_steeringVelocity.x);
                }
            }

            CalculateSteering();
        }

        public void CalculateSteering()
        {
            _steeringVelocity = Vector3.ClampMagnitude(_steeringVelocity, _maxForce);
            _steeringVelocity /= agent.Mass();

            _agentVelocity += _steeringVelocity;
            _agentVelocity = Vector3.ClampMagnitude(_agentVelocity, _maxSpeed);

            agent.Transform().position += _agentVelocity * Time.deltaTime;
            //agent.Transform().forward += _agentVelocity * Time.deltaTime;  // controls forward facing direction
        }

        public Transform Transform()
        {
            return this.transform;
        }

        public float Mass()
        {
            if (TryGetComponent(out Rigidbody rigidbody))
            {
                return rigidbody.mass;
            }
            else
            {
                return 1f;
            }
        }

        private void SetTargetPos(GameObject parent, Vector3 targetPos)
        {
            if (this.gameObject == parent)
            {
                _targetPos = targetPos;
            }
        }

        private void ObjDetected(GameObject parent, GameObject target, bool inRange)
        {
            if (this.gameObject == parent)
            {
                _targetObj = target;
                _targetDetected = inRange;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            
        }



        private void CalculateBarrelRoll(float axis)
        {
            if (!_isActive)
            {
                _isActive = true;

                StartCoroutine(BarrelRollRoutine(axis));
            }
        }

        private IEnumerator BarrelRollRoutine(float axis)
        {
            _startEulerAngles = transform.eulerAngles;

            if (axis > 0)
            {
                _endEulerAngleZ = _startEulerAngles.z + 360f; // -360 z = roll left
            }
            else
            {
                _endEulerAngleZ = _startEulerAngles.z - 360f; // +360 z = roll right
            }

            _elapsedTime = 0f;

            while (_elapsedTime < _barrelRollSpeed)
            {
                _elapsedTime += Time.deltaTime;
                _currentAngleZ = Mathf.Lerp(_startEulerAngles.z, _endEulerAngleZ, _elapsedTime / _barrelRollSpeed) % 360f;

                transform.eulerAngles = new Vector3(_startEulerAngles.x, _startEulerAngles.y, _currentAngleZ);

                yield return null;
            }

            _isActive = false;
            _useSeek = true;
        }
    }
}

