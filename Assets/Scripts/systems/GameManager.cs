using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{

    public Player player;
    public ParticleSystem explosionEffect;
    public GameObject shopPanel;
    public GameObject gameOverUI;
    public AsteroidSpawner asteroidSpawner;
    public ExpSystem expSystem;

    int score = 0;

    public int Score { get { return score; } private set { score = value; CheckButtons(); scoreText.text = value.ToString(); } }



    public Text scoreText;

    public Button[] buttons;

    public TMP_Text[] priceLabels;

    int lives;

    public int Lives { get { return lives; } private set { lives = value; livesText.text = lives.ToString(); } }
    public Text livesText;

    bool gameIsStarted = false;

    [SerializeField]
    int startingLives = 1;

    public static float invincibilityTime = 3.0f;

    public static float fireRate = 1;

    public static int damage = 1;

    //5 normally speed is equal to five
    public static float speed = 1;

    public static float mass = 1;

    public static float autorotationSpeed = 0.1f;

    public static int gainedScore = 25;

    public static int asteroidsHealth = 1;

    public static float offlineRevenue = 1;

    public static float timeScale = 1;

    public int NewScore { get; set; }




    public List<int> gainedScores = new List<int>();

    public int averageScoreInMinute = 25;

    [SerializeField]
    GameObject consumableObject;

    public TMP_Text[] quantityOfConsumablesLabels;

    public static int tapDamage = 1;

    public static float tapRate = 1;

    public static float tapArea = 0;

    //5 normally speed is equal to five
    public static int generatedBullet = 0;

    public static float BarrierDuration = 0.1f;

    public static float BarrierHealth = 0.1f;

    public static int BarrierDamage = 1;

    public static float tapRevenue = 1;

    public static float tapConsumablesDropRate = 1;

    private void SetStats()
    {
        if (PlayerPrefs.HasKey("startingLives"))
        {
            startingLives = (int)PlayerPrefs.GetFloat("startingLives");
        }

        else
        {
            startingLives = 1;
        }

        invincibilityTime = PlayerPrefs.GetFloat("invincibilityTime");

        fireRate = PlayerPrefs.GetFloat("fireRate");

        damage = (int)PlayerPrefs.GetFloat("damage");

        speed = PlayerPrefs.GetFloat("speed");

        mass = PlayerPrefs.GetFloat("mass");

        autorotationSpeed = PlayerPrefs.GetFloat("autorotationSpeed");

        gainedScore = (int)PlayerPrefs.GetFloat("gainedScore");

        asteroidsHealth = (int)PlayerPrefs.GetFloat("asteroidsHealth");

        offlineRevenue = PlayerPrefs.GetFloat("offlineRevenue");

        timeScale = PlayerPrefs.GetFloat("timeScale") + 1;

        Time.timeScale = timeScale;
    }

    private void Start()
    {
        CheckConsumables();

        NewGame();

        StartCoroutine(CalculateEarnedScoreInAMinute());
    }



    public void ClickToStart()
    {
        if (!gameIsStarted)
        {
            NewGame();

            gameIsStarted = true;
        }
    }

    private void Update()
    {
        if (Lives <= 0) {
            NewGame();
        }

        Debug.Log("Score gained in a minute is equal to = " + averageScoreInMinute);
    }

    public void NewGame()
    {
        asteroidSpawner.round = 0;

        AsteroidSpawner.asteroidsHealth = asteroidsHealth;

        Asteroid[] asteroids = FindObjectsOfType<Asteroid>();

        for (int i = 0; i < asteroids.Length; i++) {
            Destroy(asteroids[i].gameObject);
        }

        InitializeScore();

        SetStats();

        Lives = startingLives;
        Respawn();
    }

    public void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.SetActive(true);
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosionEffect.transform.position = asteroid.transform.position;
        this.explosionEffect.Play();

        int maxHealth = asteroid.maxHealth;

        if (asteroid.size < 0.7f) {
            IncrementScore(gainedScore * maxHealth * 4); // small asteroid
        } else if (asteroid.size < 1.4f) {
            IncrementScore(gainedScore * maxHealth * 2); // medium asteroid
        } else {
            IncrementScore(gainedScore* maxHealth); // large asteroid
        }

        bool instantiateConsumable = asteroid.size > UnityEngine.Random.Range(0, 1000);
        if (instantiateConsumable)
        {
            Debug.Log("Consumable Should Be Aviable");

            GameObject consumableObject = Instantiate(this.consumableObject, asteroid.transform);

            consumableObject.transform.parent = null;

            consumableObject.GetComponent<ConsumableObject>().size = asteroid.size;
        }
    }

    public void ConsumableTaken(ConsumableObject consumableObject)
    {
        this.explosionEffect.transform.position = consumableObject.transform.position;
        this.explosionEffect.Play();

        GainConsumables(consumableObject.consumable);
    }

    public void PlayerDeath(Player player)
    {
        this.explosionEffect.transform.position = player.transform.position;
        this.explosionEffect.Play();

        Lives--;

        if (Lives <= 0) {
            GameOver();
        } else {
            Invoke(nameof(Respawn), player.respawnDelay);
        }
    }

    public void GameOver()
    {
        NewGame();
    }

    private void InitializeScore()
    {
        Score = PlayerPrefs.GetInt("score");
    }

    public void IncrementScore(int gainedScore)
    {
        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + gainedScore);

        expSystem.CurrentExp += gainedScore;

        gainedScores.Add(gainedScore);

        Score = PlayerPrefs.GetInt("score");

        //CheckButtons();

        //Debug.Log("score = " + score);

    }

    private void DecrementScore(int losedScore)
    {
        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") - losedScore);

        Score = PlayerPrefs.GetInt("score");

        //CheckButtons();

        //Debug.Log("score = " + score);

    }


    public void SetScore(int newScore)
    {
        PlayerPrefs.SetInt("score", newScore);

        Score = PlayerPrefs.GetInt("score");

        Debug.Log("score = " + Score);

    }


    public void SetScore()
    {
        PlayerPrefs.SetInt("score", NewScore);

        Score = PlayerPrefs.GetInt("score");

        Debug.Log("score = " + Score);

        //CheckButtons();
    }

    public void IncrementCurrentLives()
    {
        Lives++;
    }

    /*
    public void IncrementStartingLives()
    {
        PlayerPrefs.SetInt("startingLives", PlayerPrefs.GetInt("startingLives") +1);

        Debug.Log("startingLives = " + PlayerPrefs.GetInt("startingLives"));

        SetStats();
    }
    
    public void IncrementInvincibilityTime()
    {
        PlayerPrefs.SetFloat("invincibilityTime", PlayerPrefs.GetFloat("invincibilityTime") + 0.25f);

        Debug.Log("invincibilityTime = " + PlayerPrefs.GetFloat("invincibilityTime"));

        SetStats();
    }
    

    public void IncrementFireRate()
    {
        PlayerPrefs.SetFloat("fireRate", PlayerPrefs.GetFloat("fireRate") + 0.5f);

        Debug.Log("fireRate = " + PlayerPrefs.GetFloat("fireRate"));

        SetStats();
    }
    


    public void IncrementDamage()
    {
        PlayerPrefs.SetInt("damage", PlayerPrefs.GetInt("damage") + 1);

        Debug.Log("damage = " + PlayerPrefs.GetInt("damage"));

        SetStats();
    }
    
    public void IncrementSpeed()
    {
        PlayerPrefs.SetFloat("speed", PlayerPrefs.GetFloat("speed") + 0.5f);

        Debug.Log("speed = " + PlayerPrefs.GetFloat("speed"));

        SetStats();
    }

    public void IncrementMass()
    {
        PlayerPrefs.SetFloat("mass", PlayerPrefs.GetFloat("mass") + 0.5f);

        Debug.Log("mass = " + PlayerPrefs.GetInt("mass"));

        SetStats();
    }

    public void IncrementAutorotationSpedd()
    {
        PlayerPrefs.SetFloat("autorotationSpeed", PlayerPrefs.GetFloat("autorotationSpeed") + 50f);

        Debug.Log("autorotationSpeed = " + PlayerPrefs.GetFloat("autorotationSpeed"));

        SetStats();
    }

    public void IncrementGainedScore()
    {
        PlayerPrefs.SetInt("gainedScore", PlayerPrefs.GetInt("gainedScore") + 25);

        Debug.Log("gainedScore = " + PlayerPrefs.GetInt("gainedScore"));

        SetStats();
    }

    */

    public void IncrementStat(Stat stat)
    {
        PlayerPrefs.SetFloat(stat.statName.ToString(), PlayerPrefs.GetFloat(stat.statName.ToString()) + stat.increment);

        DecrementScore(PlayerPrefs.GetInt(stat.statName.ToString() + "Price"));

        PlayerPrefs.SetInt(stat.statName.ToString()+ "Price", PlayerPrefs.GetInt(stat.statName.ToString() + "Price") + stat.incrementPrice);

        priceLabels[(int)stat.statName].text = (PlayerPrefs.GetInt(stat.statName.ToString() + "Price")).ToString();

        Debug.Log(stat.statName.ToString());

        Debug.Log(stat.statName.ToString() + " = " + PlayerPrefs.GetFloat(stat.statName.ToString()));

        SetStats();
    }

    void CheckConsumables() 
    {
        foreach (string name in Enum.GetNames(typeof(Consumable.ConsumableName)))
        {
            int quantity = PlayerPrefs.GetInt(name);

            Consumable.ConsumableName consumableName;

            Enum.TryParse(name, out consumableName);

            consumablesButtons[(int)consumableName].interactable = quantity > 0;

            quantityOfConsumablesLabels[(int)consumableName].text = quantity.ToString();
        }
    }

    public void UseConsumables(Consumable consumable)
    {
        int quantity = PlayerPrefs.GetInt(consumable.consumableName.ToString()) - 1;

        PlayerPrefs.SetInt(consumable.consumableName.ToString(), quantity);

        consumablesButtons[(int)consumable.consumableName].interactable = quantity > 0;

        quantityOfConsumablesLabels[(int)consumable.consumableName].text = quantity.ToString();

        Debug.Log($"consumable of type {consumable.consumableName} it's equal to {quantity}");

        UseConsumable(consumable.consumableName);
    }

    public void GainConsumables(Consumable consumable)
    {
        PlayerPrefs.SetInt(consumable.consumableName.ToString(), PlayerPrefs.GetInt(consumable.consumableName.ToString()) + 1);

        int quantity = PlayerPrefs.GetInt(consumable.consumableName.ToString());

        consumablesButtons[(int)consumable.consumableName].interactable = true;

        quantityOfConsumablesLabels[(int)consumable.consumableName].text = quantity.ToString();
    }

    public void CheckButtons()
    {
        foreach (int i in Enum.GetValues(typeof(Stat.StatName)))
        {
            if (Score >= PlayerPrefs.GetInt((((Stat.StatName)i).ToString() + "Price")))
            {
                buttons[i].interactable = true;
            }

            else 
            {
                buttons[i].interactable = false;
            }
        }
    }


    public void ResetStats()
    {
        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetFloat("startingLives", 1);

        PlayerPrefs.SetInt("startingLivesPrice", 1000);

        PlayerPrefs.SetFloat("invincibilityTime", 3);

        PlayerPrefs.SetInt("invincibilityTimePrice", 100);

        PlayerPrefs.SetFloat("fireRate", 1);

        PlayerPrefs.SetInt("fireRatePrice", 100);

        PlayerPrefs.SetFloat("damage", 1);

        PlayerPrefs.SetInt("damagePrice", 100);

        PlayerPrefs.SetFloat("speed", 1);

        PlayerPrefs.SetInt("speedPrice", 100);

        PlayerPrefs.SetFloat("autorotationSpeed", 100);

        PlayerPrefs.SetInt("autorotationSpeedPrice", 100);

        PlayerPrefs.SetFloat("gainedScore", 25);

        PlayerPrefs.SetInt("gainedScorePrice", 100);

        PlayerPrefs.SetFloat("asteroidsHealth", 1);

        PlayerPrefs.SetInt("asteroidsHealthPrice", 100);

        PlayerPrefs.SetFloat("offlineRevenue", 1);

        PlayerPrefs.SetInt("offlineRevenuePrice", 100);

        PlayerPrefs.SetFloat("timeScale", 1);

        PlayerPrefs.SetInt("timeScalePrice", 100);

        //PlayerPrefs.SetFloat("mass", 1);

        //PlayerPrefs.SetFloat("mass", 1);

        SetStats();
    }

    
    IEnumerator CalculateEarnedScoreInAMinute()
    {
        while (true)
        {
            yield return new WaitForSeconds(60);

            averageScoreInMinute = 25;

            foreach (int gainedScore in gainedScores)
            {
                averageScoreInMinute += gainedScore;
            }

            gainedScores.Clear();
        }
    }

    public List<Button> consumablesButtons = new List<Button>();

    void UseConsumable(Consumable.ConsumableName consumable)
    {
        switch (consumable)
        {
            case Consumable.ConsumableName.Helper:
                UseHelper();
                break;

            case Consumable.ConsumableName.ScoreX2:
                UseScoreX2();
                break;

            case Consumable.ConsumableName.Bomb:
                UseBomb();
                break;

            case Consumable.ConsumableName.RestoreLives:
                UseRestoreLives();
                break;

            case Consumable.ConsumableName.Timeskip20m:
                UseTimeskip20m();
                break;

            case Consumable.ConsumableName.Timeskip1h:
                UseTimeskip1h();
                break;

            case Consumable.ConsumableName.Timeskip2h:
                UseTimeskip2h();
                break;

            case Consumable.ConsumableName.Timeskip4h:
                UseTimeskip4h();
                break;
        }
    }

    void UseHelper()
    {

    }

    void UseScoreX2()
    {
        SetScore(Score * 2);
    }

    [SerializeField]
    GameObject bomb;

    void UseBomb()
    {
        Instantiate(bomb);
    }

    void UseRestoreLives()
    {
        Lives = startingLives;
    }

    void UseTimeskip20m()
    {
        IncrementScore(averageScoreInMinute * 20);
    }

    void UseTimeskip1h()
    {
        IncrementScore(averageScoreInMinute * 60);
    }

    void UseTimeskip2h()
    {
        IncrementScore(averageScoreInMinute * 120);
    }

    void UseTimeskip4h()
    {
        IncrementScore(averageScoreInMinute * 240);
    }
}
