using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OfflineProgresser : MonoBehaviour
{
    public GameManager gameManager;

    //DateTime logInTime = new DateTime(2021, 8, 12, 10, 45, 20);

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("lastLogin"))
        {
            DateTime lastLogin = DateTime.Parse(PlayerPrefs.GetString("lastLogin"));

            TimeSpan timeSpan = DateTime.Now - lastLogin;

            string PastedTime = string.Format("{0} Days {1} Hours {2} Minutes {3} Seconds", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

            int offlineRevenue = (int)((float)timeSpan.TotalMinutes * GameManager.offlineRevenue* gameManager.averageScoreInMinute);

            offlineRevenue -= offlineRevenue % 25;

            gameManager.IncrementScore(offlineRevenue);
        }
    }

    // Update is called once per frame
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("lastLogin", DateTime.Now.ToString());
    }
}
