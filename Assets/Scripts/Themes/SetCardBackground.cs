using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetCardBackground : MonoBehaviour
{
    public RawImage m_RawImage;
    private Texture set_Texture;

    void Start()
    {
        //Fetch the RawImage component from the GameObject
        set_Texture = getTexture();
        m_RawImage = GetComponent<RawImage>();
        m_RawImage.texture = set_Texture;
    }
    Texture getTexture()
    {
        string selectedTexture = PlayerPrefsManager.GetSelectedCard();
        if (selectedTexture == null || selectedTexture == "")
            selectedTexture = "DefaultCard";
        set_Texture = Resources.Load<Texture2D>("PhysCardItems/BackgroundImages/" + selectedTexture);
        return (set_Texture);
    }
    
}