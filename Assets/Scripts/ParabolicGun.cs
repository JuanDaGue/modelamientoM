using UnityEngine;

public class ParabolicGun : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 10;
    public float angle = 0;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float gravity = -9.81f;
    public float timeToLive = 5f;
    public float maxDistance = 100f;
    public float maxHeight = 50f;
    public Vector3 InitialVelocity;
    public Vector3 InitialPosition;




    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InstantiateProjectile();

    }
    void InstantiateProjectile()
    {
        Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
        direction = direction.normalized;
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, direction, out hit, maxDistance, 1 << LayerMask.NameToLayer("Player")))
        {
            if (hit.distance < maxDistance)
            {
                Vector3 position = hit.point + direction * maxHeight;
                GameObject projectile = Instantiate(projectilePrefab, position, Quaternion.identity) as GameObject;
                projectile.transform.position = InitialPosition;
                projectile.transform.rotation = firePoint.rotation;
                projectile.GetComponent<Rigidbody>().linearVelocity = InitialVelocity;
                projectile.GetComponent<Rigidbody>().AddForce(direction * speed);
                projectile.GetComponent<Rigidbody>().AddForce(Vector3.up * gravity * Time.deltaTime);
                Destroy(projectile, timeToLive);
            }
        }

        angle += speed * Time.deltaTime;
        if (angle > 360)
        {
            angle = 0;
        }
    }
}
