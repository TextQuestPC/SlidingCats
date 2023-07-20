﻿/*
 * Created on 2022
 *
 * Copyright (c) 2022 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */

using System;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour
{
    public GameObject GDPR;

    public static AdManager Instance;

    public bool CanShowReward
    {
        get => Advertisements.Instance.IsRewardVideoAvailable();
    }

    protected void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);

            Instance = this;

            if (Advertisements.Instance.UserConsentWasSet() == false)
            {
                if (GDPR == null)
                {
                    GameObject original = Resources.Load<GameObject>("CanvasGDPR");
                    GDPR = UnityEngine.Object.Instantiate<GameObject>(original);
                }

                GDPR.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                StartCoroutine(InitAd());
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    private IEnumerator InitAd()
    {
        Advertisements.Instance.Initialize();
        
        while (!Advertisements.Instance.IsInterstitialAvailable())
        {
            yield return new WaitForSeconds(0.5f);
        }

        SceneManager.LoadScene(1);
    }


    public void OnUserClickAccept()
    {
        Advertisements.Instance.SetUserConsent(true);
        GDPR.SetActive(false);
        Time.timeScale = 1;
        Destroy(GDPR);

        StartCoroutine(InitAd());
    }


    public void OnUserClickCancel()
    {
        Advertisements.Instance.SetUserConsent(false);
        GDPR.SetActive(false);
        Time.timeScale = 1;
        Destroy(GDPR);

        StartCoroutine(InitAd());
    }

    public void ShowInterstitial()
    {
        Advertisements.Instance.ShowInterstitial();
    }

    public void ShowBanner()
    {
        Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM, BannerType.Banner);
    }

    public void ShowReward(UnityAction<bool> callback)
    {
        Advertisements.Instance.ShowRewardedVideo(callback);
    }

    public void OnUserClickPrivacyPolicy()
    {
        Application.OpenURL("https://kogda.pro/decor-renovte-privacy-policy");
    }
}