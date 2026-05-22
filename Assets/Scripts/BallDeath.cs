using UnityEngine;

public class BallDeath : MonoBehaviour
{
    void OnCollisionEnter(
        Collision collision
    )
    {
        if (
            collision
            .gameObject
            .CompareTag(
                "Death"
            )
        )
        {
            GameManager
            .Instance
            .GameOver();
        }
    }
}