using System;
using UnityEngine;
using UnityEngine.UI;
using Unity.RemoteConfig;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Shop shop;
    [SerializeField] GameObject xGold;
    [SerializeField] GameObject xScore;
    [SerializeField] GameObject coinMagnet;
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject profilePanel;
    [SerializeField] GameObject quitPanel;
    [SerializeField] GameObject shopPanel;
    [SerializeField] GameObject aboutPanel;
    [SerializeField] GameObject forceUpdatePanel;
    [SerializeField] AudioSource startButtonSound;
    [SerializeField] AudioSource buttonSound;
    [SerializeField] Text highScoreCount;
    [SerializeField] Text coinCount;
    private bool isQuitPanelActive;

    private int remoteVersionCode;

    public struct userAttributes { }
    public struct appAttributes { }


    private void Awake()
    {
        isQuitPanelActive = false;

        Unity.Collections.NativeLeakDetection.Mode = Unity.Collections.NativeLeakDetectionMode.Disabled;
        ConfigManager.FetchCompleted += CheckVersionCode;
        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }

    private void CheckVersionCode(ConfigResponse response)
    {
        remoteVersionCode = ConfigManager.appConfig.GetInt("VersionCode",13);
        if (remoteVersionCode != Convert.ToInt32(Application.version))
        {
            ActivePanel(forceUpdatePanel);
        }
        else
        {
            CloseButton();
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isQuitPanelActive) ActivePanel(quitPanel);
            else CloseButton();
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

        AbilityActiveStatusImage();
    }


    private void OnDestroy()
    {
        ConfigManager.FetchCompleted -= CheckVersionCode;
    }

    private void AbilityActiveStatusImage()
    {
        if (GameDataVariable.dataVariables[10] == 1)
        {
            coinMagnet.SetActive(true);
        }
        else
        {
            coinMagnet.SetActive(false);
        }
        if (GameDataVariable.dataVariables[8] == 1)
        {
            xGold.SetActive(true);
        }
        else
        {
            xGold.SetActive(false);
        }
        if (GameDataVariable.dataVariables[9] == 1)
        {
            xScore.SetActive(true);
        }
        else
        {
            xScore.SetActive(false);
        }
    }

    public void StartButton()
    {
        startButtonSound.Play();

        if(GameDataVariable.dataVariables[5] == 2)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void QuitButton()
    {
        buttonSound.Play();
        Application.Quit();
    }

    public void OpenLinkButton(string link)
    {
        buttonSound.Play();
        Application.OpenURL(link);
    }

    public void ActivePanel(GameObject panel)
    {
        buttonSound.Play();
        isQuitPanelActive = true;

        mainPanel.SetActive(false);
        profilePanel.SetActive(false);
        quitPanel.SetActive(false);
        shopPanel.SetActive(false);
        aboutPanel.SetActive(false);
        forceUpdatePanel.SetActive(false);

        panel.SetActive(true);
    }

    public void CloseButton()
    {
        buttonSound.Play();
        isQuitPanelActive = false;

        mainPanel.SetActive(true);
        profilePanel.SetActive(false);
        quitPanel.SetActive(false);
        shopPanel.SetActive(false);
        aboutPanel.SetActive(false);
        forceUpdatePanel.SetActive(false);

        shop.CloseSignInPanel();
    }
}
