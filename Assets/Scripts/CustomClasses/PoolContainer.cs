using System.Collections.Generic;

namespace SpaceShooterV3.Scripts.CustomClasses
{
    [System.Serializable]
    public struct PoolContainer
    {
        public string name;
        public List<Pool> pools;

        public PoolContainer(string name, List<Pool> poolList)
        {
            this.name = name;
            this.pools = poolList;
        }
    }
}

