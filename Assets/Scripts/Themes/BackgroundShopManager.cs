using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundShopManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // since currentlySelected is used in both the background and card shop
        // resetting it at the start of the scene
        // stops a background from displaying in the card shop and vice versa
        setCurrentlySelected(null);
        
        setCurrentCost(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static string currentlySelected;
    private static int currentCost;
    
    public void setCurrentlySelected(string selection)
    {
        currentlySelected = selection;
    }

    public static string getCurrentlySelected()
    {
        return currentlySelected;
    }

    public void setCurrentCost(int cost)
    {
        currentCost = cost;
    }

    public static int getCurrentCost()
    {
        return currentCost;
    }

    public void SelectBackgroundImage(string themeName)
    {
        // if the theme has been unlocked, selects it as user's choice
        PlayerPrefsManager.SetSelectedBackgroundImage(themeName);
    }

    public void SelectCard(string cardName)
    {
         PlayerPrefsManager.SetSelectedCard(cardName);
    }

    public bool UnlockTheme(string themeName)
    {
        int userCredits = PlayerPrefsManager.GetCredits();
        int cost = getCurrentCost();
        if (userCredits >= cost)
        {
            PlayerPrefsManager.SetUnlockedThemes(themeName);
            PlayerPrefsManager.SetCredits(userCredits - cost);
            return true;
        }
        else
            return false;
    }

    public bool UnlockCard(string cardName)
    {
        int userCredits = PlayerPrefsManager.GetCredits();
        int cost = getCurrentCost();
        // technically the use/buy button shouldn't be clickable if
        // they can't afford it so this is unnecessary
        // but it doesn't hurt
        if (userCredits >= cost)
        {
            PlayerPrefsManager.SetUnlockedCards(cardName);
            PlayerPrefsManager.SetCredits(userCredits - cost);
            return true;
        }
        else
            return false;
    }

    public static bool searchList(string themeName)
    {
        string unlockedThemes = PlayerPrefsManager.GetUnlockedThemes();
        Debug.LogError(unlockedThemes);
        if (unlockedThemes.Contains(themeName))
            return true;
        else
            return false;
    }

    public static bool searchCardList(string cardName)
    {
        string unlockedCards = PlayerPrefsManager.GetUnlockedCards();
        if (unlockedCards.Contains(cardName))
            return true;
        else
            return false;
    }
    
    public void UseOrBuy()
    {
        string currentlySelected = getCurrentlySelected();

        // the defaults aren't saved in the list of unlocked themes
        if (searchList(currentlySelected) || currentlySelected == "Default")
            SelectBackgroundImage(currentlySelected);
        else
        {
            if (UnlockTheme(currentlySelected))
            {
                SelectBackgroundImage(currentlySelected);
            }
        }
    }

    public void UseOrBuyCard()
    {
        string currentlySelected = getCurrentlySelected();
        if (searchCardList(currentlySelected) || currentlySelected == "DefaultCard")
            SelectCard(currentlySelected);
        else
        {
            if (UnlockCard(currentlySelected))
                SelectCard(currentlySelected);
        }
    }
}
