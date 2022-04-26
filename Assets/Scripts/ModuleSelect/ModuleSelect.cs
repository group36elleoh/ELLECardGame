﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using Debug = UnityEngine.Debug;


// On Module select screen, create buttons, find/create user files for modules avalible to the player

[Serializable]
public class ModuleSelect : MonoBehaviour
{
    public static Button imageCheckbox;
    public GameObject buttonPrefab;
    public static GameObject buttonsArea;
    public static bool loadOnce = false;
    public static int currentModuleID;
    public static bool endless = false;
    public static bool pictureMode = false;
    public static bool hideTerms = false;

    public static GameObject wrText;
    public static GameObject pbText;
    public static GameObject qCount;
    public static GameObject qAnswered;
    public static GameObject percentCorrect;
    public static GameObject timeSpent;
    public InputField questionTimerArea;
    public static bool nPage = false;
    public static bool pPage = false;


    public static int moduleCount;
    private int currentModuleButtons = 0;

    // References to the two mutally exclusive checkboxes 
    private Toggle hideTermsCheckbox;
    private Toggle enableImagesCheckbox;
    
    void Awake()
    {
        moduleCount = BackendHook.avalibleModulesDic.Count;

        //imageCheckbox = GameObject.Find("ImageToggle").GetComponent<Button>();
        pbText = GameObject.Find("PB Text");
        wrText = GameObject.Find("WR Text");
        qCount = GameObject.Find("Q Count text");
        qAnswered = GameObject.Find("Q Answerd Text");
        percentCorrect = GameObject.Find("Percent Correct Text");
        timeSpent = GameObject.Find("Time Spent Text");
        buttonsArea = GameObject.Find("ModuleButtonArea");

        hideTermsCheckbox = GameObject.Find("TermsToggle").GetComponent<Toggle>();
        enableImagesCheckbox = GameObject.Find("ImageToggle").GetComponent<Toggle>();

        fillButtonPage(0);
    }

    void Start()
    {
        questionTimerArea.text = "5";

        currentModuleID = -1;
        Debug.Log("CLEARING THE HIGH SCORE -----------------------------------");
        BackendHook.highScores.Clear();

        endless = false;
        hideTerms = false;
        pictureMode = false;

        foreach (KeyValuePair<int, string> m in BackendHook.avalibleModulesDic)
        {
            StartCoroutine(BackendHook.getModuleHighScore(m.Key));
        }
        ModulePageManager.currentPage = 0;
    }

    void Update()
    {
        if (questionTimerArea.text != "" && !IsDigitsOnly(questionTimerArea.text))
        {
            questionTimerArea.text = "5";
        }

        if (questionTimerArea.text != "" && float.Parse(questionTimerArea.text) > 10 && IsDigitsOnly(questionTimerArea.text))
        {
            questionTimerArea.text = "10";
        }

        if (questionTimerArea.text != "" && float.Parse(questionTimerArea.text) < 1 && IsDigitsOnly(questionTimerArea.text))
        {
            questionTimerArea.text = "1";
        }



        if (BackendHook.collectionMade == true)
        {
            SceneManager.LoadScene("Game");
        }

        if (pPage)
        {
            pPage = false;
            fillButtonPage(ModulePageManager.currentPage);
        }
        if (nPage)
        {
            nPage = false;
            fillButtonPage(ModulePageManager.currentPage);
        }

    }

