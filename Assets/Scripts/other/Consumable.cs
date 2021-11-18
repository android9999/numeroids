using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Consumable", menuName = "ScriptableObjects/Consumable", order = 2)]
public class Consumable : ScriptableObject
{
    public enum ConsumableName
    {
        Helper,

        ScoreX2,

        Bomb,

        RestoreLives,

        Timeskip20m,

        Timeskip1h,

        Timeskip2h,

        Timeskip4h,
    }

    public ConsumableName consumableName;

    public Sprite sprite;

    /*bool _available = false;
    public bool available { get { return _available; } set { _available = value; } }

    [SerializeField]
    private int _quantity;
    public int quantity
    {
        get {return _quantity; }
        set { available = 0 < value; _quantity = value;}
    }*/
    /*
    [SerializeField]
    private UnityEvent _effect;

    
    public UnityEvent effect
    {
        get { return _effect; }
        set { _effect = value; }
    }
    */
}
