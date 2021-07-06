using System.Collections;
using UnityEngine;

namespace SpaceShooterV3.Scripts.Weapons
{
    public class Harpoon : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 15f;

        private WaitForSeconds _waitToDisable = new WaitForSeconds(5f);

        private void OnEnable()
        {
            StartCoroutine(ObjDisableRoutine());
        }

        private void Update()
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
        }

        private IEnumerator ObjDisableRoutine()
        {
            yield return _waitToDisable;

            if (gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false);
            }            
        }
    }
}

