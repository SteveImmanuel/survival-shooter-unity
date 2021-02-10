using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreManager : MonoBehaviour
{
    public static int score;

    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Start()
    {
        score = 0;
    }

    void Update()
    {
        text.text = "Score: " + score;
    }
}
