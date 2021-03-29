using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] GameObject quitPanel;
    [SerializeField] AudioSource buttonPressSound;
    private bool isQuitPanelActive;

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
