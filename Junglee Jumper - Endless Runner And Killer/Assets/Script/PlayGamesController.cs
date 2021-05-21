using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayGamesController : MonoBehaviour
{

    public static PlayGamesController Instance { get; private set; }

    const string SAVE_NAME = "JungleeJumperDKSoftwareSaveData9";
    bool isSaving;
    bool isCloudDataLoaded = false;
    [SerializeField] AudioSource buttonSound;
    [SerializeField] Image playerProfile;
    [SerializeField] Texture2D deafultPlayerImage;
    [SerializeField] Text playerName;
    [SerializeField] Image playerProfile2;
    [SerializeField] Text playerName2;
    [SerializeField] GameObject msgTextObject;
    [SerializeField] Text msgText;
    [SerializeField] Text profileMsgTxt;
    [SerializeField] GameObject profileMsgTxtObject;
    [SerializeField] Shop shop;

    public static bool isSignInCheck = true;

    // Use this for initialization
    void Start()
    {
        Instance = this;
        msgTextObject.SetActive(false);
        profileMsgTxtObject.SetActive(false);
        //setting default value, if the game is played for the first time
        if (!PlayerPrefs.HasKey(SAVE_NAME))
            PlayerPrefs.SetString(SAVE_NAME, string.Empty);
        //tells us if it's the first time that this game has been launched after install - 0 = no, 1 = yes 
        if (!PlayerPrefs.HasKey("IsFirstTime"))
            PlayerPrefs.SetInt("IsFirstTime", 1);

        LoadLocal(); //we want to load local data first because loading from cloud can take quite a while, if user progresses while using local data, it will all
                     //sync in our comparating loop in StringToGameData(string, string)

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        if(isSignInCheck)
        {
            msgTextObject.SetActive(true);
            msgText.color = Color.white;
            msgText.text = "Signing in to Google Play .....";
            profileMsgTxtObject.SetActive(true);
            profileMsgTxt.color = Color.white;
            profileMsgTxt.text = "Signing in to Google Play .....";
            SignIn();
        }
      
    }

    private void OnApplicationQuit()
    {
        isSignInCheck =true;
    }

    private void Update()
    {
       if(!Social.localUser.authenticated)
        {
            Texture2D tex;
            tex = deafultPlayerImage;
            if (playerProfile != null)
                playerProfile.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0f, 0f));
            if (playerProfile2 != null)
                playerProfile2.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0f, 0f));
            if (playerName != null)
                playerName.text = "Junglee Jumper";
            if (playerName2 != null)
                playerName2.text = "Junglee Jumper";
        }
       else
        {
            StartCoroutine(PlayerImage());
            if (playerName != null)
                playerName.text = Social.localUser.userName;
            if (playerName2 != null)
                playerName2.text = Social.localUser.userName;
        }
    }

    #region Sign In/Out Button
    void SignIn()
    {
        //when authentication process is done (successfuly or not), we load cloud data

        Social.localUser.Authenticate((bool success) =>
        {
            if (success == true)
            {
                isSignInCheck = false;
                LoadData();
                StartCoroutine(PlayerImage());
                if (playerName != null)
                    playerName.text = Social.localUser.userName;
                if (playerName2 != null)
                    playerName2.text = Social.localUser.userName;
                if (profileMsgTxtObject != null) profileMsgTxtObject.SetActive(true);
                profileMsgTxt.color = Color.green;
                if (profileMsgTxt != null) profileMsgTxt.text = "Successfully Logged in";
                Invoke("DesableProfilemsgText", 3);
                msgText.color = Color.green;
                if (msgTextObject != null) msgTextObject.SetActive(true);
                if (msgText != null) msgText.text = "Successfully Logged in";
                Invoke("DesablemsgText", 3);
                shop.CloseSignInPanel();
                if(shop.msgTextObject != null) shop.msgTextObject.SetActive(true);
                if(shop.msgText != null)
                {
                    shop.msgText.color = Color.green;
                    shop.msgText.text = "Successfully Logged in";
                }
                Invoke("DesableShopText", 3);
                Debug.Log("Logged in to Google Play Games Services");
            }
            else
            {
                Texture2D tex;
                tex = deafultPlayerImage;
                if (playerProfile != null)
                    playerProfile.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0f, 0f));
                if (playerProfile2 != null)
                    playerProfile2.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0f, 0f));
                if (playerName != null)
                    playerName.text = "Junglee Jumper";
                if (playerName2 != null)
                    playerName2.text = "Junglee Jumper";
                Debug.Log("Unable to sign in to Google Play Games Services");
                if (profileMsgTxtObject != null) profileMsgTxtObject.SetActive(true);
                profileMsgTxt.color = Color.red;
                if (profileMsgTxt != null) profileMsgTxt.text = "Could not login to Google Play Games Services. \n Check Network.";
                Invoke("DesableProfilemsgText", 3);
                if (msgTextObject != null) msgTextObject.SetActive(true);
                msgText.color = Color.red;
                if (msgText != null) msgText.text = "Could not login to Google Play Games Services. \n  Check Network.";
                Invoke("DesablemsgText", 3);
                shop.ShopSelectButtonManager();
                if (shop.msgTextObject != null) shop.msgTextObject.SetActive(true);
                if (shop.msgText != null)
                {
                    shop.msgText.color = Color.red;
                    shop.msgText.text = "Could not login to Google Play Games Services. \n  Check Network.";
                }
                Invoke("DesableShopText", 3);
            }
        });

    }

    private void DesableShopText()
    {
        shop.DesaibleMsgText();
    }

    public void SignInButton()
    {
        buttonSound.Play();
        if (Social.localUser.authenticated)
        {
            if (profileMsgTxtObject != null) profileMsgTxtObject.SetActive(true);
            profileMsgTxt.color = Color.green;
            if (profileMsgTxt != null) profileMsgTxt.text = "Already Logged In";
            Invoke("DesableProfilemsgText", 3);
        }
        else
        {
            SignIn();
        }
    }

    public void SignOutButton()
    {
        buttonSound.Play();
        if (!Social.localUser.authenticated)
        {
            if (profileMsgTxtObject != null) profileMsgTxtObject.SetActive(true);
            profileMsgTxt.color = Color.red;
            if (profileMsgTxt != null) profileMsgTxt.text = "Already Logged Out";
            Invoke("DesableProfilemsgText", 3);
        }
        else
        {
            PlayGamesPlatform.Instance.SignOut();
            if (profileMsgTxtObject != null) profileMsgTxtObject.SetActive(true);
            profileMsgTxt.color = Color.red;
            if (profileMsgTxt != null) profileMsgTxt.text = "LogOut Sucessfully";
            Invoke("DesableProfilemsgText", 3);
        }
    }

    private void DesablemsgText()
    {
        msgTextObject.SetActive(false);
    }

    private void DesableProfilemsgText()
    {
        profileMsgTxtObject.SetActive(false);
    }
    #endregion /Sign In/Out Button

    #region LeaderBoard
    public static void PostToLeaderboard(long newScore)
    {
        Social.ReportScore(newScore, "CgkIm7HY6ZwREAIQAQ", (bool success) => {
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
        buttonSound.Play();
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkIm7HY6ZwREAIQAQ");
        }
        else
        {
            profileMsgTxtObject.SetActive(true);
            profileMsgTxt.color = Color.red;
            profileMsgTxt.text = "You are Logged Out.Login First.";

            Invoke("DesableProfilemsgText", 3);
           msgTextObject.SetActive(true);
                msgText.text = "You are Logged Out." + "\n" +
                    "Login First.";
                msgText.color = Color.red;
                Invoke("DesablemsgText", 3);
        }
    }
    #endregion /LeaderBoard

    #region Saved Games
    //making a string out of game data (highscores...)
    string GameDataToString()
    {
        return JsonUtil.CollectionToJsonString(GameDataVariable.dataVariables, "myKey");
    }

    //this overload is used when user is connected to the internet
    //parsing string to game data (stored in CloudVariables), also deciding if we should use local or cloud save
    void StringToGameData(string cloudData, string localData)
    {
        if (cloudData == string.Empty)
        {
            StringToGameData(localData);
            isCloudDataLoaded = true;
            return;
        }
        int[] cloudArray = JsonUtil.JsonStringToArray(cloudData, "myKey", str => int.Parse(str));

        if (localData == string.Empty)
        {
            GameDataVariable.dataVariables = cloudArray;
            PlayerPrefs.SetString(SAVE_NAME, cloudData);
            isCloudDataLoaded = true;
            return;
        }
        int[] localArray = JsonUtil.JsonStringToArray(localData, "myKey", str => int.Parse(str));

        //if it's the first time that game has been launched after installing it and successfuly logging into Google Play Games
        if (PlayerPrefs.GetInt("IsFirstTime") == 1)
        {
            //set playerpref to be 0 (false)
            PlayerPrefs.SetInt("IsFirstTime", 0);
            for (int i = 0; i < cloudArray.Length; i++)
                if (cloudArray[i] > localArray[i]) //cloud save is more up to date
                {
                    //set local save to be equal to the cloud save
                    PlayerPrefs.SetString(SAVE_NAME, cloudData);
                }
        }
        //if it's not the first time, start comparing
        else
        {
            for (int i = 0; i < cloudArray.Length; i++)
                //comparing integers, if one int has higher score in it than the other, we update it
                if (localArray[i] > cloudArray[i])
                {
                    //update the cloud save, first set CloudVariables to be equal to localSave
                    GameDataVariable.dataVariables = localArray;
                    isCloudDataLoaded = true;
                    //saving the updated CloudVariables to the cloud
                    SaveData();
                    return;
                }
        }
        //if the code above doesn't trigger return and the code below executes,
        //cloud save and local save are identical, so we can load either one
        GameDataVariable.dataVariables = cloudArray;
        isCloudDataLoaded = true;
    }

    //this overload is used when there's no internet connection - loading only local data
    void StringToGameData(string localData)
    {
        if (localData != string.Empty)
            GameDataVariable.dataVariables = JsonUtil.JsonStringToArray(localData, "myKey",
                                                                        str => int.Parse(str));
    }

    //used for loading data from the cloud or locally
    public void LoadData()
    {
        //basically if we're connected to the internet, do everything on the cloud
        if (Social.localUser.authenticated)
        {
            isSaving = false;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(SAVE_NAME,
                DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
        }
        //this will basically only run in Unity Editor, as on device,
        //localUser will be authenticated even if he's not connected to the internet (if the player is using GPG)
        else
        {
            LoadLocal();
        }
    }

    private void LoadLocal()
    {
        StringToGameData(PlayerPrefs.GetString(SAVE_NAME));
    }

    //used for saving data to the cloud or locally
    public void SaveData()
    {
        //if we're still running on local data (cloud data has not been loaded yet), we also want to save only locally
        if (!isCloudDataLoaded)
        {
            SaveLocal();
            return;
        }
        //same as in LoadData
        if (Social.localUser.authenticated)
        {
            isSaving = true;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(SAVE_NAME,
                DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
        }
        else
        {
            SaveLocal();
        }
    }

    private void SaveLocal()
    {
        PlayerPrefs.SetString(SAVE_NAME, GameDataToString());
    }

    private void ResolveConflict(IConflictResolver resolver, ISavedGameMetadata original, byte[] originalData,
        ISavedGameMetadata unmerged, byte[] unmergedData)
    {
        if (originalData == null)
            resolver.ChooseMetadata(unmerged);
        else if (unmergedData == null)
            resolver.ChooseMetadata(original);
        else
        {
            //decoding byte data into string
            string originalStr = Encoding.ASCII.GetString(originalData);
            string unmergedStr = Encoding.ASCII.GetString(unmergedData);

            //parsing
            int[] originalArray = JsonUtil.JsonStringToArray(originalStr, "myKey", str => int.Parse(str));
            int[] unmergedArray = JsonUtil.JsonStringToArray(unmergedStr, "myKey", str => int.Parse(str));

            for (int i = 0; i < originalArray.Length; i++)
            {
                //if original score is greater than unmerged
                if (originalArray[i] > unmergedArray[i])
                {
                    resolver.ChooseMetadata(original);
                    return;
                }
                //else (unmerged score is greater than original)
                else if (unmergedArray[i] > originalArray[i])
                {
                    resolver.ChooseMetadata(unmerged);
                    return;
                }
            }
            //if return doesn't get called, original and unmerged are identical
            //we can keep either one
            resolver.ChooseMetadata(original);
        }
    }

    private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        //if we are connected to the internet
        if (status == SavedGameRequestStatus.Success)
        {
            //if we're LOADING game data
            if (!isSaving)
                LoadGame(game);
            //if we're SAVING game data
            else
                SaveGame(game);
        }
        //if we couldn't successfully connect to the cloud, runs while on device,
        //the same code that is in else statements in LoadData() and SaveData()
        else
        {
            if (!isSaving)
                LoadLocal();
            else
                SaveLocal();
        }
    }

    private void LoadGame(ISavedGameMetadata game)
    {
        ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, OnSavedGameDataRead);
    }

    private void SaveGame(ISavedGameMetadata game)
    {
        string stringToSave = GameDataToString();
        //saving also locally (can also call SaveLocal() instead)
        PlayerPrefs.SetString(SAVE_NAME, stringToSave);

        //encoding to byte array
        byte[] dataToSave = Encoding.ASCII.GetBytes(stringToSave);
        //updating metadata with new description
        SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();
        //uploading data to the cloud
        ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, update, dataToSave,
            OnSavedGameDataWritten);
    }

    //callback for ReadBinaryData
    private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] savedData)
    {
        //if reading of the data was successful
        if (status == SavedGameRequestStatus.Success)
        {
            string cloudDataString;
            //if we've never played the game before, savedData will have length of 0
            if (savedData.Length == 0)
                //in such case, we want to assign default value to our string
                cloudDataString = string.Empty;
            //otherwise take the byte[] of data and encode it to string
            else
                cloudDataString = Encoding.ASCII.GetString(savedData);

            //getting local data (if we've never played before on this device, localData is already
            //string.Empty, so there's no need for checking as with cloudDataString)
            string localDataString = PlayerPrefs.GetString(SAVE_NAME);

            //this method will compare cloud and local data
            StringToGameData(cloudDataString, localDataString);
        }
    }

    //callback for CommitUpdate
    private void OnSavedGameDataWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {

    }
    #endregion /Saved Games


    IEnumerator PlayerImage()
    {
        Texture2D tex;
        while(Social.localUser.image == null)
        {
            yield return null;
        }
        tex = Social.localUser.image;
        if(playerProfile != null)
            playerProfile.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0f, 0f));
        if (playerProfile2 != null)
            playerProfile2.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0f, 0f));
    }

    public void ChangeProfileButton()
    {
        if(Social.localUser.authenticated)
        {
            Application.OpenURL("https://play.google.com/store/apps/details?id=com.google.android.play.games&hl=en_IN&gl=US");
        }
        else
        {
            if (profileMsgTxtObject != null) profileMsgTxtObject.SetActive(true);
            if(profileMsgTxt != null)
            {
                profileMsgTxt.color = Color.red;
                profileMsgTxt.text = "You are Loggout ! Login First";
            }
        }
    }
         
}
