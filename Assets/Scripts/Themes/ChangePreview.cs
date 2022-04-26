using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePreview : MonoBehaviour
{
    public SpriteRenderer spriteR;
    // Start is called before the first frame update
    void Start()
    {
        string currentlySelected = BackgroundShopManager.getCurrentlySelected();
        // should be null
        if (currentlySelected != null)
        {
            spriteR = gameObject.GetComponent<SpriteRenderer>();
            spriteR.sprite = Resources.Load<Sprite>("PhysCardItems/BackgroundImages/" + currentlySelected);
        }
        else
        {
            spriteR = gameObject.GetComponent<SpriteRenderer>();
            spriteR.sprite = null;
        }

    }

    // keeps changing preview to be currentlySelected
    void Update()
    {
        string currentlySelected = BackgroundShopManager.getCurrentlySelected();
        if (currentlySelected != null)
        {
            spriteR = gameObject.GetComponent<SpriteRenderer>();
            spriteR.sprite = Resources.Load<Sprite>("PhysCardItems/BackgroundImages/" + currentlySelected);
        }
        else
        {
            spriteR = gameObject.GetComponent<SpriteRenderer>();
            spriteR.sprite = null;
        }

    }

}
