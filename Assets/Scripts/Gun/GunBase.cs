// GunBase.cs
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public abstract class GunBase : GunProperties
{
    [Tooltip("Assign this gun's properties ScriptableObject")]
    public GunProperties props;

    protected LineRenderer lineRenderer;
    protected Camera playerCamera;
    protected int ammoInClip;

    protected virtual void Awake()
    {
        //lineRenderer = this.GetComponent<LineRenderer>();
        playerCamera = Camera.main;
        ammoInClip = props.clipSize;
        lineRenderer.positionCount = props.trajectoryResolution;
    }

    protected virtual void Update()
    {
        if (cooldown > 0)
            cooldown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R))
            Reload();
    }

    protected void Reload()
    {
        ammoInClip = props.clipSize;
        Debug.Log($"Reloaded {props.weaponName}");
    }

    protected bool CanFire() => cooldown <= 0f && ammoInClip > 0;

    protected void HandleFireInput()
    {
        if (Input.GetKey(props.activationKey) && CanFire())
        {
            Fire();
            ammoInClip--;
            cooldown = props.fireRate;
        }
    }

    protected abstract void Fire();
}

