using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CandyCabinets.Components.Colour;

public class ThemeManager : MonoBehaviour
{

    private string selectedBackgroundImage;

    // Start is called before the first frame update
    void Start()
    {
        selectedBackgroundImage = PlayerPrefsManager.GetSelectedBackgroundImage();
        
        if (selectedBackgroundImage == null || !(ColourManager.Instance.IfPalette(selectedBackgroundImage)))
            selectedBackgroundImage = "Default";

        ColourManager.Instance.SelectPalette(selectedBackgroundImage);

    }

    // Update is called once per frame
    /*
     * This was for testing purposes; is unnecessary
    void Update()
    {
        selectedTheme = PlayerPrefsManager.GetSelectedTheme();
        currentTheme = ColourManager.Instance.SelectedPaletteName();
        if (!(selectedTheme.Equals(currentTheme)))
            ColourManager.Instance.SelectPalette(selectedTheme);
    }
    */
}
