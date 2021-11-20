using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "ScriptableObjects/Stat", order = 1)]
public class Stat : ScriptableObject
{
    public enum StatName
    {
        startingLives,

        invincibilityTime,

        fireRate,

        damage,

        speed,

        autorotationSpeed,

        gainedScore,

        asteroidsHealth,

        offlineRevenue,

        timeScale,
    }

    public enum incrementPriceMethod
    {
        sum,
        sum2,
        sumPercentage,
        multiplication
        
    }

    incrementPriceMethod IncrementMethod = incrementPriceMethod.sum2;

    public StatName statName;

    public int increment = 100;

    public int increment2 = 50;

    int percentageIncrease = 50;

    float multiplier = 2;


    public int startingPrice = 100;

    public float startingValue;

    [SerializeField]
    private int _incrementPrice;
    public int incrementPrice
    {
        get 
        {
            switch (IncrementMethod) 
            {
                case incrementPriceMethod.sum:
                    return _incrementPrice;

                case incrementPriceMethod.sum2:
                    _incrementPrice += increment2; return _incrementPrice;

                case incrementPriceMethod.sumPercentage:
                    _incrementPrice += (_incrementPrice* percentageIncrease) / 100; return _incrementPrice;

                case incrementPriceMethod.multiplication:
                    _incrementPrice = (int)(_incrementPrice * multiplier); return _incrementPrice;

                default:
                    _incrementPrice = (int)(_incrementPrice * multiplier); return _incrementPrice;
            }
        }
        set { _incrementPrice = value; _incrementPrice -= increment % 25; }
    }


    public void SetStartingValues()
    {
        PlayerPrefs.SetFloat(statName.ToString(), startingValue);

        PlayerPrefs.SetInt(statName.ToString() + "Price", startingPrice);

        Debug.Log("OOOPPPPPSSSSSSS");
    }
}