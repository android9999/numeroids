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

    public StatName statName;

    public int increment = 100;

    public int increment2 = 50;

    [SerializeField]
    private int _incrementPrice;
    public int incrementPrice
    {
        get { _incrementPrice += increment2; return _incrementPrice;}
        set { _incrementPrice = value; }
    }

}