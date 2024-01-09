using UnityEngine;
using System.Collections;

public abstract class Item
{
    public  ItemData itemData { get; protected set; }

    public abstract void Action();
}
