using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyAI : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Movement Settings")]
    public float maxSpeed = 5f;
    public float maxForce = 10f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevents enemy from tipping over
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 steering = Seek(target.position);
            rb.velocity += steering * Time.fixedDeltaTime;

            // Limit speed
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

            // Face the movement direction
            if (rb.velocity != Vector3.zero)
                transform.forward = rb.velocity.normalized;
        }
    }

    Vector3 Seek(Vector3 targetPosition)
    {
        Vector3 desired = (targetPosition - transform.position).normalized * maxSpeed;
        Vector3 steer = desired - rb.velocity;

        // Limit the steering force
        return Vector3.ClampMagnitude(steer, maxForce);
    }
}
