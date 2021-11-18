using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;

    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;
    public TabButton selectedTab;
    public List<GameObject> objectsToSwap;

    public void Subscribes(TabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();

        if (selectedTab == null || selectedTab != button)
        {
            button.background.sprite = tabHover;
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        if (selectedTab != null)
        {
            selectedTab.Deselect();
        }

        selectedTab = button;

        selectedTab.Select();

        ResetTabs();

        button.background.sprite = tabActive;
        /*
        Debug.Log("Sprite Location : " + AssetDatabase.GetAssetPath(tabActive));


        SetTextureImporterFormat(tabActive.texture, true);

        
        byte[] imgByte;
        string path = @"C:\Unity\Screenshot\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
        path = path.Replace(":", "");
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
        texture.Apply();

        tabActive.texture.isReadable = true;
        imgByte = tabActive.texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(path, imgByte);
        Debug.Log($"Path : {path}");
        */





        int index = button.transform.GetSiblingIndex();

        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }

            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach (TabButton button in tabButtons)
        {
            if (selectedTab != null && selectedTab == button)
            {
                continue;
            }

            button.background.sprite = tabIdle;
        }
    }
    /*
    public static void SetTextureImporterFormat(Texture2D texture, bool isReadable)
    {
        if (null == texture) return;

        string path = @"C:\Unity\Screenshot\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
        path = path.Replace(":", "");
        var tImporter = AssetImporter.GetAtPath(path) as TextureImporter;
        if (tImporter != null)
        {
            tImporter.textureType = TextureImporterType.Advanced;

            tImporter.isReadable = isReadable;

            AssetDatabase.ImportAsset(path);
            AssetDatabase.Refresh();
        }
    }*/
}
