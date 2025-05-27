// using UnityEngine;

// public class Gun1 : MonoBehaviour

// {
//     [Header("Projectile")]
//     private LineRenderer lineRenderer;
//     private Camera playerCamera;

//     [Header("Trajectory Settings")]
//     [SerializeField] private int resolution = 30; // Number of points in the trajectory

//     void Start()
//     {
//         lineRenderer = GetComponent<LineRenderer>();
//         playerCamera = Camera.main;
//         lineRenderer.positionCount = resolution;
//     }

//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.E)) // Adjust input as needed
//         {
//             ShootProjectile();
//         }
//     }

//     void ShootProjectile()
//     {
//         //GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
//         //Rigidbody rb = projectile.GetComponent<Rigidbody>();

//         Vector3 shootDirection = playerCamera.transform.forward;
//         Vector3 initialVelocity = shootDirection * speed;
//         initialVelocity.y += Mathf.Abs(gravity) * 0.5f; // Add vertical arc
//         Debug.Log("initialVelocity" + initialVelocity);
//         //rb.linearVelocity = initialVelocity;
//         ParabolicGun parabolicGun = projectile.GetComponent<ParabolicGun>();
//         parabolicGun.firePoint = firePoint;
//         parabolicGun.initialVelocity = initialVelocity;
//         Destroy(projectile, timeToLive);

//         DrawTrajectory(firePoint.position, initialVelocity);
//     }

//     void DrawTrajectory(Vector3 start, Vector3 initialVelocity)
//     {
//         Vector3[] points = new Vector3[resolution];

//         for (int i = 0; i < resolution; i++)
//         {
//             float time = (i / (float)resolution) * timeToLive;
//             points[i] = start + initialVelocity * time + 0.5f * new Vector3(0, gravity, 0) * time * time;
//         }

//         lineRenderer.SetPositions(points);
//     }
// }


// Gun1.cs
using UnityEngine;

public class Gun1 : GunBase
{
    [Header("Projectile")]
    public Transform firePoint;
    protected override void Update()
    {
        base.Update();
        HandleFireInput();
    }

    protected override void Fire()
    {
        // Instantiate and configure projectile
        Debug.Log(" Guu1 Firing: " );

        var proj = Instantiate(props.projectilePrefab, firePoint.transform.position, firePoint.transform.rotation);
        var parab = proj.GetComponent<ParabolicGun>();
        Vector3 dir = playerCamera.transform.forward;
        Vector3 initVel = dir * props.projectileSpeed;

        parab.firePoint = firePoint.transform;
        parab.initialVelocity = initVel;
        parab.gravity = props.gravity;
        parab.timeToLive = props.timeToLive;


        Destroy(proj, props.timeToLive);

        DrawTrajectory(firePoint.transform.position, initVel);
    }

    private void DrawTrajectory(Vector3 start, Vector3 initialVelocity)
    {
        int res = props.trajectoryResolution;
        Vector3[] pts = new Vector3[res];
        for (int i = 0; i < res; i++)
        {
            float t = i / (float)(res - 1) * props.timeToLive;
            pts[i] = start + initialVelocity * t + 0.5f * Vector3.up * props.gravity * t * t;
        }
        lineRenderer.SetPositions(pts);
    }
}