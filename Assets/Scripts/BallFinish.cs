using UnityEngine;

public class BallFinish : MonoBehaviour
{
    void OnCollisionEnter(
        Collision collision
    )
    {
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