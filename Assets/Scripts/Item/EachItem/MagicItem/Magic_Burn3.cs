using System;
public class Magic_Burn3:Item
{
    public Magic_Burn3()
    {
        itemData = new ItemData(ItemEnum.Magic_Burn3, "起動符:バーン", "・御札に込められた魔力を用いて呪文を唱える。\n" +
            SkillConstractParent.ReturnExplainFromSkillEnum(SkillEnum.Burn) + "\n" +
            SkillConstractParent.ReturnSkillEffectExplainFromSkillEnum(SkillEnum.Burn, 3));
    }

    public override void Action()
    {
        GameManager.instance.GetPlayerController().SkillByItem(SkillEnum.Burn,3);
    }
}
