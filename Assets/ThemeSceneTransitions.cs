using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThemeSceneTransitions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBackgroundScene()
    {
        SceneManager.LoadScene("BackgroundShop");
    }
    public void ChangeCardScene()
    {
        SceneManager.LoadScene("CardShop");
    }
    public void ChangeThemeScene()
    {
        SceneManager.LoadScene("ThemeShop");
    }
    public void ChangeModuleScene()
    {
        SceneManager.LoadScene("ModuleSelect");
    }
}
