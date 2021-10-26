using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SpaceShooterV3.Scripts.Helpers;
using SpaceShooterV3.Scripts.CustomClasses;
using SpaceShooterV3.Scripts.Player.Firing;

namespace SpaceShooterV3.Scripts.Managers
{
    public class PoolManager : MonoSingleton<PoolManager>
    {
        [SerializeField]
        private List<PoolContainer> _availablePools = new List<PoolContainer>();

        private GameObject _prefabToActivate;

        private GameObject _tempObj;

        private void Start()
        {
            GeneratePools();
        }

        private void OnEnable()
        {
            MainFire.onRequestFromPool += ActivateObjFromPool;
            SpawnManager.onRequestFromPool += ActivateObjFromPool;
        }

        private void OnDisable()
        {
            MainFire.onRequestFromPool -= ActivateObjFromPool;
            SpawnManager.onRequestFromPool -= ActivateObjFromPool;
        }

        private void GeneratePools()
        {
            for (int a = 0; a < _availablePools.Count; a++)
            {
                for (int b = 0; b < _availablePools[a].pools.Count; b++)
                {
                    for (int c = 0; c < _availablePools[a].pools[b].spawnBuffer; c++)
                    {
                        GeneratePoolObj(a, b);
                    }
                }
            }
        }

        private void GeneratePoolObj(int containerIndex, int poolIndex)
        {
            _tempObj = Instantiate(_availablePools[containerIndex].pools[poolIndex].prefab);
            _tempObj.transform.parent = _availablePools[containerIndex].pools[poolIndex].container.transform;
            _tempObj.SetActive(false);

            _availablePools[containerIndex].pools[poolIndex].prefabList.Add(_tempObj);
        }

        private GameObject ActivateObjFromPool(int containerIndex, int poolIndex)
        {
            if (_availablePools[containerIndex].pools[poolIndex].prefabList.Any(a => !a.activeInHierarchy))
            {
                _prefabToActivate = _availablePools[containerIndex].pools[poolIndex].
                    prefabList.FirstOrDefault(f => !f.activeInHierarchy);
            }
            else
            {
                GeneratePoolObj(containerIndex, poolIndex);

                return ActivateObjFromPool(containerIndex, poolIndex);
            }

            _prefabToActivate.SetActive(true);

            return _prefabToActivate;
        }
    }
}

