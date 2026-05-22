using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float maxPlanarSpeed = 5f;

    public bool dampWhenNoZone = true;

    public float idlePlanarDamping = 2f;

    Rigidbody rb;

    void Start()
    {
        rb =
            GetComponent<Rigidbody>();

        Debug.Log(
            "BallMovement Ready"
        );
    }

    void FixedUpdate()
    {
        LimitSpeed();

        ApplyIdleDamping();
    }

    void LimitSpeed()
    {
        Vector3 velocity =
            rb.linearVelocity;

        velocity.y = 0;

        if (
            velocity.magnitude
            >
            maxPlanarSpeed
        )
        {
            Vector3 limited =
                velocity
                .normalized
                *
                maxPlanarSpeed;

            rb.linearVelocity =
                new Vector3(
                    limited.x,
                    rb.linearVelocity.y,
                    limited.z
                );

            Debug.Log(
                "Speed Limited"
            );
        }
    }

    void ApplyIdleDamping()
    {
        if (
            dampWhenNoZone
            ==
            false
        )
        {
            return;
        }

        Vector3 planar =
            rb.linearVelocity;

        planar.y = 0;

        rb.AddForce(
            -planar
            *
            idlePlanarDamping
            *
            Time.fixedDeltaTime,

            ForceMode.Acceleration
        );
    }
}