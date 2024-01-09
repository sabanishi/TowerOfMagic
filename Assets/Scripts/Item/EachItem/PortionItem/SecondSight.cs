using System;
using UnityEngine;

public class SecondSight:UsingAbstractItem
{
    public SecondSight()
    {
        itemData = new ItemData(ItemEnum.SecondSight, "千里眼薬", "・マップが全部見えるようになる。");
    }

    public override void Effect()
    {
        GameObject.FindWithTag("InfoManager").GetComponent<InfoManager>().ShowAllItem();
        DialogManager.instance.SetText("千里眼でマップ全体を見渡した！");
        SoundManager.instance.PlaySE(SoundManager.SE_Type.secondSightVoice);
    }
}