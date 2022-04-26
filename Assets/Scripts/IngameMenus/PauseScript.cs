using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public static float pauseTimer = 0;

    void Update()
    {
        if (MenuManager.pauseToggle)
        {
            pauseTimer += Time.deltaTime;
        }
        
        // Debug.Log("Pause Timer: " + pauseTimer);
    }

    public void unpause()
    {
        Debug.Log("YAY");
        MenuManager.pauseToggle = false;
    }

    public void returnMenu()
    {
        Debug.Log("credits saved = " + PlayerPrefsManager.GetCredits());
        Debug.Log(FindObjectOfType<CreditsManager>().totalCredits);
        PlayerPrefsManager.SetCredits(FindObjectOfType<CreditsManager>().totalCredits);
        Debug.Log("credits saved = " + PlayerPrefsManager.GetCredits());

        StartCoroutine(BackendHook.endSession(QnAManager.points));
        GameManager.resetStats();
        SceneManager.LoadScene("ModuleSelect");
    }

    public void exitGame()
    {
        StartCoroutine(BackendHook.endSession(QnAManager.points));
        Application.Quit();
    }

    public void GoToThemeShop()
    {
        StartCoroutine(BackendHook.endSession(QnAManager.points));
        GameManager.resetStats();
        SceneManager.LoadScene("ThemeShop");
    }
}
