using UnityEngine;

public class AdsInterstitialActivator : MonoBehaviour
{
    public void ShowAds()
    {
        if (PlayerPrefs.HasKey("COMPLETE_GUIDE"))
        {
            Advertisements.Instance.ShowInterstitial();
        }
    }
}