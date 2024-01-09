using System;
public class Magic_Inferno : Item
{
    public Magic_Inferno()
    {
        itemData = new ItemData(ItemEnum.Magic_Inferno, "起動符:インフェルノ", "・御札に込められた魔力を用いて呪文を唱える。\n" +
            SkillConstractParent.ReturnExplainFromSkillEnum(SkillEnum.Comet) + "\n" +
            SkillConstractParent.ReturnSkillEffectExplainFromSkillEnum(SkillEnum.Comet, 3));
    }

    public override void Action()
    {
        GameManager.instance.GetPlayerController().SkillByItem(SkillEnum.Comet, 3);
    }
}
