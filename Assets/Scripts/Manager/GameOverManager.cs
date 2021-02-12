using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class GameOverManager : MonoBehaviour
{
    public float restartDelay = 5f;            

    private Animator anim;                          
    private bool isGameOver = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (PlayerHealth.currentHealth <= 0 && !isGameOver)
        {
            isGameOver = true;
            anim.SetTrigger("GameOver");
            Invoke(nameof(RestartGame), restartDelay);
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}