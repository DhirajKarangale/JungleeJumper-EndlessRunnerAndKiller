using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] AudioSource restartSound;
    [SerializeField] AudioSource backGroundMusic;
    [SerializeField] GameObject continueScreen;

    [SerializeField] GameObject gameOverScreen;
    [SerializeField] Text score;
    [SerializeField] Text highScore;

    private void Start()
    {
        Time.timeScale = 0f;
        continueScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        Player.isPlayerDead = false;
    }

    private void Update()
    {
        if (Player.isPlayerDead) Invoke("GameOver", 0.5f);
        score.text = Mathf.Round(scoreManager.score).ToString();
        highScore.text = Mathf.Round(scoreManager.highScore).ToString();
    }

    public void GameOver()
    {
        backGroundMusic.Stop();
        Player.isPlayerDead = true;
        Time.timeScale = 1f;
        player.gameObject.SetActive(false);
        gameOverScreen.SetActive(true);
        if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        player.deathSound.Stop();
        restartSound.Play();
        Player.isPlayerDead = false;
        Invoke("DelayInRestart", 0.2f);
    }

    private void DelayInRestart()
    {
        backGroundMusic.Play();
        SceneManager.LoadScene(1);
    }
    public void Continue()
    {
        Time.timeScale = 1f;
        continueScreen.SetActive(false);
    }

    public void HomeButton()
    {
        SceneManager.LoadScene(0);
    }
}
