using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ExpSystem : MonoBehaviour
{
    // MaxExp multiplier
    float multiplierMX = 1.5f;

    [SerializeField] GameManager gameManager;
    [SerializeField]
    private int maxExp = 1000;
    public int MaxExp { 
        get
        {
            return maxExp;
        }
        set
        {
            maxExp = value;
        }
    }

    private int currentExp;

    public int CurrentExp
    {
        get
        {
            return currentExp;
        }

        set
        {
            currentExp = value;

            PlayerPrefs.SetInt("currentExp", currentExp);
        }
    }

    public Image ExpBar;

    //public float expIncreasedPerSecond = 5;

    public int playerLevel;

    public TMP_Text levelText;

    [SerializeField]
    Button button;

    void Start()
    {
        playerLevel = PlayerPrefs.GetInt("playerLevel");

        MaxExp = PlayerPrefs.GetInt("MaxExp");

        CurrentExp = PlayerPrefs.GetInt("currentExp");

        multiplierMX = PlayerPrefs.GetFloat("multiplierMX");
    }


    void Update()
    {
        //updatedExp += expIncreasedPerSecond * Time.deltaTime;

        ExpBar.fillAmount = 1 - (float)CurrentExp / MaxExp;

        levelText.text = $"level {playerLevel}";
        if (CurrentExp >= MaxExp)
        {
            levelText.text = "Next Level";

            button.interactable = true;
        }

    }

    public void NextLevel()
    {
        playerLevel++;

        CurrentExp = 0;

        MaxExp = (int)((float)MaxExp * multiplierMX);

        multiplierMX *= 1.5f;

        MaxExp -= MaxExp % 25;

        button.interactable = false;

        PlayerPrefs.SetInt("playerLevel", playerLevel);
        PlayerPrefs.SetInt("MaxExp", MaxExp);

        PlayerPrefs.SetFloat("multiplierMX", multiplierMX);

        gameManager.IncrementScore(maxExp, false);
    }

}