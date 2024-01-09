using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class  TextController: MonoBehaviour
{
    protected Text text;
    protected Color red = new Color(1, 0, 0);
    protected Color black = new Color(0.2f, 0.2f, 0.2f);

    void Start()
    {
        text = GetComponent<Text>();
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
