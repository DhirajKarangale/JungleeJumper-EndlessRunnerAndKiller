using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;
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
    [SerializeField] GameObject scoreCanvas;
    [SerializeField] GameObject pauseScreen;
    public static bool isPause;
    [SerializeField] Text coinText;
    [SerializeField] GameObject scoreDecreseTextObject;
    [SerializeField] Text scoreDescreaseText;


    [Header("Coin Collect")]
    [SerializeField] float speed;
    [SerializeField] Transform target;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] Camera cam;
    private int coinPickPoints = 2;



   

    private void Start()
    {
        scoreDecreseTextObject.SetActive(false);
        pauseScreen.SetActive(false);
        backGroundMusic.Play();
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    private void Update()
    {
        if(!Player.isPlayerDead && !isPause && player.playerRuning && !player.isPlayerHitObstacles)
        {
            score += pointePerSecond * Time.deltaTime * player.speed;
        }

        if (score > GPGCSaving.highScore)
        {
            GPGCSaving.highScore = score;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause) ResumeButton();
            else PauseButton();
        }

        if(CoinCollector.isCoinHit)
        {
            StartCoinMove(player.transform.position, ()=> { GPGCSaving.coin += coinPickPoints;});
            CoinCollector.isCoinHit = false;
        }
        
        if(PlayerFireball.playerFireballCollideWithCutter)
        {
            scoreDescreaseText.color = Color.red;
            scoreDescreaseText.text = "-3";
            scoreDecreseTextObject.SetActive(true);
            Invoke("ScoreDecreaseTestFalse", 1f);
        }
        else if(PlayerFireball.playerFireballCollideWithVerticalCutter)
        {
            scoreDescreaseText.color = Color.red;
            scoreDescreaseText.text = "-7";
            scoreDecreseTextObject.SetActive(true);
            Invoke("ScoreDecreaseTestFalse", 1f);
        }
        else if (PlayerFireball.playerFireballCollideZombie)
        {
            scoreDescreaseText.color = Color.green;
            scoreDescreaseText.text = "+15";
            scoreDecreseTextObject.SetActive(true);
            Invoke("ScoreDecreaseTestFalse", 1f);
        }

        scoreText.text = Mathf.Round(score).ToString();
        highScoreText.text = Mathf.Round(GPGCSaving.highScore).ToString();
        coinText.text = GPGCSaving.coin + "";
    }

    private void ScoreDecreaseTestFalse()
    {
        scoreDecreseTextObject.SetActive(false);
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
        clickSound.Play();
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void RestartButton()
    {
        clickSound.Play();
        SceneManager.LoadScene(1);
        ResumeButton();
    }
}
