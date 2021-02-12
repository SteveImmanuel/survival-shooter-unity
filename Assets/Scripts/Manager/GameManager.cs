using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float restartDelay = 5f;            

    private bool isGameOver = false;

    void Update()
    {
        if (PlayerHealth.currentHealth <= 0 && !isGameOver)
        {
            isGameOver = true;
            UIController.instance.GameOver();
            Invoke(nameof(RestartGame), restartDelay);
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}