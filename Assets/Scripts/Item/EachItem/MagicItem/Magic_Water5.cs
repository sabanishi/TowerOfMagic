using System;
public class Magic_Water5 : Item
{

    public Magic_Water5()
    {
        itemData = new ItemData(ItemEnum.Magic_Water5, "起動符:ウォーター", "・御札に込められた魔力を用いて呪文を唱える。\n" +
            SkillConstractParent.ReturnExplainFromSkillEnum(SkillEnum.Water) + "\n" +
            SkillConstractParent.ReturnSkillEffectExplainFromSkillEnum(SkillEnum.Water, 5));
    }

    public override void Action()
    {
        GameManager.instance.GetPlayerController().SkillByItem(SkillEnum.Water, 5);
    }
}
