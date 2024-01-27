using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShootStrategy : MonoBehaviour, IShootStrategy
{
    ShootInteractor shootInteractor;
    Transform projectileSpawnPoint;

    public RocketShootStrategy(ShootInteractor shootInteractor)
    {
        this.shootInteractor = shootInteractor;
        projectileSpawnPoint = shootInteractor.GetProjectileSpawnPoint();
    }

    public void Shoot()
    {
        Debug.Log("Shoot rocket");
    }
}
