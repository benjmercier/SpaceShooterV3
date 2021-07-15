using System;
using UnityEngine;

namespace SpaceShooterV3.Scripts.AI.Steering
{
    public class ObjDetection : MonoBehaviour
    {
        [SerializeField]
        private GameObject _parentObj;

        public static event Action<GameObject, GameObject, bool> onDetectionRadiusTriggered;

        private void Awake()
        {
            if (_parentObj == null)
            {
                _parentObj = this.transform.parent.gameObject;
            }
        }

        private void OnDetectionRadiusTriggered(GameObject parent, GameObject target, bool inRange)
        {
            onDetectionRadiusTriggered?.Invoke(parent, target, inRange);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MainFire") || other.CompareTag("Player"))
            {
                OnDetectionRadiusTriggered(_parentObj, other.gameObject, true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("MainFire") || other.CompareTag("Player"))
            {
                OnDetectionRadiusTriggered(_parentObj, other.gameObject, false);
            }
        }
    }
}

