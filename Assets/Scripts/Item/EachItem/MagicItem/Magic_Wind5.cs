using System;
public class Magic_Wind5 :Item
{
    public Magic_Wind5()
    {
        itemData = new ItemData(ItemEnum.Magic_Wind5, "起動符:ウィンド", "・御札に込められた魔力を用いて呪文を唱える。\n" +
            SkillConstractParent.ReturnExplainFromSkillEnum(SkillEnum.Wind) + "\n" +
            SkillConstractParent.ReturnSkillEffectExplainFromSkillEnum(SkillEnum.Wind, 5));
    }

    public override void Action()
    {
        GameManager.instance.GetPlayerController().SkillByItem(SkillEnum.Wind, 5);
    }
}
