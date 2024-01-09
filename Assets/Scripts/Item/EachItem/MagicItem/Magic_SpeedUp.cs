using System;
public class Magic_SpeedUp: Item
{
    public Magic_SpeedUp()
    {
        itemData = new ItemData(ItemEnum.Magic_SpeedUp, "起動符:スピードアップ", "・御札に込められた魔力を用いて呪文を唱える。\n" +
            SkillConstractParent.ReturnExplainFromSkillEnum(SkillEnum.SpeedUp) + "\n" +
            SkillConstractParent.ReturnSkillEffectExplainFromSkillEnum(SkillEnum.SpeedUp, 1));
    }

    public override void Action()
    {
        GameManager.instance.GetPlayerController().SkillByItem(SkillEnum.SpeedUp,1);
    }
}
