using System;
public class Magic_Heal2 : Item
{
    public Magic_Heal2()
    {
        itemData = new ItemData(ItemEnum.Magic_Heal2, "起動符:ヒール", "・御札に込められた魔力を用いて呪文を唱える。\n" +
            SkillConstractParent.ReturnExplainFromSkillEnum(SkillEnum.Heal) + "\n" +
            SkillConstractParent.ReturnSkillEffectExplainFromSkillEnum(SkillEnum.Heal, 2));
    }

    public override void Action()
    {
        GameManager.instance.GetPlayerController().SkillByItem(SkillEnum.Heal,2);
    }
}
