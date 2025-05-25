using UnityEngine;

public class Gun1 : MonoBehaviour
{
    public float speed = 10f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float gravity = -9.81f;
    public float timeToLive = 5f;
    public int resolution = 30; // Number of points to draw the trajectory

    private LineRenderer lineRenderer;
    private Camera playerCamera;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        playerCamera = Camera.main;
        lineRenderer.positionCount = resolution;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Adjust input as needed
        {
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        Vector3 shootDirection = playerCamera.transform.forward;
        Vector3 initialVelocity = shootDirection * speed;
        initialVelocity.y += Mathf.Abs(gravity) * 0.5f; // Add vertical arc
        Debug.Log("initialVelocity" + initialVelocity);
        rb.linearVelocity = initialVelocity;
        Destroy(projectile, timeToLive);

        DrawTrajectory(firePoint.position, initialVelocity);
    }

    void DrawTrajectory(Vector3 start, Vector3 initialVelocity)
    {
        Vector3[] points = new Vector3[resolution];

        for (int i = 0; i < resolution; i++)
        {
            float time = (i / (float)resolution) * timeToLive;
            points[i] = start + initialVelocity * time + 0.5f * new Vector3(0, gravity, 0) * time * time;
        }

        lineRenderer.SetPositions(points);
    }
}