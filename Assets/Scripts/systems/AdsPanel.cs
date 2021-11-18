using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdsPanel : MonoBehaviour
{
    

    [SerializeField]
    Image icon;

    [SerializeField]
    private List<Consumable> consumables = new List<Consumable>();

    [SerializeField]
    public Consumable consumable;

    public TMP_Text name;

    GameManager gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        //AddPhysics2DRaycaster();

        ChooseRandomConsumable();

        gameManager = FindObjectOfType<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //LeanTween.scale(gameObject, Vector3.one, 1);
    }

    // Start is called before the first frame update
    public void ShowPanel()
    {
        LeanTween.scale(gameObject,Vector3.one , 1);
    }

    public void HidePanel()
    {
        LeanTween.scale(gameObject, Vector3.zero, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Reward()
    {
        gameManager.UseConsumables(consumable);
    }

    public void ChooseRandomConsumable()
    {
        int random = Random.Range(0, consumables.Count);

        consumable = consumables[random];

        icon.sprite = consumable.sprite;

        name.text = "Get " + consumable.name;
    }
}
