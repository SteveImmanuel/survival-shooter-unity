using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class UIController : MonoBehaviour
{
    public Text scoreText;
    public Slider healthSlider;

    public static UIController instance;
    private int currentScore = 0;
    private Animator animator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        animator = GetComponent<Animator>();
    }

    public void AddScore(int score)
    {
        currentScore += score;
        scoreText.text = "Score: " + currentScore;
    }

    public void SetHealthSlider(float value)
    {
        healthSlider.value = value;
    }

    public void GameOver()
    {
        animator.SetTrigger("GameOver");
    }
}
