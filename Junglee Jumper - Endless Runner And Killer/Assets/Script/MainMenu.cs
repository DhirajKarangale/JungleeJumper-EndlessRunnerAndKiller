using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject quitPanel;
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
