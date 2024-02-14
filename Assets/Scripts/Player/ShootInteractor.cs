using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootInteractor : Interactor
{
    [Header("Gun")]
    public MeshRenderer gunMeshRenderer;
    public Color bulletColor;
    public Color rocketColor;

    [Header("Shoot")]
    // Create a Dictionary to store the different shoot strategies
    // Use the shoot type as the key
    public ObjectPool bulletPool;
    public ObjectPool rocketPool;
    [SerializeField] private float shootForce;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private PlayerMovement playerMovement;

    private float finalShootVelocity;
    private IShootStrategy currentShootStrategy;

    public override void Interact()
    {
        currentShootStrategy ??= new BulletShootStrategy(this);

        if(playerInput.weapon1Pressed)
        {
            currentShootStrategy = new BulletShootStrategy(this);
        }
        else if(playerInput.weapon2Pressed)
        {
            currentShootStrategy = new RocketShootStrategy(this);
        }

        if(playerInput.primaryShootPressed && currentShootStrategy != null)
        {
            currentShootStrategy.Shoot();
        }
    }

    // void Shoot()
    // {
    //     // finalShootVelocity = playerMovement.GetForwardVelocity() + shootForce;

    //     // PooledObject pooledProjectile = projectilePool.GetPooledObject();
    //     // // pooledProjectile.gameObject.SetActive(true);
        
    //     // // Rigidbody projectileInstance = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
    //     // Rigidbody projectile = pooledProjectile.GetComponent<Rigidbody>();
    //     // projectile.transform.position = projectileSpawnPoint.position;
    //     // projectile.transform.rotation = projectileSpawnPoint.rotation;

    //     // projectile.velocity = projectileSpawnPoint.forward * finalShootVelocity;
    //     // // Destroy(projectile.gameObject, 5f);
    //     // projectilePool.DestroyPooledObject(pooledProjectile, 5f);
    // }

    #region Getters and Setters
    public Transform GetProjectileSpawnPoint()
    {
        return projectileSpawnPoint;
    }

    public float GetFinalShootVelocity()
    {
        finalShootVelocity = playerMovement.GetForwardVelocity() + shootForce;
        return finalShootVelocity;
    }
    #endregion
}
