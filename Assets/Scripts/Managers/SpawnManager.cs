using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooterV3.Scripts.Helpers;


public class SpawnManager : MonoSingleton<SpawnManager>
{
    public static Func<int, int, GameObject> onRequestFromPool;
}
