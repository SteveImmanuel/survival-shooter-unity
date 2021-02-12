using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text scoreText;
    public Slider healthSlider;

    public static UIController instance;
    private int currentScore = 0;

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
}
