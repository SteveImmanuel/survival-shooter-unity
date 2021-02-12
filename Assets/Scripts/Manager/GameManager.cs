using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float restartDelay = 5f;
    public GameObject mainVcam;
    public GameObject deathCam;
    private bool isGameOver = false;

    void Update()
    {
        if (PlayerHealth.currentHealth <= 0 && !isGameOver)
        {
            isGameOver = true;
        }
    }
}