using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetImageBackground : MonoBehaviour
{
    Image m_Image;
    private Sprite selectedSprite;
    public Sprite m_Sprite;
    // Start is called before the first frame update
    void Start()
    {
        m_Image = GetComponent<Image>();
        string selectedImage = PlayerPrefsManager.GetSelectedBackgroundImage();
        if (selectedImage == null || selectedImage == "")
            selectedImage = "Default";
        selectedSprite = Resources.Load<Sprite>("PhysCardItems/BackgroundImages/" + selectedImage);
        m_Image.sprite = selectedSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
