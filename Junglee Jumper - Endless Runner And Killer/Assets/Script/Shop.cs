using UnityEngine;
using System;
using UnityEngine.UI;

public class Shop : MonoBehaviour
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
    private bool isSigninPanelActivate;

    private void Start()
    {
        msgTextObject.SetActive(false);
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
        ShowScore();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape) && !isSigninPanelActivate)
        {
            mainMenu.CloseShopButton();
        }
        if(Input.GetKey(KeyCode.Escape) && isSigninPanelActivate)
        {
            CloseSignInPanel();
        }
    }

    private void ShowScore()
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
            Invoke("DesaibleMsgText", 1.5f);
        }
        else if(GameDataVariable.dataVariables[1] < 1000)
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.red;
            msgText.text = "Not Enough Coin";
            Invoke("DesaibleMsgText", 2.5f);
        }
        else if (!Social.localUser.authenticated)
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.red;
            msgText.text = "Your are not login to Google play";
            Invoke("DesaibleMsgText", 2.5f);
            signInPanel.SetActive(true);
            isSigninPanelActivate = true;
        }
        else
        {
            GameDataVariable.dataVariables[1] -= 1000;
            GameDataVariable.dataVariables[2] = 1;
            Fireball2SelectButton();
            PlayGamesController.Instance.SaveData();
            ShowScore();
            msgTextObject.SetActive(true);
            msgText.color = Color.green;
            msgText.text = "Purchased Sucessfully !";
            Invoke("DesaibleMsgText", 2.5f);
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
            msgText.color = Color.red;
            msgText.text = "Purchase First then Select";
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
            Invoke("DesaibleMsgText", 1.5f);
        }
        else if (GameDataVariable.dataVariables[1] < 5000)
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.red;
            msgText.text = "Not Enough Coin";
            Invoke("DesaibleMsgText", 2.5f);
        }
        else if (!Social.localUser.authenticated)
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.red;
            msgText.text = "Your are not login to Google play";
            Invoke("DesaibleMsgText", 2.5f);
            signInPanel.SetActive(true);
            isSigninPanelActivate = true;
        }
        else
        {
            GameDataVariable.dataVariables[1] -= 5000;
            GameDataVariable.dataVariables[4] = 1;
            Game2SelectButton();
            PlayGamesController.Instance.SaveData();
            ShowScore();
            msgTextObject.SetActive(true);
            msgText.color = Color.green;
            msgText.text = "Purchased Sucessfully !";
            Invoke("DesaibleMsgText", 2.5f);
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
            msgText.color = Color.red;
            msgText.text = "Purchase First then Select";
            Invoke("DesaibleMsgText", 2.5f);
        }
    }
}
