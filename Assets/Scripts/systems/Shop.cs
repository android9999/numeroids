using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    GameObject shopPanel;

    public void ToggleShopPanel(bool state)
    {
        shopPanel.SetActive(state);
    }
}
