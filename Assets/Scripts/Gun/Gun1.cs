using UnityEngine;

[CreateAssetMenu(fileName = "Gun1", menuName = "FPS/Guns/Gun1", order = 1)]
public class Gun1 : GunBase
{
    public override void Fire(Transform firePoint, Camera playerCamera, LineRenderer lineRenderer)
    {
        Debug.Log("Gun1 Firing...");

        GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        var parab = proj.GetComponent<ParabolicGun>();

        Vector3 dir = playerCamera.transform.forward;
        Vector3 initVel = dir * projectileSpeed;

        parab.firePoint = firePoint;
        parab.initialVelocity = initVel;
        parab.gravity = gravity;
        parab.timeToLive = timeToLive;

        Object.Destroy(proj, timeToLive);

        DrawTrajectory(firePoint.position, initVel, lineRenderer);
    }

    private void DrawTrajectory(Vector3 start, Vector3 initialVelocity, LineRenderer lineRenderer)
    {
        Vector3[] pts = new Vector3[trajectoryResolution];
        for (int i = 0; i < trajectoryResolution; i++)
        {
            float t = i / (float)(trajectoryResolution - 1) * timeToLive;
            pts[i] = start + initialVelocity * t + 0.5f * Vector3.up * gravity * t * t;
        }
        lineRenderer.positionCount = trajectoryResolution;
        lineRenderer.SetPositions(pts);
    }
}
