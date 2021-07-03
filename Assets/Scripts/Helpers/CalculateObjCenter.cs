using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooterV3.Scripts.Helpers
{
    public class CalculateObjCenter : MonoBehaviour
    {
        // This script is used to calculate the center of an object with multiple child renderers.
        // That new center is then used to center the object in local or world space.

        private Renderer[] _childRenderers;

        private float _rendererCount;

        private Vector3 _center;
        private Vector3 _newCenter;

        private void Start()
        {
            Debug.Log("Set obj local pos to " + ReturnObjCenter() + " within parent to center.");
        }

        private Vector3 ReturnObjCenter()
        {
            _childRenderers = transform.GetComponentsInChildren<Renderer>();

            _center = Vector3.zero;
            _rendererCount = 0f;

            foreach (var renderer in _childRenderers)
            {
                _center += renderer.transform.position;

                _rendererCount++;
            }
            
            _newCenter = _center / _rendererCount;
            
            _newCenter *= -1f;

            return _newCenter;
        }
    }
}

