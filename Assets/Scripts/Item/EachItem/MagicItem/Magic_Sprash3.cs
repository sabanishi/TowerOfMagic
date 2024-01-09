using System;
public class Magic_Sprash3:Item
{
    public Magic_Sprash3()
    {
        itemData = new ItemData(ItemEnum.Magic_Sprash3, "起動符:スプラッシュ", "・御札に込められた魔力を用いて呪文を唱える。\n" +
            SkillConstractParent.ReturnExplainFromSkillEnum(SkillEnum.Sprash) + "\n" +
            SkillConstractParent.ReturnSkillEffectExplainFromSkillEnum(SkillEnum.Sprash, 3));
    }

    public override void Action()
    {
        GameManager.instance.GetPlayerController().SkillByItem(SkillEnum.Sprash,3);
    }
}
