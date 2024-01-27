using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShootStrategy : MonoBehaviour, IShootStrategy
{
    ShootInteractor shootInteractor;
    Transform projectileSpawnPoint;

    public BulletShootStrategy(ShootInteractor shootInteractor)
    {
        this.shootInteractor = shootInteractor;
        projectileSpawnPoint = shootInteractor.GetProjectileSpawnPoint();

        // Set gun color
        this.shootInteractor.gunMeshRenderer.material.color = this.shootInteractor.bulletColor;
    }

    public void Shoot()
    {
        PooledObject pooledProjectile = shootInteractor.bulletPool.GetPooledObject();
        // pooledProjectile.gameObject.SetActive(true);
        
        // Rigidbody projectileInstance = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        Rigidbody projectile = pooledProjectile.GetComponent<Rigidbody>();
        projectile.transform.position = projectileSpawnPoint.position;
        projectile.transform.rotation = projectileSpawnPoint.rotation;

        projectile.velocity = projectileSpawnPoint.forward * shootInteractor.GetFinalShootVelocity();
        // Destroy(projectile.gameObject, 5f);
        shootInteractor.bulletPool.DestroyPooledObject(pooledProjectile, 5f);
    }
}
