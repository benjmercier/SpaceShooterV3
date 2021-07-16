using UnityEngine;

namespace SpaceShooterV3.Scripts.Interfaces
{
    public interface IDamageable
    {
        float Health { get; }
        float Armor { get; }
        void DamageReceived(GameObject damagedObj, float damageAmount);
    }
}

