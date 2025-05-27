using UnityEngine;
using System.Collections.Generic;

public class GunManager : MonoBehaviour
{
    [Tooltip("List your ScriptableObject assets here in inspector order")]
    public List<GunProperties> guns;

    private GunProperties activeGun;
    private float cooldownTimer=10f;
    private int ammoInClip;
    public Transform  firePoint;
    private Camera playerCamera;
    void Start()
    {

        
        playerCamera = Camera.main;
        if (guns.Count > 0)
            EquipGun(0);
        cooldownTimer = activeGun.cooldown;
    }

    void Update()
    {
        SwitchGuns();
  
        Debug.Log("Cooldown Timer: " + cooldownTimer);
        if (activeGun == null) return;

        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;

        if (Input.GetKey(KeyCode.E) && cooldownTimer <= 0 && ammoInClip > 0)
        {
            FireActiveGun();
            //activeGun.
        }

        if (Input.GetKeyDown(KeyCode.R))
            Reload();
    }

    void SwitchGuns()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            if (Input.GetKeyDown(guns[i].activationKey))
            {
                EquipGun(i);
                Debug.Log("Switched to: " + guns[i].weaponName);
                break;
            }
        }
    }

    void EquipGun(int index)
    {
        activeGun = guns[index];
        ammoInClip = activeGun.clipSize;
        cooldownTimer = 0f;
        Debug.Log("Equipped: " + activeGun.weaponName);
    }

    void FireActiveGun()
    {
        Debug.Log(" Manager Firing: " + activeGun.weaponName);
        var proj = Instantiate(activeGun.projectilePrefab, firePoint.transform.position, firePoint.transform.rotation);
        var parab = proj.GetComponent<ParabolicGun>();
        Vector3 shootDirection = playerCamera.transform.forward;
        Vector3 initialVelocity = shootDirection * activeGun.speed;
        parab.initialVelocity = initialVelocity;
        parab.firePoint = firePoint;
        parab.gravity = activeGun.gravity;
        parab.timeToLive = activeGun.timeToLive;
        ammoInClip--;
        cooldownTimer = activeGun.fireRate;
    }

    void Reload()
    {
        ammoInClip = activeGun.clipSize;
        Debug.Log("Reloaded " + activeGun.weaponName);
    }
}