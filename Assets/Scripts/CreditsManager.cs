using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreditsManager : MonoBehaviour
{
    // UI Elements
    [SerializeField] TMPro.TMP_Text totalCreditText;
    [SerializeField] TMPro.TMP_Text creditsEarnedText;
    [SerializeField] TMPro.TMP_Text streakText;
    [SerializeField] TMPro.TMP_Text multiplierText;

    // Credit System
    public int startingValue = 100;
    public int streakLength = 5;
    public int incrementValue = 20;
    [HideInInspector] public int currentStreak;
    [HideInInspector] public int creditSalary; // Credits earned when a question is currently answered correctly
    [HideInInspector] public int totalCredits; // Total number of credits the player currently has
    [HideInInspector] public int creditsEarned; // Total number of credits earned so far this round

    public void UpdateCreditDisplay()
    {
        //totalCreditText.text = "Credits: " + PlayerPrefsManager.GetCredits().ToString();
        totalCreditText.text = totalCredits.ToString();
        creditsEarnedText.text = "( " + creditsEarned.ToString() + " )";
        streakText.text = currentStreak.ToString();
        multiplierText.text = ((double)creditSalary / startingValue).ToString() + "x";
        PlayerPrefsManager.SetCredits(totalCredits);
    }
}
