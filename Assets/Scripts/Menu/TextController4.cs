using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextController4 : MonoBehaviour
{

    protected Image text;
    protected Color red = new Color(1, 0, 0);
    protected Color black = new Color(0, 0, 0);

    void Start()
    {
        text = GetComponent<Image>();
    }

    public virtual void OnPointerEnter()
    {
        text.color = red;
    }

    public virtual void OnPointerExit()
    {
        text.color = black;
    }
}