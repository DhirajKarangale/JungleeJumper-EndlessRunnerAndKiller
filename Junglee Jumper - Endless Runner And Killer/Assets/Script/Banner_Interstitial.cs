using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class Banner_Interstitial : MonoBehaviour
{
    private const string gameId = "4086101";
    private string surfacingId = "Banner_Android";
    private string placement_Interstitial = "Interstitial_Android";
    private bool testMode = false;

    private void Start()
    {
         int currentScene = SceneManager.GetActiveScene().buildIndex;
        Advertisement.Initialize (gameId,testMode);
        if(currentScene == 0)
        {
            StartCoroutine(ShowBannerWhenInitialized());
        }
        else
        {
            Advertisement.Banner.Hide();
        }
        Invoke("ShowInterstitialAd",3f);
       
        if(currentScene == 1) 
        {
          Advertisement.Banner.Hide();
        }
    }

    private void Update()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if(currentScene == 1) 
        {
          Advertisement.Banner.Hide();
        }
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
        Debug.Log("Banner Ad Sucess DK");
    }
}
