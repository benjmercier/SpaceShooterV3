using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooterV3.Scripts
{
    public class AreaBounding : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;
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

        private Vector3 _size;

        private void Start()
        {
            if (_camera == null)
            {
                _camera = Camera.main;
            }

            if (TryGetComponent(out Collider collider))
            {
                _size = collider.bounds.size;
            }
        }

        private void Update()
        {
            StayWithinCameraView();
        }

        private void StayWithinCameraView()
        {
            _cameraDist = _camera.transform.InverseTransformPoint(transform.position).z;
            
            _localPos = _camera.transform.InverseTransformPoint(transform.position);

            _lBVector = new Vector3(0, 0, _cameraDist);
            _rTVector = new Vector3(1, 1, _cameraDist);

            _leftBottom = _camera.ViewportToWorldPoint(_lBVector);
            _rightTop = _camera.ViewportToWorldPoint(_rTVector);

            _leftBottom = _camera.transform.InverseTransformPoint(_leftBottom);
            _rightTop = _camera.transform.InverseTransformPoint(_rightTop);

            _xClamp = Mathf.Clamp(_localPos.x, _leftBottom.x + _size.x, _rightTop.x - _size.x);
            _yClamp = Mathf.Clamp(_localPos.y, _leftBottom.y + _size.y, _rightTop.y - _size.y);

            _clampedPos = new Vector3(_xClamp, _yClamp, _localPos.z);

            transform.position = _camera.transform.TransformPoint(_clampedPos);
            
            _staticYPos.x = transform.position.x;
            _staticYPos.y = 0;
            _staticYPos.z = transform.position.z;

            transform.position = _staticYPos;
        }
    }
}

