using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.UI;


public class AdManager : MonoBehaviour, IUnityAdsListener
{
    private const string gameId = "4086101";
    private bool testMode = true;

    public static AdManager instance;

    [SerializeField] GameObject msgTextObject;
    [SerializeField] Text msgText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        Advertisement.Initialize(gameId, testMode);
        Advertisement.AddListener(this);
        ShowBannerAd();
        if(Random.Range(0,2) == 0) Invoke("ShowInterstitialAd", 2f);
    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }

    public void ShowBannerAd()
    {
        if (Advertisement.IsReady("Banner_Android"))
        {
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            Advertisement.Banner.Show("Banner_Android");
        }
        else
        {
            StartCoroutine(RepeatShowBanner());
        }
    }
    
    IEnumerator RepeatShowBanner()
    {
        yield return new WaitForSeconds(1);
        ShowBannerAd();
    }

    public void ShowInterstitialAd()
    {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady())
        {
            Advertisement.Show("Interstitial_Android");
        }
        else
        {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }

    public void ShowRewardedVideoAd()
    {
        if (Advertisement.IsReady("Rewarded_Android"))
            Advertisement.Show("Rewarded_Android");
        else
        {
            Debug.Log("Reward Ad is not loaded");
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
            msgText.color = Color.red;
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
        if((placementId == "Rewarded_Android") && (showResult == ShowResult.Finished))
        {
            if (msgTextObject != null) msgTextObject.SetActive(true);
            if (msgText != null)
            {
                msgText.color = Color.green;
                msgText.text = "You Received Reward";
                Invoke("DesableTxt", 1.8f);
            }
            GameDataVariable.dataVariables[1] += 100;
            PlayGamesController.Instance.SaveData();
        }
    }

    private void DesableTxt()
    {
        msgTextObject.SetActive(false);
    }
}
