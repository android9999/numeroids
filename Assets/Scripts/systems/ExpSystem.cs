using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ExpSystem : MonoBehaviour
{
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
        playerLevel = 1;

        MaxExp = 1000;

        currentExp = 0;
    }


    void Update()
    {
        //updatedExp += expIncreasedPerSecond * Time.deltaTime;

        ExpBar.fillAmount = 1 - (float)currentExp / MaxExp;

        levelText.text = $"level {playerLevel}";
        if (currentExp >= MaxExp)
        {
            levelText.text = "Next Level";

            button.interactable = true;
        }

    }

    public void NextLevel()
    {
        playerLevel++;

        currentExp = 0;

        MaxExp = (int)((float)MaxExp * 1.5f);

        MaxExp -= MaxExp % 25;

        button.interactable = false;
    }

}