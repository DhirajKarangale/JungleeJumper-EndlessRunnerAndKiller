using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] float pointePerSecond;
    [SerializeField] Text scoreText;
    [SerializeField] Text highScoreText;
    public float score;
    private float highScore;

    private void Start()
    {
        highScore = PlayerPrefs.GetFloat("HighScore", 0f);
    }

    private void Update()
    {
        score += pointePerSecond * Time.deltaTime;

        if (score > highScore) highScore = score;
        PlayerPrefs.SetFloat("HighScore", highScore);

        scoreText.text = Mathf.Round(score).ToString();
        highScoreText.text = Mathf.Round(highScore).ToString();
    }
}
