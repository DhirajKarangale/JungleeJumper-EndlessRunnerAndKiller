using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.Purchasing;

public class Shop : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] MainMenu mainMenu;
    [SerializeField] GameObject signInPanel;
    [SerializeField] AudioSource buttonSound;
    [SerializeField] Sprite buttonDiseableSprite;
    public Text msgText;
    public GameObject msgTextObject;
    [SerializeField] Text coinCountText;
    [SerializeField] Button playerFireball1SelectButton;
    [SerializeField] Button playerFireball2SelectButton;
    [SerializeField] Button game1SelectButton;
    [SerializeField] Button game2SelectButton;
    [SerializeField] Button dashEffect1SelectButton;
    [SerializeField] Button dashEffect2SelectButton;
    public float xGoldTimer;
    public float xScoreTimer;
    public float xCoinMagnetTimer;
    private bool isSigninPanelActivate;

    private const string coin3000 = "com.dksoftware.jungleejumperendlessrunnerandkiller.coin3000";
    private const string removeAds = "com.dksoftware.jungleejumperendlessrunnerandkiller.removeads";


    private void Start()
    {
        Advertisement.Initialize("4086101", false);
        Advertisement.AddListener(this);

        if (GameDataVariable.dataVariables[8] == 1)
       {
            xGoldTimer = 1800f;
            xGoldTimer -= TimeCalculator.instance.CheckDate();
       }

        if (GameDataVariable.dataVariables[9] == 1)
        {
            xScoreTimer = 2700f;
            xScoreTimer -= TimeCalculator.instance.CheckDate();
        }

        if (GameDataVariable.dataVariables[10] == 1)
        {
            xCoinMagnetTimer = 3600f;
            xCoinMagnetTimer -= TimeCalculator.instance.CheckDate();
        }

        msgTextObject.SetActive(false);
        ShowScore();
        ShopSelectButtonManager();
    }

    private void Update()
    {
        ShowScore();

        if (GameDataVariable.dataVariables[8] == 1)
        {
            xGoldTimer -= Time.deltaTime;
        }

        if (xGoldTimer <= 0)
        {
            GameDataVariable.dataVariables[8] = 0;
            PlayGamesController.Instance.SaveData();
        }

        if (GameDataVariable.dataVariables[10] == 1)
        {
            xCoinMagnetTimer -= Time.deltaTime;
        }

        if (xCoinMagnetTimer <= 0)
        {
            GameDataVariable.dataVariables[10] = 0;
            PlayGamesController.Instance.SaveData();
        }

        if (GameDataVariable.dataVariables[9] == 1)
        {
            xScoreTimer -= Time.deltaTime;
        }

        if (xScoreTimer <= 0)
        {
            GameDataVariable.dataVariables[9] = 0;
            PlayGamesController.Instance.SaveData();
        }

        if (Input.GetKey(KeyCode.Escape) && !isSigninPanelActivate)
        {
            mainMenu.CloseShopButton();
        }
        if(Input.GetKey(KeyCode.Escape) && isSigninPanelActivate)
        {
            CloseSignInPanel();
        }

        ShopSelectButtonManager();
    }

    public void ShopSelectButtonManager()
    {
        // Fireballs
        if ((GameDataVariable.dataVariables[2] == 1) && (GameDataVariable.dataVariables[3] == 2))
        {
            playerFireball1SelectButton.interactable = true;
            playerFireball2SelectButton.interactable = false;
            playerFireball1SelectButton.GetComponentInChildren<Text>().text = "Select";
            playerFireball2SelectButton.GetComponentInChildren<Text>().text = "Selected";
            playerFireball1SelectButton.image.overrideSprite = null;
            playerFireball2SelectButton.image.overrideSprite = buttonDiseableSprite;
        }
        else
        {
            playerFireball1SelectButton.interactable = false;
            playerFireball2SelectButton.interactable = true;
            playerFireball1SelectButton.GetComponentInChildren<Text>().text = "Selected";
            playerFireball2SelectButton.GetComponentInChildren<Text>().text = "Select";
            playerFireball1SelectButton.image.overrideSprite = buttonDiseableSprite;
            playerFireball2SelectButton.image.overrideSprite = null;
        }

        // Areana
        if ((GameDataVariable.dataVariables[4] == 1) && (GameDataVariable.dataVariables[5] == 2))
        {
            game1SelectButton.interactable = true;
            game2SelectButton.interactable = false;
            game1SelectButton.GetComponentInChildren<Text>().text = "Select";
            game2SelectButton.GetComponentInChildren<Text>().text = "Selected";
            game1SelectButton.image.overrideSprite = null;
            game2SelectButton.image.overrideSprite = buttonDiseableSprite;
        }
        else
        {
            game1SelectButton.interactable = false;
            game2SelectButton.interactable = true;
            game1SelectButton.GetComponentInChildren<Text>().text = "Selected";
            game2SelectButton.GetComponentInChildren<Text>().text = "Select";
            game1SelectButton.image.overrideSprite = buttonDiseableSprite;
            game2SelectButton.image.overrideSprite = null;
        }

        //DashEffect
        if ((GameDataVariable.dataVariables[6] == 1) && (GameDataVariable.dataVariables[7] == 2))
        {
            dashEffect1SelectButton.interactable = true;
            dashEffect2SelectButton.interactable = false;
            dashEffect1SelectButton.GetComponentInChildren<Text>().text = "Select";
            dashEffect2SelectButton.GetComponentInChildren<Text>().text = "Selected";
            dashEffect1SelectButton.image.overrideSprite = null;
            dashEffect2SelectButton.image.overrideSprite = buttonDiseableSprite;
        }
        else
        {
            dashEffect1SelectButton.interactable = false;
            dashEffect2SelectButton.interactable = true;
            dashEffect1SelectButton.GetComponentInChildren<Text>().text = "Selected";
            dashEffect2SelectButton.GetComponentInChildren<Text>().text = "Select";
            dashEffect1SelectButton.image.overrideSprite = buttonDiseableSprite;
            dashEffect2SelectButton.image.overrideSprite = null;
        }
    }

    public void ShowScore()
    {
        if (GameDataVariable.dataVariables[1] >= 1000)
        {
            coinCountText.text = string.Format("{0}.{1}K", Convert.ToInt32((GameDataVariable.dataVariables[1] / 1000)), int.Parse(((GameDataVariable.dataVariables[1] % 1000) / 100).ToString()[0].ToString()));
        }
        else
        {
            coinCountText.text = GameDataVariable.dataVariables[1].ToString();

        }
    }

    public void DesaibleMsgText()
    {
        msgTextObject.SetActive(false);
    }
    public void CloseSignInPanel()
    {
        buttonSound.Play();
        isSigninPanelActivate = false;
        signInPanel.SetActive(false);
    }


    public void Fireball2BuyButton()
    {
        buttonSound.Play();
        if(GameDataVariable.dataVariables[2] == 1)
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.green;
            msgText.text = "Already Purchased !";
            Invoke("DesaibleMsgText", 1f);
        }
        else if(GameDataVariable.dataVariables[1] < 5000)
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.white;
            msgText.text = "Not Enough Coin";
            Invoke("DesaibleMsgText", 1.7f);
        }
        else if (!Social.localUser.authenticated)
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.white;
            msgText.text = "Your are not login to Google play";
            Invoke("DesaibleMsgText", 2f);
            signInPanel.SetActive(true);
            isSigninPanelActivate = true;
        }
        else
        {
            GameDataVariable.dataVariables[1] -= 5000;
            GameDataVariable.dataVariables[2] = 1;
            PlayGamesController.Instance.SaveData();
            Fireball2SelectButton();
            ShowScore();
            msgTextObject.SetActive(true);
            msgText.color = Color.green;
            msgText.text = "Purchased Sucessfully !";
            Invoke("DesaibleMsgText", 1.19f);
        }
    }

    public void Fireball1SelectButton()
    {
        buttonSound.Play();
        GameDataVariable.dataVariables[3] = 1;
        playerFireball2SelectButton.GetComponentInChildren<Text>().text = "Select";
        playerFireball1SelectButton.GetComponentInChildren<Text>().text = "Selected";
        playerFireball1SelectButton.interactable = false;
        playerFireball2SelectButton.interactable = true;
        playerFireball1SelectButton.image.overrideSprite = buttonDiseableSprite;
        playerFireball2SelectButton.image.overrideSprite = null;
        PlayGamesController.Instance.SaveData();
    }

    public void Fireball2SelectButton()
    {
        buttonSound.Play();
        if (GameDataVariable.dataVariables[2] == 1)
        {
            GameDataVariable.dataVariables[3] = 2;
            playerFireball2SelectButton.GetComponentInChildren<Text>().text = "Selected";
            playerFireball1SelectButton.GetComponentInChildren<Text>().text = "Select";
            playerFireball2SelectButton.interactable = false;
            playerFireball1SelectButton.interactable = true;
            playerFireball1SelectButton.image.overrideSprite = null;
            playerFireball2SelectButton.image.overrideSprite = buttonDiseableSprite;
            PlayGamesController.Instance.SaveData();
        }
        else
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.white;
            msgText.text = "Item is not Purchased";
            Invoke("DesaibleMsgText", 2.5f);
        }
    }



    public void Game2BuyButton()
    {
        buttonSound.Play();
        if (GameDataVariable.dataVariables[4] == 1)
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.green;
            msgText.text = "Already Purchased !";
            Invoke("DesaibleMsgText", 1f);
        }
        else if (GameDataVariable.dataVariables[1] < 10000)
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.white;
            msgText.text = "Not Enough Coin";
            Invoke("DesaibleMsgText", 1.7f);
        }
        else if (!Social.localUser.authenticated)
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.white;
            msgText.text = "Your are not login to Google play";
            Invoke("DesaibleMsgText", 2f);
            signInPanel.SetActive(true);
            isSigninPanelActivate = true;
        }
        else
        {
            GameDataVariable.dataVariables[1] -= 10000;
            GameDataVariable.dataVariables[4] = 1;
            PlayGamesController.Instance.SaveData();
            Game2SelectButton();
            ShowScore();
            msgTextObject.SetActive(true);
            msgText.color = Color.green;
            msgText.text = "Purchased Sucessfully !";
            Invoke("DesaibleMsgText", 1.19f);
        }
    }

    public void Game1SelectButton()
    {
        buttonSound.Play();
        GameDataVariable.dataVariables[5] = 1;
        game2SelectButton.GetComponentInChildren<Text>().text = "Select";
        game1SelectButton.GetComponentInChildren<Text>().text = "Selected";
        game1SelectButton.interactable = false;
        game2SelectButton.interactable = true;
        game1SelectButton.image.overrideSprite = buttonDiseableSprite;
        game2SelectButton.image.overrideSprite = null;
        PlayGamesController.Instance.SaveData();
    }

    public void Game2SelectButton()
    {
        buttonSound.Play();
        if (GameDataVariable.dataVariables[4] == 1)
        {
            GameDataVariable.dataVariables[5] = 2;
            game2SelectButton.GetComponentInChildren<Text>().text = "Selected";
            game1SelectButton.GetComponentInChildren<Text>().text = "Select";
            playerFireball2SelectButton.interactable = false;
            game1SelectButton.interactable = true;
            game1SelectButton.image.overrideSprite = null;
            game2SelectButton.image.overrideSprite = buttonDiseableSprite;
            PlayGamesController.Instance.SaveData();
        }
        else
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.white;
            msgText.text = "Item is not Purchased";
            Invoke("DesaibleMsgText", 1.5f);
        }
    }



    public void DashEffect2BuyButton()
    {
        buttonSound.Play();
        if (GameDataVariable.dataVariables[6] == 1)
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.green;
            msgText.text = "Already Purchased !";
            Invoke("DesaibleMsgText", 1f);
        }
        else if (GameDataVariable.dataVariables[1] < 1500)
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.white;
            msgText.text = "Not Enough Coin";
            Invoke("DesaibleMsgText", 1.7f);
        }
        else if (!Social.localUser.authenticated)
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.white;
            msgText.text = "Your are not login to Google play";
            Invoke("DesaibleMsgText", 2f);
            signInPanel.SetActive(true);
            isSigninPanelActivate = true;
        }
        else
        {
            GameDataVariable.dataVariables[1] -= 1500;
            GameDataVariable.dataVariables[6] = 1;
            PlayGamesController.Instance.SaveData();
            DashEffect2SelectButton();
            ShowScore();
            msgTextObject.SetActive(true);
            msgText.color = Color.green;
            msgText.text = "Purchased Sucessfully !";
            Invoke("DesaibleMsgText", 1.19f);
        }
    }

    public void DashEffect1SelectButton()
    {
        buttonSound.Play();
        GameDataVariable.dataVariables[7] = 1;
        dashEffect2SelectButton.GetComponentInChildren<Text>().text = "Select";
        dashEffect1SelectButton.GetComponentInChildren<Text>().text = "Selected";
        dashEffect1SelectButton.interactable = false;
        dashEffect2SelectButton.interactable = true;
        dashEffect1SelectButton.image.overrideSprite = buttonDiseableSprite;
        dashEffect2SelectButton.image.overrideSprite = null;
        PlayGamesController.Instance.SaveData();
    }

    public void DashEffect2SelectButton()
    {
        buttonSound.Play();
        if (GameDataVariable.dataVariables[6] == 1)
        {
            GameDataVariable.dataVariables[7] = 2;
            dashEffect2SelectButton.GetComponentInChildren<Text>().text = "Selected";
            dashEffect1SelectButton.GetComponentInChildren<Text>().text = "Select";
            playerFireball2SelectButton.interactable = false;
            dashEffect1SelectButton.interactable = true;
            dashEffect1SelectButton.image.overrideSprite = null;
            dashEffect2SelectButton.image.overrideSprite = buttonDiseableSprite;
            PlayGamesController.Instance.SaveData();
        }
        else
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.white;
            msgText.text = "Item is not Purchased";
            Invoke("DesaibleMsgText", 1.5f);
        }
    }



    public void XGoldBuyButton()
    {
        buttonSound.Play();
        if (GameDataVariable.dataVariables[8] == 1)
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.white;
            msgText.text = "Last ability is not over yet";
            Invoke("DesaibleMsgText", 1.7f);
        }
        else if (GameDataVariable.dataVariables[1] < 2700)
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.white;
            msgText.text = "Not Enough Coin";
            Invoke("DesaibleMsgText", 1.7f);
        }
        else if (!Social.localUser.authenticated)
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.white;
            msgText.text = "Your are not login to Google play";
            Invoke("DesaibleMsgText", 2f);
            signInPanel.SetActive(true);
            isSigninPanelActivate = true;
        }
        else
        {
            GameDataVariable.dataVariables[1] -= 2700;
            GameDataVariable.dataVariables[8] = 1;
            TimeCalculator.instance.SaveTime();
            msgTextObject.SetActive(true);
            msgText.color = Color.green;
            msgText.text = "3X Gold Added for 30Min";
            Invoke("DesaibleMsgText", 2f);
            xGoldTimer = 1800f;
            xGoldTimer -= TimeCalculator.instance.CheckDate();
            PlayGamesController.Instance.SaveData();
        }
    }



    public void XScoreBuyButton()
    {
        buttonSound.Play();
        if (GameDataVariable.dataVariables[9] == 1)
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.white;
            msgText.text = "Last ability is not over yet";
            Invoke("DesaibleMsgText", 1.7f);
        }
        else if (GameDataVariable.dataVariables[1] < 2500)
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.white;
            msgText.text = "Not Enough Coin";
            Invoke("DesaibleMsgText", 1.7f);
        }
        else if (!Social.localUser.authenticated)
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.white;
            msgText.text = "Your are not login to Google play";
            Invoke("DesaibleMsgText", 2f);
            signInPanel.SetActive(true);
            isSigninPanelActivate = true;
        }
        else
        {
            GameDataVariable.dataVariables[1] -= 2500;
            GameDataVariable.dataVariables[9] = 1;
            msgTextObject.SetActive(true);
            msgText.color = Color.green;
            msgText.text = "3X Score Added for 45Min";
            Invoke("DesaibleMsgText", 2f);
            TimeCalculator.instance.SaveTime();
            xScoreTimer = 2700f;
            xScoreTimer -= TimeCalculator.instance.CheckDate();
            PlayGamesController.Instance.SaveData();
        }
    }



    public void CoinMagnetBuyButton()
    {
        buttonSound.Play();
        if (GameDataVariable.dataVariables[10] == 1)
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.white;
            msgText.text = "Last ability is not over yet";
            Invoke("DesaibleMsgText", 1.7f);
        }
        else if (GameDataVariable.dataVariables[1] < 2000)
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.white;
            msgText.text = "Not Enough Coin";
            Invoke("DesaibleMsgText", 1.7f);
        }
        else if (!Social.localUser.authenticated)
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.white;
            msgText.text = "Your are not login to Google play";
            Invoke("DesaibleMsgText", 2f);
            signInPanel.SetActive(true);
            isSigninPanelActivate = true;
        }
        else
        {
            GameDataVariable.dataVariables[1] -= 2000;
            GameDataVariable.dataVariables[10] = 1;
            TimeCalculator.instance.SaveTime();
            msgTextObject.SetActive(true);
            msgText.color = Color.green;
            msgText.text = "Coin Magnet Added for 1Hr";
            Invoke("DesaibleMsgText", 2f);
            ShowScore();
            xCoinMagnetTimer = 3600;
            xCoinMagnetTimer -= TimeCalculator.instance.CheckDate();
            PlayGamesController.Instance.SaveData();
        }
    }


    public void RewardedAdButton()
    {
        buttonSound.Play();
        if (!Social.localUser.authenticated)
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.white;
            msgText.text = "Your are not login to Google play";
            Invoke("DesaibleMsgText", 2f);
            signInPanel.SetActive(true);
            isSigninPanelActivate = true;
        }
        else
        {
            ShowRewardedVideoAd();
        }
    }



    public void ShowRewardedVideoAd()
    {
        if (Advertisement.IsReady("Rewarded_Android"))
            Advertisement.Show("Rewarded_Android");
        else
        {
            Debug.Log("Reward Ad is not loaded");
            msgTextObject.SetActive(true);
            msgText.color = Color.white;
            msgText.text = "Ad not Loaded Check Network";
            Invoke("DesaibleMsgText", 1.5f);
        }
    }


    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Ads ready");
    }

    public void OnUnityAdsDidError(string message)
    {
        if (msgTextObject != null) msgTextObject.SetActive(true);
        if (msgText != null)
        {
            msgText.color = Color.white;
            msgText.text = "Error" + message;
        }
        Invoke("DesableTxt", 1.8f);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Ads Started");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if ((placementId == "Rewarded_Android") && (showResult == ShowResult.Finished))
        {
            GameDataVariable.dataVariables[1] += 100;
            Advertisement.RemoveListener(this);
            PlayGamesController.Instance.SaveData();
            if (msgTextObject != null) msgTextObject.SetActive(true);
            if (msgText != null)
            {
                msgText.color = Color.green;
                msgText.text = "You Received Reward";
                Invoke("DesaibleMsgText", 1.8f);
            }
        }
    }




    public void OnPurChaseComplete(Product product)
    {
        if (product.definition.id == coin3000)
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.green;
            msgText.text = "Purchased Sucessfully ! You Got 3000 Coin";
            Invoke("DesaibleMsgText", 1.9f);
            GameDataVariable.dataVariables[1] += 3000;
            PlayGamesController.Instance.SaveData();
            ShowScore();
        }

        if(product.definition.id == removeAds)
        {
           if(GameDataVariable.dataVariables[11] != 1)
           {
                msgTextObject.SetActive(true);
                msgText.color = Color.green;
                msgText.text = "Purchased Sucessfully ! Ads Removed";
                Invoke("DesaibleMsgText", 1.9f);
                AdManager.instance.HideBanner();
                GameDataVariable.dataVariables[11] = 1;
                PlayGamesController.Instance.SaveData();
           }
            else
            {
                msgTextObject.SetActive(true);
                msgText.color = Color.green;
                msgText.text = "Already Purchased ! Ads already Removed";
                Invoke("DesaibleMsgText", 1.9f);
            }
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason purchaseFailureReason)
    {
        msgTextObject.SetActive(true);
        msgText.color = Color.white;
        msgText.text = "Purchased Failed " + purchaseFailureReason;
        Invoke("DesaibleMsgText", 1.4f);
    }
}
