namespace SpaceShooterV3.Scripts.Interfaces
{
    public interface IDamageable
    {
        float Health { get; }
        float Armor { get; }
        void Damage();
    }
}

