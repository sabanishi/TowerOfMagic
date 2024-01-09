using System;
public class Magic_Thundar : Item
{
    public Magic_Thundar()
    {
        itemData = new ItemData(ItemEnum.Magic_Thundar, "起動符:サンダー", "・御札に込められた魔力を用いて呪文を唱える。\n" +
            SkillConstractParent.ReturnExplainFromSkillEnum(SkillEnum.Thundar) + "\n" +
            SkillConstractParent.ReturnSkillEffectExplainFromSkillEnum(SkillEnum.Thundar, 3));
    }

    public override void Action()
    {
        GameManager.instance.GetPlayerController().SkillByItem(SkillEnum.Thundar, 3);
    }
}
