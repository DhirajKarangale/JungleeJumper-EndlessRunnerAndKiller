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

    private bool isAdAllow;

    public static bool isGameStart;

    private void Start()
    {
        isAdAllow = true;
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
        PlayGamesController.Instance.SaveData();
        if (Social.localUser.authenticated) PlayGamesController.PostToLeaderboard(long.Parse(GameDataVariable.dataVariables[0].ToString()));
        player.runingSound.Stop();
        backGroundMusic.Stop();
        Player.isPlayerDead = true;
        scoreManagerObject.SetActive(false);
        Time.timeScale = 1f;
        player.gameObject.SetActive(false);
        gameOverScreen.SetActive(true);
       
        if (isAdAllow && (GameDataVariable.dataVariables[11] != 1))
        {
            if (UnityEngine.Random.Range(0, 5) == 3)
            {
                Invoke("ShowAD", 2);
            }
            
            isAdAllow = false;
        }
        if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene(0);
    }

    private void ShowAD()
    {
        AdManager.instance.ShowInterstitialAd();
    }
  
    public void Quit()
    {
        PlayGamesController.Instance.SaveData();
        if (Social.localUser.authenticated) PlayGamesController.PostToLeaderboard(long.Parse(GameDataVariable.dataVariables[0].ToString()));
        clickSound.Play();
        Application.Quit();
    }

    public void Restart()
    {
        PlayGamesController.Instance.SaveData();
        if (Social.localUser.authenticated) PlayGamesController.PostToLeaderboard(long.Parse(GameDataVariable.dataVariables[0].ToString()));
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
        PlayGamesController.Instance.SaveData();
        if (Social.localUser.authenticated) PlayGamesController.PostToLeaderboard(long.Parse(GameDataVariable.dataVariables[0].ToString()));
        Time.timeScale = 1f;
        Player.isPlayerDead = false;
        clickSound.Play();
        SceneManager.LoadScene(0);
    }
}
