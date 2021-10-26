using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooterV3.Scripts.Helpers;

namespace SpaceShooterV3.Scripts.Managers
{
    public class BoundaryManager : MonoSingleton<BoundaryManager>
    {
        [SerializeField]
        private Camera _camera;

        private Vector3 _boundaryVector;

        [SerializeField, Range(0f, 0.5f)]
        private float _yOffset = 0.1f;

        private float _upperX;
        private float _upperY;        
        private Vector3 _upperViewVector;
        private Vector3 _upperWorldVector;

        private float _lowerY;
        private Vector3 _lowerViewVector;
        private Vector3 _lowerWorldVector;

        #region Old Boundary Calculator
        /*        
        private float _cameraDist;
        private Vector3 _localPos;
        private Vector3 _lBVector;
        private Vector3 _rTVector;
        private Vector3 _leftBottom;
        private Vector3 _rightTop;
        private float _xClamp;
        private float _yClamp;
        private Vector3 _clampedPos;
        private Vector3 _staticYPos;

        public Vector3 StayWithinBoundary(Transform objTransform, Vector3 colliderSize)
        {
            _cameraDist = _camera.transform.InverseTransformPoint(objTransform.position).z;

            _localPos = _camera.transform.InverseTransformPoint(objTransform.position);
            
            _lBVector = new Vector3(0, 0, _cameraDist);
            _rTVector = new Vector3(1, 1, _cameraDist);

            _leftBottom = _camera.ViewportToWorldPoint(_lBVector);
            _rightTop = _camera.ViewportToWorldPoint(_rTVector);

            _leftBottom = _camera.transform.InverseTransformPoint(_leftBottom);
            _rightTop = _camera.transform.InverseTransformPoint(_rightTop);

            var xmin = _leftBottom.x + colliderSize.x;
            var xmax = _rightTop.x - colliderSize.x;
            Debug.Log("X Min: " + xmin);
            Debug.Log("X Max: " + xmax);

            var ymin = _leftBottom.y + colliderSize.y;
            var ymax = _rightTop.y - colliderSize.y;
            Debug.Log("Y Min: " + ymin);
            Debug.Log("Y Max: " + ymax);
                        
            _xClamp = Mathf.Clamp(_localPos.x, _leftBottom.x + colliderSize.x, _rightTop.x - colliderSize.x);
            _yClamp = Mathf.Clamp(_localPos.y, _leftBottom.y + colliderSize.y, _rightTop.y - colliderSize.y);

            _clampedPos = new Vector3(_xClamp, _yClamp, _localPos.z);

            objTransform.position = _camera.transform.TransformPoint(_clampedPos);
            
            _staticYPos.x = objTransform.position.x;
            _staticYPos.y = 0;
            _staticYPos.z = objTransform.position.z;
            
            return _staticYPos;
        }
        */
        #endregion

        protected override void Init()
        {
            base.Init();

            if (_camera == null)
            {
                _camera = Camera.main;
            }
        }

        private void Start()
        {
            
        }

        public Vector3 StayWithinBoundary(Transform objTransform)
        {
            _boundaryVector = _camera.WorldToViewportPoint(objTransform.position);
            
            _boundaryVector.x = Mathf.Clamp01(_boundaryVector.x);
            _boundaryVector.y = Mathf.Clamp01(_boundaryVector.y);
            
            return _camera.ViewportToWorldPoint(_boundaryVector);
        }

        public Vector3 CalculateUpperBounds()
        {
            // lower left = (0, 0)
            // top right = (1, 1)
            _upperX = Random.Range(0f, 1f);
            _upperY = 1f + _yOffset;       

            return SetViewportToWorldPoint(_upperX, _upperY, _upperViewVector, _upperWorldVector); 
        }

        public Vector3 CalculateLowerBounds()
        {
            _lowerY = 0f - _yOffset;

            return SetViewportToWorldPoint(0, _lowerY, _lowerViewVector, _lowerWorldVector);
        }

        private Vector3 SetViewportToWorldPoint(float viewX, float viewY, Vector3 viewVector, Vector3 worldVector)
        {
            viewVector = new Vector3(viewX, viewY, 0);

            worldVector.x = _camera.ViewportToWorldPoint(viewVector).x;
            worldVector.y = 0;
            worldVector.z = _camera.ViewportToWorldPoint(viewVector).z;

            return worldVector;
        }
    }
}

