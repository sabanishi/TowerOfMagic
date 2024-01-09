using System;
public class Magic_Stome : Item
{
    public Magic_Stome()
    {
        itemData = new ItemData(ItemEnum.Magic_Stome, "起動符:ストーム", "・御札に込められた魔力を用いて呪文を唱える。\n" +
            SkillConstractParent.ReturnExplainFromSkillEnum(SkillEnum.Stome) + "\n" +
            SkillConstractParent.ReturnSkillEffectExplainFromSkillEnum(SkillEnum.Stome, 3));
    }

    public override void Action()
    {
        GameManager.instance.GetPlayerController().SkillByItem(SkillEnum.Stome, 3);
    }
}

