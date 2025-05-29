using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class GunManager : MonoBehaviour
{
    public List<GunBase> guns;
    public Transform firePoint;

    private Camera playerCamera;
    private GunBase activeGun;
    private LineRenderer lineRenderer;

    private float cooldownTimer = 0f;
    private int ammoInClip = 0;

    void Start()
    {
        playerCamera = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();

        if (guns.Count > 0)
            EquipGun(0);
    }

    void Update()
    {
        if (activeGun == null) return;

        cooldownTimer -= Time.deltaTime;
        HandleInput();
        SwitchGuns();
    }

    void HandleInput()
    {
        if (Input.GetKey(KeyCode.E) && cooldownTimer <= 0 && ammoInClip > 0)
        {
            activeGun.Fire(firePoint, playerCamera, lineRenderer);
            cooldownTimer = activeGun.fireRate;
            ammoInClip--;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void SwitchGuns()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            if (Input.GetKeyDown(guns[i].activationKey))
            {
                EquipGun(i);
                break;
            }
        }
    }

    void EquipGun(int index)
    {
        activeGun = guns[index];
        ammoInClip = activeGun.clipSize;
        cooldownTimer = 0;
        Debug.Log("Equipped: " + activeGun.weaponName);
    }

    void Reload()
    {
        ammoInClip = activeGun.clipSize;
        Debug.Log("Reloaded: " + activeGun.weaponName);
    }
    public GunBase GetActiveGun()
    {
        return activeGun;
    }
    public float GetCooldownTime()
    {
        return cooldownTimer;
    }
    public float GetReloadTime()
    {
        return activeGun.fireRate;
    }
    public float GetReloadProgress()
    {
        return (cooldownTimer / activeGun.fireRate);
    }
    public int GetAmmoInClip()
    {
        return ammoInClip;
    }
}
