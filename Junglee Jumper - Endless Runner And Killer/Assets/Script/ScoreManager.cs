using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] float pointePerSecond;
    [SerializeField] Text scoreText;
    [SerializeField] Text highScoreText;
    [SerializeField] Player player;
    [SerializeField] AudioSource clickSound;
    [SerializeField] AudioSource backGroundMusic;
    public float score;
    public float highScore;
    [SerializeField] GameObject scoreCanvas;
    [SerializeField] GameObject pauseScreen;
    public static bool isPause;

    private void Start()
    {
        highScore = PlayerPrefs.GetFloat("HighScore", 0f);
        pauseScreen.SetActive(false);
        backGroundMusic.Play();
    }

    private void Update()
    {
        if(!player.isPlayerDead && !isPause && player.playerRuning && !player.isPlayerHitObstacles)
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

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause) ResumeButton();
            else PauseButton();
        }
    }

    public void PauseButton()
    {
        backGroundMusic.Stop();
        clickSound.Play();
        isPause = true;
        Time.timeScale = 0f;
        scoreCanvas.SetActive(false);
        pauseScreen.SetActive(true);
    }

    public void ResumeButton()
    {
        backGroundMusic.Play();
        clickSound.Play();
        isPause = false;
        Time.timeScale = 1f;
        scoreCanvas.SetActive(true);
        pauseScreen.SetActive(false);
    }

    public void HomeButton()
    {
        clickSound.Play();
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void RestartButton()
    {
        clickSound.Play();
        SceneManager.LoadScene(1);
    }
}
