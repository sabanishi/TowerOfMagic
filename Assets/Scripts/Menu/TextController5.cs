using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController5 : MonoBehaviour
{
    public Image image;
    protected Color red = new Color(1, 0, 0);
    protected Color white= new Color(1, 1, 1);

    protected Color redAlpha = new Color(1, 0, 0, 0.5f);
    protected Color blackAlpha = new Color(0.5f, 0.5f, 0.5f, 0.5f);

    [HideInInspector] public bool isActive = true;

    public virtual void OnPointerEnter()
    {
        if (isActive)
        {
            image.color = red;

        }
        else
        {
            image.color = redAlpha;
        }
    }

    public virtual void OnPointerExit()
    {
        if (isActive)
        {
            image.color = white;
        }
        else
        {
            image.color = blackAlpha;
        }
    }
}
