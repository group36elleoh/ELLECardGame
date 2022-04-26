using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchPopup : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    // [SerializeField] Text englishDescription;
    // [SerializeField] Text translatedDescription;
    [SerializeField] Text englishWord;
    [SerializeField] Text translatedWord;
    [SerializeField] Text creditsEarned;

    private const float POPUP_TIME = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public void ActivatePopup(string questionType, string english, string translated, int credits)
    public void ActivatePopup(string english, string translated, int credits)
    {
        // string qType = formatQuestionType(questionType);

        // englishDescription.text = "English " + qType + ":";
        // translatedDescription.text = "Translated " + qType + ":";
        
        englishWord.text = english;
        translatedWord.text = translated;

        creditsEarned.text = "+" + credits.ToString();

        canvas.SetActive(true);

        StartCoroutine(StartToDeactivate());

        Time.timeScale = 0;
    }

    public IEnumerator StartToDeactivate()
    {
        yield return new WaitForSecondsRealtime(POPUP_TIME);
        DeactivatePopup();
    }

    public void DeactivatePopup()
    {
        StopAllCoroutines();
        canvas.SetActive(false);
        Time.timeScale = 1;
    }

    /*
    private string formatQuestionType(string questionType)
    {
        return char.ToUpper(questionType[0]) + questionType.Substring(1).ToLower();
    }
    */
}
