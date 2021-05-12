using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] GameObject quitPanel;
    [SerializeField] AudioSource buttonPressSound;
    [SerializeField] Text highScoreCount;
    [SerializeField] Text coinCount;
    private bool isQuitPanelActive;

    private void Start()
    {
        coinCount.text = PlayerPrefs.GetInt("Coin", 0).ToString();
        highScoreCount.text = Mathf.Round(PlayerPrefs.GetFloat("HighScore", 0f)).ToString();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            if (isQuitPanelActive) DesableQuitPanel();
            else SetQuitPanel();
        }
    }

    public void StartButton(string sceneToLoad)
    {
        buttonPressSound.Play();
        rigidBody.velocity = new Vector2(16, rigidBody.velocity.y);
        FindObjectOfType<SceneFader>().FadeTo(sceneToLoad);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void LeaderBoardButton()
    {
        PlayGamesController.ShowLeaderboardUI();
    }

    private void SetQuitPanel()
    {
        quitPanel.SetActive(true);
        isQuitPanelActive = true;
    }

    public void DesableQuitPanel()
    {
        quitPanel.SetActive(false);
        isQuitPanelActive = false;
    }
}
