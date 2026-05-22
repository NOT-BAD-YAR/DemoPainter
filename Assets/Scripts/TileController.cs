using UnityEngine;

public class TileController : MonoBehaviour
{
    private Renderer rend;

    private Rigidbody currentBall;

    private int state;

    public int initialState = 0;

    public float forceStrength = 2.5f;

    public Material greyMat;
    public Material redMat;
    public Material yellowMat;
    public Material blueMat;

    void Start()
    {
        rend =
            GetComponent<Renderer>();

        if (
            CompareTag(
                "Death"
            )
            ||
            CompareTag(
                "Finish"
            )
        )
        {
            Debug.Log(
                "Special Tile Loaded"
            );

            return;
        }

        state =
            Mathf.Clamp(
                initialState,
                0,
                3
            );

        UpdateColor();

        Debug.Log(
            gameObject.name
            +
            " Initial State → "
            +
            state
        );
    }

    void OnMouseDown()
    {
        if (
            GameManager
            .Instance
            !=
            null
        )
        {
            if (
                GameManager
                .Instance
                .gameEnded
            )
            {
                Debug.Log(
                    "Game Locked"
                );

                return;
            }
        }

        if (
            CompareTag(
                "Death"
            )
            ||
            CompareTag(
                "Finish"
            )
        )
        {
            Debug.Log(
                "Special Tile Locked"
            );

            return;
        }

        state++;

        if (
            state > 3
        )
        {
            state = 0;
        }

        UpdateColor();

        ApplyForce();
    }

    void UpdateColor()
    {
        if (
            rend
            ==
            null
        )
        {
            return;
        }

        switch (state)
        {
            case 0:
                rend.material =
                    greyMat;
                break;

            case 1:
                rend.material =
                    redMat;
                break;

            case 2:
                rend.material =
                    yellowMat;
                break;

            case 3:
                rend.material =
                    blueMat;
                break;
        }
    }

    void OnCollisionEnter(
        Collision collision
    )
    {
        if (
            collision
            .gameObject
            .CompareTag(
                "Player"
            )
        )
        {
            currentBall =
                collision
                .gameObject
                .GetComponent<Rigidbody>();

            Debug.Log(
                "Ball Entered"
            );

            if (
                state
                !=
                0
            )
            {
                ApplyForce();
            }
        }
    }

    void OnCollisionExit(
        Collision collision
    )
    {
        if (
            collision
            .gameObject
            .CompareTag(
                "Player"
            )
        )
        {
            currentBall =
                null;

            Debug.Log(
                "Ball Left"
            );
        }
    }

    void ApplyForce()
    {
        if (
            currentBall
            ==
            null
        )
        {
            return;
        }

        Vector3 velocity =
            currentBall.linearVelocity;

        velocity.y =
            0;

        float speed =
            Mathf.Max(
                velocity.magnitude,
                3f
            );

        Vector3 direction =
            velocity.normalized;

        if (
            direction
            ==
            Vector3.zero
        )
        {
            direction =
                Vector3.forward;
        }

        Vector3 target =
            direction;

        // RED
        if (
            state == 1
        )
        {
            Vector3 straight =
                Vector3.forward;

            float straighten =
                Mathf.Lerp(
                    0.7f,
                    1f,
                    Mathf.Abs(
                        direction.x
                    )
                );

            target =
                Vector3
                .Slerp(
                    direction,
                    straight,
                    straighten
                );

            speed *= 1.3f;
        }

        // YELLOW
        // YELLOW
        if (
            state == 2
        )
        {
            Vector3 leftTarget =
                new Vector3(
                    -6f,
                    0,
                    1f
                ).normalized;

            target =
                Vector3
                .Slerp(
                    direction,
                    leftTarget,
                    1f
                );

            speed *= 1.5f;
        }

        // BLUE
        if (
            state == 3
        )
        {
            Vector3 rightTarget =
                new Vector3(
                    6f,
                    0,
                    1f
                ).normalized;

            target =
                Vector3
                .Slerp(
                    direction,
                    rightTarget,
                    1f
                );

            speed *= 1.5f;
        }

        target.Normalize();

        Vector3 newVelocity =
            Vector3.Lerp(
                velocity,

                target
                *
                (
                    speed
                    +
                    forceStrength
                ),

                0.9f
            );

        currentBall.linearVelocity =
            new Vector3(
                newVelocity.x,
                currentBall.linearVelocity.y,
                newVelocity.z
            );

        Debug.Log(
            "Movement Applied"
        );
    }
}