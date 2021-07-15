using UnityEngine;

namespace SpaceShooterV3.Scripts.Interfaces
{
    public interface IAutonomousAgent
    {
        Transform Transform();
        float Mass();
    }
}

