using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Player player;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] GameObject scoreManagerObject;
    [SerializeField] AudioSource restartSound;
    [SerializeField] AudioSource backGroundMusic;
    [SerializeField] GameObject continueScreen;

    [SerializeField] GameObject gameOverScreen;
    [SerializeField] Text score;
    [SerializeField] Text highScore;
    [SerializeField] Text coin;


    [SerializeField] AudioSource playerFireballHitSound;
    [SerializeField] AudioSource zombieFireballHitSound;
    [SerializeField] AudioSource clickSound;

    public static bool isGameStart;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameOverScreen.SetActive(false);
        Player.isPlayerDead = false;
        isGameStart = false;
        scoreManagerObject.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (Player.isPlayerDead) Invoke("GameOver", 0.5f);
        score.text = Mathf.Round(scoreManager.score).ToString();
        highScore.text = GameDataVariable.dataVariables[0].ToString();
        coin.text = GameDataVariable.dataVariables[1].ToString();
        if (ScoreManager.isPause) continueScreen.SetActive(false);

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
        Player.isPlayerDead = true;
        scoreManagerObject.SetActive(false);
        Time.timeScale = 1f;
        player.gameObject.SetActive(false);
        gameOverScreen.SetActive(true);
        if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene(0);
    }
  
    public void Quit()
    {
        clickSound.Play();
        Application.Quit();
    }

    public void Restart()
    {
        player.gameOverSound.Stop();
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
        isGameStart = true; 
    }

    public void HomeButton()
    {
        Player.isPlayerDead = false;
        clickSound.Play();
        SceneManager.LoadScene(0);
    }
}
