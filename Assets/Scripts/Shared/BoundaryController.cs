using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooterV3.Scripts.Managers;

namespace SpaceShooterV3.Scripts.Shared
{
    [RequireComponent(typeof(Collider))]
    public class BoundaryController : MonoBehaviour
    {
        private Vector3 _colliderSize;
        
        private void OnEnable()
        {
            if (TryGetComponent(out Collider collider))
            {
                _colliderSize = collider.bounds.size;
            }
        }

        private void Update()
        {
            transform.position = BoundaryManager.Instance.StayWithinBoundary(transform);
        }
    }
}

