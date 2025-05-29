using UnityEngine;

public abstract class GunBase : ScriptableObject
{
    public string weaponName;
    public KeyCode activationKey = KeyCode.Alpha1;
    public float fireRate = 0.2f;
    public int clipSize = 10;
    public float projectileSpeed = 20f;
    public float gravity = -9.81f;
    public float timeToLive = 5f;
    public int trajectoryResolution = 10;

    public GameObject projectilePrefab;

    public Sprite weaponIcon;

    public abstract void Fire(Transform firePoint, Camera playerCamera, LineRenderer lineRenderer);
}
