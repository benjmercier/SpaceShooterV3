using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooterV3.Scripts.ScriptableObjects.Pools
{
    [CreateAssetMenu(fileName = "NewPool.asset", menuName = "Scriptable Objects/Pool")]
    public class PoolSO : ScriptableObject
    {
        public string poolName;
        public GameObject objectPrefab;
        public int spawnBuffer;
    }
}

