using System;
public class Magic_Stone5 :Item
{
    public Magic_Stone5()
    {
        itemData = new ItemData(ItemEnum.Magic_Stone5, "起動符:ストーン", "・御札に込められた魔力を用いて呪文を唱える。\n" +
            SkillConstractParent.ReturnExplainFromSkillEnum(SkillEnum.Stone) + "\n" +
            SkillConstractParent.ReturnSkillEffectExplainFromSkillEnum(SkillEnum.Stone, 5));
    }

    public override void Action()
    {
        GameManager.instance.GetPlayerController().SkillByItem(SkillEnum.Stone,5);
    }
}