    public void fillButtonPage(int page)
    {
        foreach (Transform child in buttonsArea.transform)
        {
            Destroy(child.gameObject);
        }

        int currentButtons = 0;

        foreach (KeyValuePair<int, string> entry in BackendHook.avalibleModulesDic)
        {
            if (currentButtons >= 8 * page && currentButtons <= 8 * page + 8)
            {
                createButton(entry.Key, entry.Value);
                // createButton(currentButtons, "Test " + currentButtons);
                // Creates user json record file for any new modules

                if (!System.IO.Directory.Exists(Application.persistentDataPath + "/UserRecords"))
                {
                    System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/UserRecords");
                }

                Debug.Log(Application.persistentDataPath  + " / UserRecords / Module");

                if (!File.Exists(Application.persistentDataPath + "/UserRecords/Module" + entry.Key + ".json"))
                {
                    Debug.Log("Making new file");
                    ModuleInfo newMod = new ModuleInfo(entry.Key, entry.Value, 0, 0, 0, 0, 0, 0);
                    CollectionManager.moduleCollection.Add(entry.Key, newMod);

                    Debug.Log("Middle of file ceration area");
                    
                    string moduleJson = newMod.createJson();
                    newMod.saveJson();


                    Debug.Log(moduleJson);
                    Debug.Log("End of file creation area");
                }
                else
                {
                    // If the file already exists, create a moduleInfo object with the json info. This moduleInfo object is used to set changes to the json.
                    string path = Application.persistentDataPath + "/UserRecords/Module" + entry.Key + ".json";
                    StreamReader reader = new StreamReader(path);

                    if (!CollectionManager.moduleCollection.ContainsKey(entry.Key))
                        CollectionManager.moduleCollection.Add(entry.Key, JsonUtility.FromJson<ModuleInfo>(reader.ReadToEnd()));

                    reader.Close();
                }
            }
            currentButtons++;
        }
    }

    public void createButton(int moduleID, string moduleName)
    {
        GameObject buttongo = Instantiate(buttonPrefab, buttonsArea.transform);

        buttongo.GetComponent<ButtonScript>().setButtonModule(moduleID);

        var button = buttongo.GetComponent<UnityEngine.UI.Button>();

        button.GetComponentInChildren<Text>().text = moduleName;
        button.onClick.AddListener(() => buttongo.GetComponent<ButtonScript>().pickModule(moduleID));
    }



    public static void setSelectedModule(int id)
    {
        //imageCheckbox.interactable = BackendHook.modulesWithImages.Contains(id);
        pbText.GetComponentInChildren<Text>().text = CollectionManager.moduleCollection[id].getPB().ToString();
        wrText.GetComponentInChildren<Text>().text = BackendHook.highScores[id].ToString();
        qCount.GetComponentInChildren<Text>().text = BackendHook.moduleQCount[id].ToString();
        qAnswered.GetComponentInChildren<Text>().text = CollectionManager.moduleCollection[id].getTotalQuestionsAnswered().ToString();
        percentCorrect.GetComponentInChildren<Text>().text = (CollectionManager.moduleCollection[id].getPercentCorrect() * 100).ToString() + "%";
        timeSpent.GetComponentInChildren<Text>().text = (Math.Round(CollectionManager.moduleCollection[id].getTime() / 60, 0).ToString() + " minutes");

        currentModuleID = id;
        GameObject.Find("PlayButton").GetComponentInChildren<Text>().text = "Play Module: " + CollectionManager.moduleCollection[id].getName();
    }

    public void startGame()
    {
        if (currentModuleID != -1)
        {
            GameManager.timeBwtweenQuestions = float.Parse(questionTimerArea.text);
            StartCoroutine(BackendHook.startSession(currentModuleID));
            Debug.Log("STARTING GAME");
            BackendHook.modulesFound = false;
            GameManager.endlessMode = endless;
            GameManager.pictureMode = pictureMode;
            GameManager.hideTerms = hideTerms;
            GameManager.currentModule = CollectionManager.moduleCollection[currentModuleID];
            StartCoroutine(BackendHook.getModule(currentModuleID));
        }
    }

    public void toggleEndless()
    {
        endless = !endless;
    }

    public void toggleTerms()
    {
        hideTerms = !hideTerms;
        enableImagesCheckbox.interactable = !hideTerms;
    }

    public void togglePictures()
    {
        pictureMode = !pictureMode;
        hideTermsCheckbox.interactable = !pictureMode;
    }
    
    bool IsDigitsOnly(string str)
    {
        foreach (char c in str)
        {
            if (c < '0' || c > '9')
                return false;
        }

        return true;
    }

    // Resets the player's credits to 0
    public void ResetCredits()
    {
        PlayerPrefsManager.SetCredits(0);
    }

}
 