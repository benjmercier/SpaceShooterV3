using Unity.Entities;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    void Start()
    {
        MakeEntity();
    }

    private void MakeEntity()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        entityManager.CreateEntity();
    }
}
