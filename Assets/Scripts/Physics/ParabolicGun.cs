// using System;
// using UnityEngine;

// public class ParabolicGun : MonoBehaviour
// {
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     public float speed = 10;
//     public float angle;
//     public GameObject projectilePrefab;
//     public Transform firePoint;
//     public float gravity = -9.81f;
//     public float timeToLive = 5f;
//     public float maxDistance = 100f;
//     public float maxHeight = 50f;
//     public Vector3 initialVelocity;
//     public Vector3 velocity;
//     public Vector3 initialPosition;
//     public Vector3 position;
//     public float globalTime = 0f;
//     public float time = 0f;



//     void Start()
//     {
//         angle = Mathf.Atan2(initialVelocity.y, initialVelocity.x) * Mathf.Rad2Deg;
//         Debug.Log(angle);
//         initialPosition = firePoint.position;
//         velocity = new Vector3(initialVelocity.x, initialVelocity.y, initialVelocity.z);

//     }

//     // Update is called once per frame
//     void Update()
//     {
//         parabolicMove();

//     }
//     void parabolicMove()
//     {
//         time += Time.deltaTime;
//         float x = initialPosition.x + initialVelocity.x * time;
//         float y = initialPosition.y + initialVelocity.y * time + 0.5f * gravity * time * time;
//         float z = initialPosition.z + initialVelocity.z * time;

//         position = new Vector3(x, y, z);
//         velocity = new Vector3(velocity.x, velocity.y + gravity * time, velocity.z);
//         initialPosition = position;


//         if (position.y > maxHeight)
//         {
//             position.y = maxHeight;
//             velocity.y = 0;
//         }
//         else if (position.y < 0)
//         {
//             position.y = 0;
//             velocity.y = 0;
//         }

//         if (position.x > maxDistance)
//         {
//             position.x = maxDistance;
//             velocity.x = 0;
//         }
//         else if (position.x < 0)
//         {
//             position.x = 0;
//             velocity.x = 0;
//         }

//         transform.position = position;
//         firePoint.rotation = Quaternion.Euler(0, angle, 0);

//         DrawTrajectory(position, velocity);
//     }

//     void DrawTrajectory(Vector3 start, Vector3 initialVelocity)
//     {
//         Vector3[] points = new Vector3[100];

//         for (int i = 0; i < 100; i++)
//         {
//             float time = (i / (float)100) * timeToLive;
//             points[i] = start + initialVelocity * time + 0.5f * new Vector3(0, gravity, 0) * time * time;
//         }

//         GetComponent<LineRenderer>().SetPositions(points);
//     }
//     void OnCollisionEnter(Collision collision)
//     {
        
//     }
// }


using UnityEngine;

public class ParabolicGun : MonoBehaviour
{

    public GameObject projectilePrefab;
    public Transform firePoint;
    public float gravity = -9.81f;
    public float timeToLive = 5f;
    public float bounciness = 0.6f; // 0.6 = lose 40% energy per bounce
    public int maxBounces = 3;

    public Vector3 initialVelocity;
    private Vector3 position;
    private Vector3 velocity;
    private float elapsedTime = 0f;
    private float lifeTimer = 0f;
    private int bounceCount = 0;



    void Start()
    {
        // Calculate initial velocity from angle and speed
        //float radAngle = Mathf.Deg2Rad * angle;
        float radAngle = Mathf.Atan2(initialVelocity.y, initialVelocity.x); // Ensure angle is between 0 and 90 degrees
        //Debug.Log("ParabolicGun radAngle"+radAngle);
        Vector3 dir = firePoint.forward;
        //initialVelocity = new Vector3(dir.x * speed * Mathf.Cos(radAngle), speed * Mathf.Sin(radAngle), dir.z * speed * Mathf.Cos(radAngle));
        
        velocity = initialVelocity;
        position = firePoint.position;


    }

    void Update()
    {
        lifeTimer += Time.deltaTime;
        if (lifeTimer > timeToLive)
        {
            Destroy(gameObject);
            return;
        }

        ParabolicMove();
    }

    void ParabolicMove()
    {
        float deltaTime = Time.deltaTime;
        elapsedTime += deltaTime;

        // Apply gravity
        velocity.y += gravity * deltaTime;

        // Calculate next position
        Vector3 nextPosition = position + velocity * deltaTime;

        // Simple collision with ground (y = 0)
        if (nextPosition.y <= 0f && velocity.y < 0f)
        {
            nextPosition.y = 0f;
            velocity.y = -velocity.y * bounciness;
            velocity.x *= bounciness;
            velocity.z *= bounciness;

            bounceCount++;
            if (bounceCount >= maxBounces || velocity.magnitude < 1f)
            {
                velocity = Vector3.zero;
                return;
            }
        }

        position = nextPosition;
        transform.position = position;
    }


}
