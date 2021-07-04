using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooterV3.Scripts.Helpers;
using SpaceShooterV3.Scripts.CustomClasses;

namespace SpaceShooterV3.Scripts.Managers
{
    public class PoolManager : MonoSingleton<PoolManager>
    {
        [SerializeField]
        private List<Pool> _activePools = new List<Pool>();

        private void Start()
        {
            GeneratePools();
        }

        private void GeneratePools()
        {
            for (int a = 0; a < _activePools.Count; a++)
            {
                for (int b = 0; b < _activePools[a].poolObj.spawnBuffer; b++)
                {
                    GameObject obj = Instantiate(_activePools[a].poolObj.objectPrefab);
                    obj.transform.parent = _activePools[a].poolContainer.transform;
                    obj.SetActive(false);
                }
            }
        }
    }
}

