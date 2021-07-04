using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooterV3.Scripts.ScriptableObjects.Pools;

namespace SpaceShooterV3.Scripts.CustomClasses
{
    [System.Serializable]
    public struct Pool
    {
        public PoolSO poolObj;
        public GameObject poolContainer;
    }
}

