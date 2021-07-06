using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooterV3.Scripts.ScriptableObjects.Pools;

namespace SpaceShooterV3.Scripts.CustomClasses
{
    [System.Serializable]
    public struct Pool
    {
        public string name;
        public GameObject prefab;
        public List<GameObject> prefabList;
        public GameObject container;
        public int spawnBuffer;

        public Pool (string name, GameObject prefab, List<GameObject> list, GameObject container, int buffer)
        {
            this.name = name;
            this.prefab = prefab;                      
            this.prefabList = list;
            this.container = container;
            this.spawnBuffer = buffer;
        }
    }
}

