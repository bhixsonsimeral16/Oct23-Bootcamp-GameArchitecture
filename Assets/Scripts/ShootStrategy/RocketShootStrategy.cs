using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShootStrategy : IShootStrategy
{
    ShootInteractor shootInteractor;
    Transform projectileSpawnPoint;

    public RocketShootStrategy(ShootInteractor shootInteractor)
    {
        Debug.Log("Switched to Rocket mode");
        this.shootInteractor = shootInteractor;
        projectileSpawnPoint = shootInteractor.GetProjectileSpawnPoint();

        // Set gun color
        this.shootInteractor.gunMeshRenderer.material.color = this.shootInteractor.rocketColor;
    }

    public void Shoot()
    {
        PooledObject pooledProjectile = shootInteractor.rocketPool.GetPooledObject();
        
        Rigidbody projectile = pooledProjectile.GetComponent<Rigidbody>();
        projectile.transform.SetPositionAndRotation(projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        projectile.velocity = projectileSpawnPoint.forward * shootInteractor.GetFinalShootVelocity();
        
        shootInteractor.rocketPool.DestroyPooledObject(pooledProjectile, 5f);
    }
}
