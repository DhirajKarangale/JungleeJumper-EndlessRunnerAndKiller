using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] float pointePerSecond;
    [SerializeField] Text scoreText;
    [SerializeField] Text highScoreText;
    [SerializeField] Player player;
    public float score;
    public float highScore;
    [SerializeField] GameObject scoreCanvas;
    [SerializeField] GameObject pauseScreen;
    public bool isPause;

    private void Start()
    {
        highScore = PlayerPrefs.GetFloat("HighScore", 0f);
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if(!Player.isPlayerDead && !isPause && player.playerRuning && !Player.isPlayerHitObstacles)
        {
            score += pointePerSecond * Time.deltaTime ;
        }

        if (score > highScore)
        {
            highScore = score;
        }
        PlayerPrefs.SetFloat("HighScore", highScore);

        scoreText.text = Mathf.Round(score).ToString();
        highScoreText.text = Mathf.Round(highScore).ToString();

        if(Input.GetKey(KeyCode.Escape))
        {
            if (isPause) ResumeButton();
            else PauseButton();
        }
    }

    public void PauseButton()
    {
        isPause = true;
        Time.timeScale = 0f;
        scoreCanvas.SetActive(false);
        pauseScreen.SetActive(true);
    }

    public void ResumeButton()
    {
        isPause = false;
        Time.timeScale = 1f;
        scoreCanvas.SetActive(true);
        pauseScreen.SetActive(false);
    }

    public void HomeButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(1);
    }
}
