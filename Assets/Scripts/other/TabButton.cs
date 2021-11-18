using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public Image background;

    public TabGroup tabGroup;

    public UnityEvent onTabSelected;

    public UnityEvent onTabDeselected;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribes(this);
    }

    public void Select()
    {
        if (onTabSelected != null)
        {
            onTabSelected.Invoke();

            SelectAction();
        }
    }

    public void Deselect()
    {
        if (onTabDeselected != null)
        {
            onTabDeselected.Invoke();

            DeselectAction();
        }
    }

    // Update is called once per frame
    void SelectAction()
    {
        GetComponent<Image>().color = Color.white;
        transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.black;
    }

    void DeselectAction()
    {
        GetComponent<Image>().color = Color.black;
        transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
    }
}
