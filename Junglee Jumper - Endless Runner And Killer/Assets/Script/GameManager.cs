using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] AudioSource restartSound;
    [SerializeField] GameObject continueScreen;
    [SerializeField] ScrowllingBackGround scrowlling;
    

    private Vector3 playerStartingpoint;
    private Vector3 groundGenerationStartingPoint;

    [SerializeField] GroundPoolers groundPoolers;

    [SerializeField] GameObject smallGround;
    [SerializeField] GameObject largeGround;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject inGameUI;
    [SerializeField] Text score;
    [SerializeField] Text highScore;

    private void Start()
    {
        Time.timeScale = 0f;
        continueScreen.SetActive(true);
        playerStartingpoint = player.transform.position;
        groundGenerationStartingPoint = groundPoolers.transform.position;
        gameOverScreen.SetActive(false);
    }

    private void Update()
    {
        if (player.isPlayerDead) Invoke("GameOver", 0.5f);
        score.text = Mathf.Round(scoreManager.score).ToString();
        highScore.text = Mathf.Round(scoreManager.highScore).ToString();
    }

    public void GameOver()
    {
        scoreManager.enabled = false;
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
        scoreManager.enabled = true;
        player.deathSound.Stop();
        restartSound.Play();
        Invoke("DelayInRestart", 0.2f);
    }

    private void DelayInRestart()
    {
        continueScreen.SetActive(true);
        Time.timeScale = 0f;
        restartSound.Stop();
        scrowlling.backGroundSpeed = 0.2f;
        player.runingSpeedAnim = 1f;
        player.isPlayerDead = false;
        GroundDestroyer[] groundDestroyer = FindObjectsOfType<GroundDestroyer>();
        for (int i = 0; i < groundDestroyer.Length; i++)
        {
            groundDestroyer[i].gameObject.SetActive(false);
        }
        player.speed = player.originalSpeed;
        smallGround.SetActive(true);
        largeGround.SetActive(true);
        player.transform.position = playerStartingpoint;
        groundPoolers.transform.position = groundGenerationStartingPoint;
        scoreManager.score = 0;
        player.gameObject.SetActive(true);
        gameOverScreen.SetActive(false);
        inGameUI.SetActive(true);
    }
    public void Continue()
    {
        Time.timeScale = 1f;
        continueScreen.SetActive(false);
    }

    public void HomeButton(string sceneToLoad)
    {
        SceneManager.LoadScene(0);
    }
}
