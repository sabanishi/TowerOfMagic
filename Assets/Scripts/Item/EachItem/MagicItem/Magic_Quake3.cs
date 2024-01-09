using System;
public class Magic_Quake3:Item
{
    public Magic_Quake3()
    {
        itemData = new ItemData(ItemEnum.Magic_Quake3, "起動符:クエイク", "・御札に込められた魔力を用いて呪文を唱える。\n" +
            SkillConstractParent.ReturnExplainFromSkillEnum(SkillEnum.Quake) + "\n" +
            SkillConstractParent.ReturnSkillEffectExplainFromSkillEnum(SkillEnum.Quake, 3));
    }

    public override void Action()
    {
        GameManager.instance.GetPlayerController().SkillByItem(SkillEnum.Quake,3);
    }
}
