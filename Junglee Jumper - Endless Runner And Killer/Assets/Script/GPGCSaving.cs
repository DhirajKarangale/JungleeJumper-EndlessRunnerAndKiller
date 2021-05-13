using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine.UI;

public class GPGCSaving : MonoBehaviour
{
    public bool isSaving = false;
    private string saveName = "JungleeJumperDKSoftwareData";
    public static float highScore;
    public static int coin;
    private float deviceHighScore;
    private int deviceCoin;
    private float cloudHighScore;
    private int cloudCoin;

    public bool isGPDataLoadAtBegningAllow = true;
    public bool isGPSaveAllow = true;


    public Text debugText;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        deviceHighScore = PlayerPrefs.GetFloat("HighScore", 0f);
        deviceCoin = PlayerPrefs.GetInt("Coin", 0);
    }

    private void Update()
    {
        if(isGPDataLoadAtBegningAllow)
        {
            isGPDataLoadAtBegningAllow = false;
            if (Social.localUser.authenticated)
            {
                OnSaveToCloud(false);
                if ((deviceCoin > cloudCoin) || (deviceHighScore > cloudHighScore))
                {
                    highScore = deviceHighScore;
                    coin = deviceCoin;
                    OnSaveToCloud(true);
                }
                else
                {
                    highScore = cloudHighScore;
                    coin = cloudCoin;
                    PlayerPrefs.SetFloat("HighScore", highScore);
                    PlayerPrefs.SetInt("Coin", coin);
                }
            }
            else
            {
                highScore = PlayerPrefs.GetFloat("HighScore", 0);
                coin = PlayerPrefs.GetInt("Coin", 0);
            }
        }

        if(isGPSaveAllow)
        {
            if (Player.isPlayerDead)
            {
                if (Social.localUser.authenticated) PlayGamesController.PostToLeaderboard(long.Parse(highScore.ToString()));
                PlayerPrefs.SetFloat("HighScore", highScore);
                PlayerPrefs.SetInt("Coin", coin);
                OnSaveToCloud(true);
            }
            isGPSaveAllow = false;
        }

        if(!Player.isPlayerDead)
        {
            isGPSaveAllow = true;
        }
    }

    private void OnApplicationQuit()
    {
        isGPDataLoadAtBegningAllow = true;
    }

    public void OnSaveToCloud(bool saving)
    {
        if (Social.localUser.authenticated)
        {
            isSaving = saving;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(saveName, GooglePlayGames.BasicApi.DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseMostRecentlySaved, SavedGameOpen);
        }
    }

    private void SavedGameOpen(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            if (isSaving)
            {
                byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(GetDataToStoreInCloud());
                SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();
                ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(meta, update, data, SaveUpdate);
            }
            else
            {
                ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(meta, ReadDataFromCloud);
            }
        }
    }

    private void ReadDataFromCloud(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            string savedData = System.Text.ASCIIEncoding.ASCII.GetString(data);
            LoadDataFromCloudToOurGame(savedData);
        }
    }

    private void LoadDataFromCloudToOurGame(string savedData)
    {
        string[] data = savedData.Split('|');
        cloudHighScore = float.Parse(data[0]);
        cloudCoin = int.Parse(data[1]);
        if(debugText != null)
        {
            debugText.text = "Data load from cloud";
        }
    }

    private void SaveUpdate(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        //msgText.text = "Progress Save Sucessfully";
    }

    private string GetDataToStoreInCloud()
    {
        string data = "";
        data += highScore;
        data += "|";
        data += coin;
        if(debugText != null)
        {
            debugText.text = "Data save to cloud";
        }
        return data;
    }
}
