using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

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
        AdManager.instance.RequestInterstitial();
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (Player.isPlayerDead) Invoke("GameOver", 0.5f);
        if (scoreManager.score >= 1000)
        {
            score.text = string.Format("{0}.{1}K", Convert.ToInt32((scoreManager.score / 1000)), int.Parse(((scoreManager.score%1000)/100).ToString()[0].ToString()));
        }
        else
        {
            score.text = Mathf.Round(scoreManager.score).ToString();
        }
        if (GameDataVariable.dataVariables[0] >= 1000)
        {
            highScore.text = string.Format("{0}.{1}K", Convert.ToInt32((GameDataVariable.dataVariables[0] / 1000)), int.Parse(((GameDataVariable.dataVariables[0]%1000)/100).ToString()[0].ToString()));

        }
        else
        {
            highScore.text = GameDataVariable.dataVariables[0].ToString();
        }

        if (GameDataVariable.dataVariables[1] >= 1000)
        {
            coin.text = string.Format("{0}.{1}K", Convert.ToInt32((GameDataVariable.dataVariables[1] / 1000)), int.Parse(((GameDataVariable.dataVariables[1]%1000)/100).ToString()[0].ToString()));
        }
        else
        {
            coin.text = GameDataVariable.dataVariables[1].ToString();

        }
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
        player.runingSound.Stop();
        backGroundMusic.Stop();
        Player.isPlayerDead = true;
        scoreManagerObject.SetActive(false);
        Time.timeScale = 1f;
        player.gameObject.SetActive(false);
        gameOverScreen.SetActive(true);
        if(UnityEngine.Random.Range(0,3) == 0) AdManager.instance.ShowInterstitialAd();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Continue()
    {
        Time.timeScale = 1f;
        continueScreen.SetActive(false);
        isGameStart = true; 
    }

    public void HomeButton()
    {
        Time.timeScale = 1f;
        Player.isPlayerDead = false;
        clickSound.Play();
        SceneManager.LoadScene(0);
    }
}
