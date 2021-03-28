using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] AudioSource restartSound;
    [SerializeField] GameObject continueScreen;
    

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
        continueScreen.SetActive(false);
        playerStartingpoint = player.transform.position;
        groundGenerationStartingPoint = groundPoolers.transform.position;
        gameOverScreen.SetActive(false);
    }

    private void Update()
    {
        if (player.isPlayerDead) GameOver();
        score.text = Mathf.Round(scoreManager.score).ToString();
        highScore.text = Mathf.Round(scoreManager.highScore).ToString();
    }

    public void GameOver()
    {
        player.gameObject.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        restartSound.Play();
        Invoke("DelayInRestart", 0.2f);
    }

    private void DelayInRestart()
    {
        continueScreen.SetActive(true);
        Time.timeScale = 0f;
        restartSound.Stop();
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
}
