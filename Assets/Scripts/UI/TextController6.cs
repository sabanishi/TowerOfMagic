using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextController6 : MonoBehaviour
{
    protected Text text;
    protected Color red = new Color(1, 0, 0);
    protected Color white = new Color(1f, 1f, 1f);

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
        text.color = white;
    }
}
