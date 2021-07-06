using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SpaceShooterV3.Scripts.Helpers;
using SpaceShooterV3.Scripts.CustomClasses;
using SpaceShooterV3.Scripts.Controllers.Firing;

namespace SpaceShooterV3.Scripts.Managers
{
    public class PoolManager : MonoSingleton<PoolManager>
    {
        [SerializeField]
        private List<Pool> _activePools = new List<Pool>();
        private Dictionary<int, List<GameObject>> _activePoolObjDictionary = new Dictionary<int, List<GameObject>>();

        private GameObject _prefabToActivate;

        private GameObject _tempObj;

        private void Start()
        {
            GeneratePools();
        }

        private void OnEnable()
        {
            MainFire.onRequestFromPool += ActivateObjFromPool;
        }

        private void OnDisable()
        {
            MainFire.onRequestFromPool -= ActivateObjFromPool;
        }

        private void GeneratePools()
        {
            for (int a = 0; a < _activePools.Count; a++)
            {
                for (int b = 0; b < _activePools[a].spawnBuffer; b++)
                {
                    GeneratePoolObject(a);
                }
            }
        }

        private void GeneratePoolObject(int poolIndex)
        {
            _tempObj = Instantiate(_activePools[poolIndex].prefab);
            _tempObj.transform.parent = _activePools[poolIndex].container.transform;
            _tempObj.SetActive(false);

            _activePools[poolIndex].prefabList.Add(_tempObj);
        }

        private GameObject ActivateObjFromPool(int poolIndex)
        {
            if (_activePools[poolIndex].prefabList.Any(a => !a.activeInHierarchy))
            {
                _prefabToActivate = _activePools[poolIndex].prefabList.FirstOrDefault(f => !f.activeInHierarchy);
            }
            else
            {
                GeneratePoolObject(poolIndex);

                return ActivateObjFromPool(poolIndex);
            }

            _prefabToActivate.SetActive(true);

            return _prefabToActivate;
        }
    }
}

