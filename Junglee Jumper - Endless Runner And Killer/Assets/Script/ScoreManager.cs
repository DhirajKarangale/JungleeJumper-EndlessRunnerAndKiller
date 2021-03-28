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
    private bool isPause;

    private void Start()
    {
        highScore = PlayerPrefs.GetFloat("HighScore", 0f);
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if(!player.isPlayerDead && !isPause && player.playerRuning)
        score += pointePerSecond * Time.deltaTime;

        if (score > highScore) highScore = score;
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

    public void HomeButton(string sceneToLoad)
    {
        SceneManager.LoadScene(0);
    }
}
