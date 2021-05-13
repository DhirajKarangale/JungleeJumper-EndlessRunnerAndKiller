using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class PlayGamesController : MonoBehaviour {

    public Text msgText;
    [SerializeField] GameObject msgTextObject;
    private static bool isSignInAllowed = true;
    

    private void Start()
    {
        if(isSignInAllowed)
        {
            msgTextObject.SetActive(true);
            AuthenticateUser();
            isSignInAllowed = false;
        }
        else
        {
            msgTextObject.SetActive(false);
        }
    }


    private void OnApplicationQuit()
    {
        isSignInAllowed = true;
    }

    private void AuthenticateUser()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) =>
        {
            if (success == true)
            {
                Debug.Log("Logged in to Google Play Games Services");
                msgTextObject.SetActive(true);
                msgText.text = "Successfully Logged in";
                Invoke("DesablemsgText", 3);
            }
            else
            {
                Debug.LogError("Unable to sign in to Google Play Games Services");
                msgText.text = "Could not login to Google Play Games Services. \n Try Again.";
                msgText.color = Color.red;
                Invoke("DesablemsgText", 3);
            }
        });
    }

    public void SignInButton()
    {
        if(Social.localUser.authenticated)
        {
            msgTextObject.SetActive(true);
            msgText.text = "Already Logged In";
            Invoke("DesablemsgText", 3);
        }
        else
        {
            AuthenticateUser();
        }
    }

    public void SignOutButton()
    {
        if(!Social.localUser.authenticated)
        {
            msgTextObject.SetActive(true);
            msgText.text = "Already Logged Out";
            Invoke("DesablemsgText", 3);
        }
        else
        {
            PlayGamesPlatform.Instance.SignOut();
            msgTextObject.SetActive(true);
            msgText.text = "LogOut Sucessfully";
            Invoke("DesablemsgText", 3);
        }
    }

    private void DesablemsgText()
    {
        msgTextObject.SetActive(false);
    }

    public static void PostToLeaderboard(long newScore)
    {
        Social.ReportScore(newScore, "CgkI59ausbsKEAIQAg", (bool success) => {
            if (success)
            {
                Debug.Log("Posted new score to leaderboard");
            }
            else
            {
                Debug.LogError("Unable to post new score to leaderboard");
            }
        });
    }

    public void ShowLeaderboardUI()
    {
         if(Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkI59ausbsKEAIQAg");
        }
         else
        {
            msgTextObject.SetActive(true);
            msgText.text = "You are Logged Out.\nLogin First.";
            msgText.color = Color.red;
            Invoke("DesablemsgText", 3);
        }
    }
}
