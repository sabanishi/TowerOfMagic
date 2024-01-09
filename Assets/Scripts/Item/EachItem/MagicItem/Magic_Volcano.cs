using System;
public class Magic_Volcano : Item
{
    public Magic_Volcano()
    {
        itemData = new ItemData(ItemEnum.Magic_Volcano, "起動符:ボルケイノ", "・御札に込められた魔力を用いて呪文を唱える。\n" +
            SkillConstractParent.ReturnExplainFromSkillEnum(SkillEnum.Volcano) + "\n" +
            SkillConstractParent.ReturnSkillEffectExplainFromSkillEnum(SkillEnum.Volcano, 3));
    }

    public override void Action()
    {
        GameManager.instance.GetPlayerController().SkillByItem(SkillEnum.Volcano, 3);
    }
}

