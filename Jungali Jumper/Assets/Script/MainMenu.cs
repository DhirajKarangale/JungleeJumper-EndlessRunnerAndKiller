using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.RemoteConfig;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Shop shop;
    [SerializeField] GameObject xGold;
    [SerializeField] GameObject xScore;
    [SerializeField] GameObject coinMagnet;
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject profilePanel;
    [SerializeField] GameObject quitPanel;
    [SerializeField] GameObject shopCanvas;
    [SerializeField] GameObject creditsPanel;
    [SerializeField] GameObject forceUpdatePanel;
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject groundObject;
    [SerializeField] GameObject quadBGObject;
    [SerializeField] AudioSource startButtonSound;
    [SerializeField] AudioSource buttonSound;
    [SerializeField] Text highScoreCount;
    [SerializeField] Text coinCount;
    private bool isQuitPanelActive,isShopActive,isCreditsActive,isForceUpdateActive;
    public static bool isProfilePanelActive;

    private int remoteVersionCode;

    public struct userAttributes { }
    public struct appAttributes { }


    void Awake()
    {
        ConfigManager.FetchCompleted += CheckVersionCode;
        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }

    void CheckVersionCode(ConfigResponse response)
    {
        remoteVersionCode = ConfigManager.appConfig.GetInt("VersionCode");
        Debug.Log("Remote Version Code is : " + remoteVersionCode);
        if (remoteVersionCode != Convert.ToInt32(Application.version))
        {
            ShowForceUpdate();
        }
        else
        {
            CloseForceUpdate();
        }
    }


    private void Update()
    {
      
        if (Input.GetKey(KeyCode.Escape) && !isProfilePanelActive && !isShopActive && !isCreditsActive)
        {
            if (isQuitPanelActive) DesableQuitPanel();
            else SetQuitPanel();
        }

        if (Input.GetKey(KeyCode.Escape) && isProfilePanelActive)
        {
            CloseProfileButton();
        }

        if (Input.GetKey(KeyCode.Escape) && isShopActive)
        {
            CloseShopButton();
        }

        if ((Input.GetKey(KeyCode.Escape) && isCreditsActive))
        {
            CloseCreditsButton();
        }

        if(Input.GetKey(KeyCode.Escape) && isForceUpdateActive)
        {
            UpdateNowButton();
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

    public void CreditsButton()
    {
        AdManager.instance.HideBanner();
        isCreditsActive = true;
        creditsPanel.SetActive(true);
        buttonSound.Play();
        mainPanel.SetActive(false);
        playerObject.SetActive(false);
        groundObject.SetActive(false);
        quadBGObject.SetActive(false);
    }

    public void StartButton()
    {
        startButtonSound.Play();
        if(GameDataVariable.dataVariables[5] == 2)
        {
            FindObjectOfType<SceneFader>().FadeTo("Game 2");
        }
        else
        {
            FindObjectOfType<SceneFader>().FadeTo("Game");
        }
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
        shopCanvas.SetActive(true);
        buttonSound.Play();
        isShopActive = true;
        mainPanel.SetActive(false);
        playerObject.SetActive(false);
        groundObject.SetActive(false);
        quadBGObject.SetActive(false);
    }
      
    public void CloseShopButton()
    {
        shop.CloseSignInPanel();
        buttonSound.Play();
        isShopActive = false;
        mainPanel.SetActive(true);
        playerObject.SetActive(true);
        groundObject.SetActive(true);
        quadBGObject.SetActive(true);
        shopCanvas.SetActive(false);
    }


    public void CloseCreditsButton()
    {
        AdManager.instance.ShowBannerAd();
        isCreditsActive = false;
        creditsPanel.SetActive(false);
        buttonSound.Play();
        mainPanel.SetActive(true);
        playerObject.SetActive(true);
        groundObject.SetActive(true);
        quadBGObject.SetActive(true);
    }

    public void ShowForceUpdate()
    {
        isForceUpdateActive = true; 
        forceUpdatePanel.SetActive(true);
        mainPanel.SetActive(false);
        playerObject.SetActive(false);
        groundObject.SetActive(false);
        quadBGObject.SetActive(false);
    }

    public void CloseForceUpdate()
    {
        isForceUpdateActive = false;
        forceUpdatePanel.SetActive(false);
        mainPanel.SetActive(true);
        playerObject.SetActive(true);
        groundObject.SetActive(true);
        quadBGObject.SetActive(true);
    }

    public void UpdateNowButton()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.DKSoftware.JungleeJumperEndlessRunnerAndKiller");
    }

    void OnDestroy()
    {
        ConfigManager.FetchCompleted -= CheckVersionCode;
    }
}
