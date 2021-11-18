using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System;

public class UnityAds : MonoBehaviour, IUnityAdsListener
{
    public enum Rewards
    {
        Reward_1,
        Reward_2,
        Reward_3,
    }

    Rewards reward;

    public bool save;

    public static bool ads = true;

    [SerializeField]
    private string gameID = "";

    [Header("Ad Placement IDs")]
    [Tooltip("Add banner Placement ID .You can find it under Placements tab from Unity dashboard")]
    [SerializeField] private string bannerID = "";
    [SerializeField] BannerPosition pos;
    [Space(10)]
    [Tooltip("Add video Placement ID .You can find it under Placements tab from Unity dashboard")]
    [SerializeField] private string interstitialID = "";
    [Space(10)]
    [Tooltip("Add rewardedAds Placement ID .You can find it under Placements tab from Unity dashboard")]
    [SerializeField] private string rewardedVideoID = "rewardedVideo";
    [SerializeField] List<UnityEvent> RewardActions = new List<UnityEvent>(Enum.GetNames(typeof(Rewards)).Length);
    [Space(10)]
    public bool TestMode;
    [Header("Buttons")]
    public Button showInterstitial;
    public Button watchRewardAd;
    public TextMeshProUGUI gemsAmt;

    AdsPanel adsPanel;

    void Start()
    {
        adsPanel = FindObjectOfType<AdsPanel>();

        ads = !PlayerPrefs.HasKey("ads");

        Advertisement.Initialize(gameID, TestMode);
        Advertisement.AddListener(this);

        StartCoroutine(Set_Buttons());

        StartCoroutine(ShowAdsPanel());
    }

    IEnumerator ShowAdsPanel()
    {
        yield return new WaitForSecondsRealtime(120);

        adsPanel.ShowPanel();
    }

    void HideAdsPanel()
    {
        adsPanel.HidePanel();

        StartCoroutine(ShowAdsPanel());
    }

    IEnumerator Set_Buttons()
    {
        while (true)
        {
            if (showInterstitial != null)
            {
                showInterstitial.interactable = Advertisement.IsReady(interstitialID);
            }

            if (watchRewardAd != null)
            {
                watchRewardAd.interactable = Advertisement.IsReady(rewardedVideoID);

                adsPanel.ChooseRandomConsumable();
            }


            yield return new WaitForSeconds(3f);
        }

    }

    public void ShowInterstitial()
    {
        if (Advertisement.IsReady(interstitialID))
        {
            Advertisement.Show(interstitialID);
        }
    }

    public void ShowRewardedVideo()
    {
        Advertisement.Show(rewardedVideoID);
    }

    public void ShowRewardedVideo(Rewards reward)
    {
        this.reward = reward;

        Advertisement.Show(rewardedVideoID);
    }

    public void ShowRewardedVideo(int reward)
    {
        if (ads)
        {
            this.reward = (Rewards)reward;

            Debug.Log(this.reward);

            Advertisement.Show(rewardedVideoID);
        }

        else
        {
            this.reward = (Rewards)reward;

            GetReward();
        }

    }

    public void OnUnityAdsReady(string placementID)
    {
        if (placementID == rewardedVideoID && watchRewardAd != null)
        {
            watchRewardAd.interactable = true;
        }

        if (placementID == interstitialID && showInterstitial != null)
        {
            showInterstitial.interactable = true;
        }

        if (placementID == bannerID)
        {
            Advertisement.Banner.SetPosition(pos);
            Advertisement.Banner.Show(bannerID);
        }
    }

    public void OnUnityAdsDidFinish(string placementID, ShowResult showResult)
    {
        if (placementID == rewardedVideoID)
        {
            if (showResult == ShowResult.Finished)
            {
                HideAdsPanel();

                GetReward();
            }
            else if (showResult == ShowResult.Skipped)
            {
                //Do nothing
            }
            else if (showResult == ShowResult.Failed)
            {
                //tell player ads failed
            }
        }
    }


    public void OnUnityAdsDidError(string message)
    {
        //Show or log the error here
    }

    public void OnUnityAdsDidStart(string placementID)
    {
        //Do this if ads starts
    }

    public void GetReward()
    {
        if (RewardActions[(int)reward] != null)
        {
            Debug.Log(RewardActions[(int)reward].GetPersistentMethodName(0));

            RewardActions[(int)reward].Invoke();
        }
        /*
        if (PlayerPrefs.HasKey("gems"))
        {
            int gemAmount = PlayerPrefs.GetInt("gems");
            PlayerPrefs.SetInt("gems", gemAmount + 50);
        }
        else
        {
            PlayerPrefs.SetInt("gems", 50);
        }

        gemsAmt.text = "Gems: " + PlayerPrefs.GetInt("gems").ToString();

        */
    }

    public void ShowBannerAds()
    {
        Debug.Log("ShowBannerAds Called.");
        // Advertisement.Banner.Show(bannerPlacementId);
        StartCoroutine(ShowBannerWhenReady());
    }

    public void HideBannerAds()
    {
        Debug.Log("HideBannerAds Called.");
        Advertisement.Banner.Hide();
    }

    IEnumerator ShowBannerWhenReady()
    {
        yield return new WaitForSeconds(0.2f);
        Advertisement.Banner.SetPosition(pos);
        Advertisement.Banner.Show(bannerID);
        Debug.Log("BannerAds Position:" + pos);
    }

    public static void Remove_Ads()
    {
        PlayerPrefs.SetInt("ads", 1);

        ads = false;
    }
}