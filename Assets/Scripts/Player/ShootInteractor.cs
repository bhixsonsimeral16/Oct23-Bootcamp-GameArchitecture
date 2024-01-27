using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootInteractor : Interactor
{
    [SerializeField] private Input inputType;
    // [SerializeField] private Rigidbody projectilePrefab;

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

    public enum Input
    {
        LeftClick,
        RightClick
    }

    public override void Interact()
    {
        if ((inputType == Input.LeftClick && playerInput.primaryShootPressed) ||
            (inputType == Input.RightClick && playerInput.secondaryShootPressed))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // finalShootVelocity = playerMovement.GetForwardVelocity() + shootForce;

        // PooledObject pooledProjectile = projectilePool.GetPooledObject();
        // // pooledProjectile.gameObject.SetActive(true);
        
        // // Rigidbody projectileInstance = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        // Rigidbody projectile = pooledProjectile.GetComponent<Rigidbody>();
        // projectile.transform.position = projectileSpawnPoint.position;
        // projectile.transform.rotation = projectileSpawnPoint.rotation;

        // projectile.velocity = projectileSpawnPoint.forward * finalShootVelocity;
        // // Destroy(projectile.gameObject, 5f);
        // projectilePool.DestroyPooledObject(pooledProjectile, 5f);
    }

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
