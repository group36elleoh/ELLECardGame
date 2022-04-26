using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableUseBuy : MonoBehaviour
{
    public Button UseBuyButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int userCredits = PlayerPrefsManager.GetCredits();
        string currentlySelected = BackgroundShopManager.getCurrentlySelected();
        if (currentlySelected == null)
            currentlySelected = "";
        int cost = BackgroundShopManager.getCurrentCost();
        if (cost == null)
            cost = 0;
        bool available = BackgroundShopManager.searchList(currentlySelected);
        if (available)
        {
            Debug.LogError("owned");
            UseBuyButton.interactable = true;
        }
        else if (userCredits >= cost)
        {
            Debug.LogError("can afford");
            UseBuyButton.interactable = true;
        }
        else
        {
            Debug.LogError("too expensive");
            UseBuyButton.interactable = false;
        }
    }

    public void EnableUseBuyButton()
    {
        int userCredits = PlayerPrefsManager.GetCredits();
        string currentlySelected = BackgroundShopManager.getCurrentlySelected();
        int cost = BackgroundShopManager.getCurrentCost();
        bool available = BackgroundShopManager.searchList(currentlySelected);
        if (available)
        {
            Debug.LogError("owned");
            UseBuyButton.interactable = true;
        }
        else if (userCredits >= cost)
        {
            Debug.LogError("can afford");
            UseBuyButton.interactable = true;
        }
        else
        {
            Debug.LogError("too expensive");
            UseBuyButton.interactable = false;
        }
    }
}
