using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class ScoreManager : MonoBehaviour
{
    private float pointePerSecond;
    [SerializeField] Text scoreText;
    [SerializeField] Text highScoreText;
    [SerializeField] Player player;
    [SerializeField] AudioSource clickSound;
    [SerializeField] AudioSource backGroundMusic;
    public float score;
    [SerializeField] GameObject scoreCanvas;
    [SerializeField] GameObject pauseScreen;
    public static bool isPause;
    [SerializeField] Text coinText;
    public Text scoreDescreaseText;


    [Header("Coin Collect")]
    [SerializeField] float speed;
    [SerializeField] Transform target;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] Camera cam;
    private int coinPickPoints;
    private int coin;
    private float highScore;



   

    private void Start()
    {
        if (GameDataVariable.dataVariables[5] == 2)
        {
            if(GameDataVariable.dataVariables[9] == 1)
            {
                pointePerSecond = 2.1f;
            }
            else
            {
                pointePerSecond = 0.7f;
            }
            if(GameDataVariable.dataVariables[8] == 1)
            {
                coinPickPoints = 6;
            }
            else
            {
                coinPickPoints = 2;
            }
        }
        else
        {
           if(GameDataVariable.dataVariables[9] == 1)
           {
                pointePerSecond = 1.5f;
           }
            else
            {
                pointePerSecond = 0.5f;
            }
            if (GameDataVariable.dataVariables[8] == 1)
            {
                coinPickPoints = 3;
            }
            else
            {
                coinPickPoints = 1;
            }
        }
        scoreDescreaseText.gameObject.SetActive(false);
        pauseScreen.SetActive(false);
        backGroundMusic.Play();
        if (cam == null)
        {
            cam = Camera.main;
        }

        coin = GameDataVariable.dataVariables[1];
        highScore = float.Parse(GameDataVariable.dataVariables[0].ToString());
    }

    private void Update()
    {
        if(!Player.isPlayerDead && !isPause && player.playerRuning )
        {
            score += pointePerSecond * Time.deltaTime * player.speed;
        }

        if (score > highScore)
        {
           highScore = score;
           GameDataVariable.dataVariables[0] = Convert.ToInt32(highScore);
            PlayGamesController.Instance.SaveData();
            if (Social.localUser.authenticated) PlayGamesController.PostToLeaderboard(long.Parse(GameDataVariable.dataVariables[0].ToString()));
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause) ResumeButton();
            else PauseButton();
        }

        if(CoinCollector.isCoinHit)
        {
            StartCoinMove(CoinCollector.coinPosition.position, ()=> {coin += coinPickPoints;
                GameDataVariable.dataVariables[1] = coin;
                PlayGamesController.Instance.SaveData();
            });
            CoinCollector.isCoinHit = false;
        }
        
       
        if (score >= 1000)
        {
            scoreText.text = string.Format("{0}.{1}K", Convert.ToInt32((score / 1000)), int.Parse(((score%1000)/100).ToString()[0].ToString()));
        }
        else
        {
            scoreText.text = Mathf.Round(score).ToString();
        }
        if (highScore >= 1000)
        {
            highScoreText.text = string.Format("{0}.{1}K", Convert.ToInt32((highScore / 1000)), int.Parse(((highScore%1000)/100).ToString()[0].ToString()));

        }
        else
        {
            highScoreText.text = Convert.ToInt32(highScore).ToString();
        }

        if (coin >= 1000)
        {
            coinText.text = string.Format("{0}.{1}K", Convert.ToInt32((coin / 1000)), int.Parse(((coin%1000)/100).ToString()[0].ToString()));
        }
        else
        {
            coinText.text = coin.ToString();
        }


        if (Player.isPlayerDead)
        {
            GameDataVariable.dataVariables[0] = Convert.ToInt32(highScore);
            GameDataVariable.dataVariables[1] = coin;
            PlayGamesController.Instance.SaveData();
            if (Social.localUser.authenticated) PlayGamesController.PostToLeaderboard(long.Parse(GameDataVariable.dataVariables[0].ToString()));
        }
    }

   

    private void StartCoinMove(Vector3 initial, Action onComplete)
    {
        Vector3 targetPos = cam.ScreenToWorldPoint(new Vector3(target.position.x, target.position.y, cam.transform.position.z * -1));
        GameObject _coin = Instantiate(coinPrefab, transform);
        StartCoroutine(MoveCoin(_coin.transform, initial, targetPos, onComplete));
    }

    IEnumerator MoveCoin(Transform obj, Vector3 startPos, Vector3 endPos, Action onComplete)
    {
        float time = 0;
        while (time < 1)
        {
            time += speed * Time.deltaTime;
            obj.position = Vector3.Lerp(startPos, endPos, time);
            yield return new WaitForEndOfFrame();
        }
        onComplete.Invoke();
        Destroy(obj.gameObject);
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
        Time.timeScale = 1f;
        PlayGamesController.Instance.SaveData();
        if (Social.localUser.authenticated) PlayGamesController.PostToLeaderboard(long.Parse(GameDataVariable.dataVariables[0].ToString()));
        clickSound.Play();
        SceneManager.LoadScene(0);
        ResumeButton();
    }

    public void RestartButton()
    {
        PlayGamesController.Instance.SaveData();
        if (Social.localUser.authenticated) PlayGamesController.PostToLeaderboard(long.Parse(GameDataVariable.dataVariables[0].ToString()));
        clickSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResumeButton();
    }

    public void DesableText()
    {
        Invoke("ScoreDecreaseTestFalse", 1);
    }

    private void ScoreDecreaseTestFalse()
    {
        scoreDescreaseText.gameObject.SetActive(false);
    }
}
