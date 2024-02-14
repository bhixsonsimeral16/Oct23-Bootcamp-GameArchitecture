using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShootStrategy : IShootStrategy
{
    ShootInteractor shootInteractor;
    Transform projectileSpawnPoint;

    public BulletShootStrategy(ShootInteractor shootInteractor)
    {
        Debug.Log("Switched to Bullet mode");
        this.shootInteractor = shootInteractor;
        projectileSpawnPoint = shootInteractor.GetProjectileSpawnPoint();

        // Set gun color
        this.shootInteractor.gunMeshRenderer.material.color = this.shootInteractor.bulletColor;
    }

    public void Shoot()
    {
        PooledObject pooledProjectile = shootInteractor.bulletPool.GetPooledObject();
        
        Rigidbody projectile = pooledProjectile.GetComponent<Rigidbody>();
        projectile.transform.SetPositionAndRotation(projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        projectile.velocity = projectileSpawnPoint.forward * shootInteractor.GetFinalShootVelocity();

        shootInteractor.bulletPool.DestroyPooledObject(pooledProjectile, 5f);
    }
}
