using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Player player;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] AudioSource restartSound;
    [SerializeField] AudioSource backGroundMusic;
    [SerializeField] GameObject continueScreen;

    [SerializeField] GameObject gameOverScreen;
    [SerializeField] Text score;
    [SerializeField] Text highScore;

    [SerializeField] AudioSource playerFireballHitSound;
    [SerializeField] AudioSource zombieFireballHitSound;

    public static bool isGameStart;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Time.timeScale = 0f;
        continueScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        player.isPlayerDead = false;
        isGameStart = false;
    }

    private void Update()
    {
        if (player.isPlayerDead) Invoke("GameOver", 0.5f);
        score.text = Mathf.Round(scoreManager.score).ToString();
        highScore.text = Mathf.Round(scoreManager.highScore).ToString();
        if (scoreManager.isPause) continueScreen.SetActive(false);

        if(PlayerFireball.playerFireballHitObject)
        {
            if (playerFireballHitSound.isPlaying) playerFireballHitSound.Stop();
            playerFireballHitSound.Play();
            PlayerFireball.playerFireballHitObject = false;
        }

        if(ZombieFireball.zombieFireballHitObject)
        {
            if (zombieFireballHitSound.isPlaying) zombieFireballHitSound.Stop();
            zombieFireballHitSound.Play();
            ZombieFireball.zombieFireballHitObject = false;
        }
    }

    public void GameOver()
    {
        backGroundMusic.Stop();
        player.isPlayerDead = true;
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
        player.gameOverSound.Stop();
        restartSound.Play();
        player.isPlayerDead = false;
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
        isGameStart = true; 
    }

    public void HomeButton()
    {
        SceneManager.LoadScene(0);
    }
}
