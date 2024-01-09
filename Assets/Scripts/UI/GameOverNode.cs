using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverNode: MonoBehaviour
{
    [SerializeField] private Image Square;
    private Color red = new Color(1, 0, 0);
    private Color white = new Color(1f, 1f, 1f);
   
    public virtual void OnPointerEnter()
    {
        Square.color = red;
    }

    public virtual void OnPointerExit()
    {
        Square.color = white;
    }
}
