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
            if (UnityEngine.Random.Range(0, 3) == 2) Invoke("ShowInterstitialAd", 2f);
       }
    }

    public void ShowInterstitialAd()
    {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady())
        {
            Advertisement.Show("Interstitial_Android");
        }
    }
}
