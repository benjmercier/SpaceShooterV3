using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooterV3.Scripts.CustomClasses
{
    [System.Serializable]
    public struct Pool
    {
        public string name;
        public GameObject container;
        public int spawnBuffer;
        public GameObject prefab;
        public List<GameObject> prefabList;

        public Pool (string name, GameObject container, int buffer,  GameObject prefab, List<GameObject> list)
        {
            this.name = name;
            this.container = container;
            this.spawnBuffer = buffer;
            this.prefab = prefab;                      
            this.prefabList = list;
        }
    }
}

