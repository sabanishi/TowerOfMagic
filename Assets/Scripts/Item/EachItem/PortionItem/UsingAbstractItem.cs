using System;
using System.Collections;
using UnityEngine;

public  abstract class UsingAbstractItem:Item
{
    private GameObject gameObject;
    public override void Action()
    {
        DialogManager.instance.UseItemFormat(itemData.Name);
        Effect();
        GameManager.instance.GetPlayerController().GetPlayer().SetIsMove(true);
        CoroutineHandler.StartStaticCoroutine(Act());

        gameObject = UnityEngine.Object.Instantiate(AttackStock.instance.ItemEffect,
            GameManager.instance.GetPlayerController().GetPlayer().transform.position,Quaternion.identity);

    }

    private IEnumerator Act()
    {
        yield return new WaitForSeconds(0.67f);
        GameManager.instance.GetPlayerController().GetPlayer().Finish();
        UnityEngine.Object.Destroy(gameObject);
    }

    public abstract void Effect();
}
