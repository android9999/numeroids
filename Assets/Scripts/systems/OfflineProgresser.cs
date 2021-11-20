using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class OfflineProgresser : MonoBehaviour
{
    public GameManager gameManager;


    public GameObject offlineTimePanel;
    public TMP_Text timeLabel;
    public TMP_Text revenueLabel;

    //DateTime logInTime = new DateTime(2021, 8, 12, 10, 45, 20);

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("lastLogin"))
        {
            DateTime lastLogin = DateTime.Parse(PlayerPrefs.GetString("lastLogin"));

            TimeSpan timeSpan = DateTime.Now - lastLogin;

            string PastedTime = string.Format("you have been offline for  {0}d {1}h {2}m {3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

            int offlineRevenue = (int)((float)timeSpan.TotalMinutes * GameManager.offlineRevenue* gameManager.averageScoreInMinute);

            offlineRevenue -= offlineRevenue % 25;

            gameManager.IncrementScore(offlineRevenue);

            if (timeSpan.TotalSeconds > 60)
            {

                offlineTimePanel.SetActive(true);
                timeLabel.text = PastedTime;
                revenueLabel.text = "your offline revenue is " + offlineRevenue.ToString();
            }
        }
    }

    // Update is called once per frame
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("lastLogin", DateTime.Now.ToString());
    }

    public void CloseOfflineTimePanel()
    {
        offlineTimePanel.SetActive(false);
    }
}
