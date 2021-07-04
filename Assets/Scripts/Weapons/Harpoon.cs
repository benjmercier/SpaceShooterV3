using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooterV3.Scripts.Weapons
{
    public class Harpoon : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 15f;

        private void Start()
        {
            StartCoroutine(ObjDisableRoutine());
        }

        private void Update()
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
        }

        private IEnumerator ObjDisableRoutine()
        {
            yield return new WaitForSeconds(5f);

            if (gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false);
            }            
        }
    }
}

