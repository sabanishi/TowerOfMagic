using System;
public class Magic_BigBurn : Item
{
    public Magic_BigBurn()
    {
        itemData = new ItemData(ItemEnum.Magic_BigBurn, "起動符:ビッグバン", "・御札に込められた魔力を用いて呪文を唱える。\n" +
            SkillConstractParent.ReturnExplainFromSkillEnum(SkillEnum.BigBurn) + "\n" +
            SkillConstractParent.ReturnSkillEffectExplainFromSkillEnum(SkillEnum.BigBurn, 3));
    }

    public override void Action()
    {
        GameManager.instance.GetPlayerController().SkillByItem(SkillEnum.BigBurn,3);
    }
}
