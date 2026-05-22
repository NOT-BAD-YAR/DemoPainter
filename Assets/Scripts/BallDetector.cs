using UnityEngine;

public class BallDetector : MonoBehaviour
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

        if (
            collision
            .gameObject
            .CompareTag(
                "Finish"
            )
        )
        {
            GameManager
            .Instance
            .LevelComplete();
        }
    }
}