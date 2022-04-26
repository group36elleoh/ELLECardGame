using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UseBuyScript : MonoBehaviour
{
    public TMP_Text UseText;
    // Start and Update are identical
    void Start()
    {
        string selection = BackgroundShopManager.getCurrentlySelected();
        int cost = BackgroundShopManager.getCurrentCost();
        string selectedImage = PlayerPrefsManager.GetSelectedBackgroundImage();
        string selectedCard = PlayerPrefsManager.GetSelectedCard();
        // selection should theoretically always be null as the start
        if (selection != null)
        {
            if (selectedImage == selection || selectedCard == selection)
                UseText.text = "Selected";
            else if (BackgroundShopManager.searchList(selection) || BackgroundShopManager.searchCardList(selection))
                UseText.text = "Use";
            else if (selection == "DefaultCard" || selection == "Default")
                UseText.text = "Use";
            else
                UseText.text = "Buy (" + cost.ToString() + ")";
        }
        else
        {
            UseText.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        string selection = BackgroundShopManager.getCurrentlySelected();
        int cost = BackgroundShopManager.getCurrentCost();
        string selectedImage = PlayerPrefsManager.GetSelectedBackgroundImage();
        string selectedCard = PlayerPrefsManager.GetSelectedCard();
        if (selection != null)
        {
            if (selectedImage == selection || selectedCard == selection)
                UseText.text = "Selected";
            else if (BackgroundShopManager.searchList(selection) || BackgroundShopManager.searchCardList(selection))
                UseText.text = "Use";
            else if (selection == "DefaultCard" || selection == "Default")
                UseText.text = "Use";
            else
                UseText.text = "Buy (" + cost.ToString() + ")";
        }
        else
        {
            UseText.text = "";
        }
    }
    

}
