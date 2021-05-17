using UnityEngine;
using UnityEngine.UI;
using System;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject profilePanel;
    [SerializeField] GameObject quitPanel;
    [SerializeField] GameObject shopCanvas;
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject groundObject;
    [SerializeField] GameObject quadBGObject;
    [SerializeField] AudioSource startButtonSound;
    [SerializeField] AudioSource buttonSound;
    [SerializeField] Text highScoreCount;
    [SerializeField] Text coinCount;
    private bool isQuitPanelActive,isShopActive;
    public static bool isProfilePanelActive;
   
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && !isProfilePanelActive)
        {
            if (isQuitPanelActive) DesableQuitPanel();
            else SetQuitPanel();
        }

        if(Input.GetKey(KeyCode.Escape) && isProfilePanelActive)
        {
            CloseProfileButton();
        }

        if(Input.GetKey(KeyCode.Escape) && isShopActive)
        {
            CloseShopButton();
        }

        if (GameDataVariable.dataVariables[0] >= 1000)
        {
            highScoreCount.text = string.Format("{0}.{1}K", Convert.ToInt32((GameDataVariable.dataVariables[0] / 1000)), int.Parse(((GameDataVariable.dataVariables[0]%1000)/100).ToString()[0].ToString()));

        }
        else
        {
            highScoreCount.text = GameDataVariable.dataVariables[0].ToString();
        }

        if(GameDataVariable.dataVariables[1] >= 1000)
        {
            coinCount.text = string.Format("{0}.{1}K", Convert.ToInt32((GameDataVariable.dataVariables[1] / 1000)), int.Parse(((GameDataVariable.dataVariables[1]%1000)/100).ToString()[0].ToString()));
        }
        else
        {
            coinCount.text = GameDataVariable.dataVariables[1].ToString();

        }
    }

    public void StartButton(string sceneToLoad)
    {
        startButtonSound.Play();
        FindObjectOfType<SceneFader>().FadeTo(sceneToLoad);
    }

    public void QuitButton()
    {
        buttonSound.Play();
        Application.Quit();
    }

    private void SetQuitPanel()
    {
        buttonSound.Play();
        quitPanel.SetActive(true);
        isQuitPanelActive = true;
    }

    public void DesableQuitPanel()
    {
        buttonSound.Play();
        quitPanel.SetActive(false);
        isQuitPanelActive = false;
    }

    public void ProfileButton()
    {
        buttonSound.Play();
        isProfilePanelActive = true;
        profilePanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void CloseProfileButton()
    {
        buttonSound.Play();
        isProfilePanelActive = false;
        profilePanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void ShopButton()
    {
        buttonSound.Play();
        isShopActive = true;
        mainPanel.SetActive(false);
        playerObject.SetActive(false);
        groundObject.SetActive(false);
        quadBGObject.SetActive(false);
        shopCanvas.SetActive(true);
    }
      
    public void CloseShopButton()
    {
        buttonSound.Play();
        isShopActive = false;
        mainPanel.SetActive(true);
        playerObject.SetActive(true);
        groundObject.SetActive(true);
        quadBGObject.SetActive(true);
        shopCanvas.SetActive(false);
    }

}
