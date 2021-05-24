using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;


public class AdManager : MonoBehaviour
{
    public static AdManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
       if(GameDataVariable.dataVariables[11] != 1)
       {
            Advertisement.Initialize("4086101", false);
            ShowBannerAd();
            if (UnityEngine.Random.Range(0, 3) == 2) Invoke("ShowInterstitialAd", 2f);
       }
    }

    private void OnDestroy()
    {
        Advertisement.Banner.Hide();
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
}
