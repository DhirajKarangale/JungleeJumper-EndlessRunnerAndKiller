using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class Banner_Interstitial : MonoBehaviour
{
    private const string gameId = "4086101";
    private string surfacingId = "Banner_Android";
    private string placement_Interstitial = "Interstitial_Android";
    private bool testMode = true;

    private void Start()
    {
#if UNITY_ANDROID
        Advertisement.Initialize (gameId,testMode);
        StartCoroutine(ShowBannerWhenInitialized());
        Invoke("ShowInterstitialAd",3f);
#endif        
    }

    public void OnDestroy()
    {
      Advertisement.Banner.Hide();
    }

    private void ShowInterstitialAd()
    {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady()) 
        {
            Advertisement.Show(placement_Interstitial);
        } 
        else 
        {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }

    IEnumerator ShowBannerWhenInitialized ()
     {
        Advertisement.Banner.SetPosition (BannerPosition.TOP_CENTER);
        while (!Advertisement.isInitialized) 
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show (surfacingId);
    }
}
