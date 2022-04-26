using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DisplayCoin : MonoBehaviour
{
    [SerializeField] TMP_Text CreditText;
    // Start is called before the first frame update
    void Start()
    {
        int credits = PlayerPrefsManager.GetCredits();
        CreditText.text = credits.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        int credits = PlayerPrefsManager.GetCredits();
        CreditText.text = credits.ToString();

    }
}
