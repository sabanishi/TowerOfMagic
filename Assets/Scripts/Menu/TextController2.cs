using System;
using UnityEngine;
using UnityEngine.UI;
public class TextController2:TextController
{
    protected Color redAlpha = new Color(1, 0, 0,0.5f);
    protected Color blackAlpha = new Color(0.2f, 0.2f, 0.2f,0.5f);

    [HideInInspector]public bool isActive=true;
    [HideInInspector]public bool isDecide;

    public override void OnPointerEnter()
    {
        if (!isDecide)
        {
            if (isActive)
            {
                text.color = red;
            }
            else
            {
                text.color = redAlpha;
            }
        }
    }

    public override void OnPointerExit()
    {
        if (!isDecide)
        {
            if (isActive)
            {
                text.color = black;
            }
            else
            {
                text.color = blackAlpha;
            }
        }
    }
}
