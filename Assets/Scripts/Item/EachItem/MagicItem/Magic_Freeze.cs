using System;
public class Magic_Freeze : Item
{
    public Magic_Freeze()
    {
        itemData = new ItemData(ItemEnum.Magic_Freeze, "起動符:フリーズ", "・御札に込められた魔力を用いて呪文を唱える。\n" +
            SkillConstractParent.ReturnExplainFromSkillEnum(SkillEnum.Freeze) + "\n" +
            SkillConstractParent.ReturnSkillEffectExplainFromSkillEnum(SkillEnum.Freeze, 1));
    }

    public override void Action()
    {
        GameManager.instance.GetPlayerController().SkillByItem(SkillEnum.Freeze, 1);
    }
}
