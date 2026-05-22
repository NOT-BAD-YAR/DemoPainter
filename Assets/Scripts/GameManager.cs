using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float fallLimit = -5f;

    public Transform player;

    public GameObject gameOverPanel;

    public GameObject levelCompletePanel;

    public bool gameEnded = false;

    Rigidbody playerRB;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (
            player
            !=
            null
        )
        {
            playerRB =
                player
                .GetComponent<Rigidbody>();
        }

        if (
            gameOverPanel
            !=
            null
        )
        {
            gameOverPanel
                .SetActive(
                    false
                );
        }

        if (
            levelCompletePanel
            !=
            null
        )
        {
            levelCompletePanel
                .SetActive(
                    false
                );
        }
    }

    void Update()
    {
        if (
            gameEnded
        )
        {
            return;
        }

        if (
            player
            ==
            null
        )
        {
            return;
        }

        if (
            player
            .position
            .y
            <
            fallLimit
        )
        {
            Debug.Log(
                "Ball Fell"
            );

            GameOver();
        }
    }

    public void GameOver()
    {
        if (
            gameEnded
        )
        {
            return;
        }

        gameEnded =
            true;

        Debug.Log(
            "GAME OVER"
        );

        FreezeGame();

        if (
            gameOverPanel
            !=
            null
        )
        {
            gameOverPanel
                .SetActive(
                    true
                );
        }
    }

    public void LevelComplete()
    {
        if (
            gameEnded
        )
        {
            return;
        }

        gameEnded =
            true;

        Debug.Log(
            "LEVEL COMPLETE"
        );

        FreezeGame();

        if (
            levelCompletePanel
            !=
            null
        )
        {
            levelCompletePanel
                .SetActive(
                    true
                );
        }
    }

    void FreezeGame()
    {
        Time.timeScale =
            0;

        if (
            playerRB
            !=
            null
        )
        {
            playerRB.linearVelocity =
                Vector3.zero;

            playerRB.angularVelocity =
                Vector3.zero;

            playerRB.isKinematic =
                true;
        }
    }

    public void RestartLevel()
    {
        Time.timeScale =
            1;

        SceneManager
            .LoadScene(
                SceneManager
                .GetActiveScene()
                .buildIndex
            );
    }
}