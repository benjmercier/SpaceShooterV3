using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooterV3.Scripts.Helpers;
using SpaceShooterV3.Scripts.Managers;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    private GameObject _spawnedPrefab;
    private Vector3 _spawnPos;
    private Vector3 _targetPos;

    private bool _canSpawn = false;

    public static Action<GameObject, Vector3> onSetTargetPos;
    public static Func<int, int, GameObject> onRequestFromPool;

    // spawn x amound of enemies at start
    // delay between each

    // make request to pool manager for enemy
    // activate requested enemy

    private void Start()
    {
        _canSpawn = true;
    }

    private void Update()
    {
        if (_canSpawn)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _canSpawn = false;

                //start spawn
                ActivatePrefabFromPool();
            }
        }
    }

    private void ActivatePrefabFromPool()
    {
        _spawnedPrefab = OnRequestFromPool(1, 0); // deep sea predator

        _spawnedPrefab.transform.position = SetSpawnPos();

        SetTargetPos(_spawnPos.x);

        var lookPos = _targetPos - _spawnedPrefab.transform.position;
        //lookPos.y = 0;

        var spawnRotation = Quaternion.LookRotation(lookPos);
        _spawnedPrefab.transform.rotation = spawnRotation;
    }

    private GameObject OnRequestFromPool(int containerIndex, int poolIndex)
    {
        return onRequestFromPool?.Invoke(containerIndex, poolIndex);
    }

    private Vector3 SetSpawnPos()
    {
        _spawnPos = BoundaryManager.Instance.CalculateUpperBounds();

        return _spawnPos;
    }

    private void SetTargetPos(float spawnPosX)
    {
        _targetPos = new Vector3(spawnPosX, 0f, BoundaryManager.Instance.CalculateLowerBounds().z);

        OnSetTargetPos(_spawnedPrefab);
    }

    private void OnSetTargetPos(GameObject spawnedPrefab)
    {
        onSetTargetPos?.Invoke(spawnedPrefab, _targetPos);
    }    
}
