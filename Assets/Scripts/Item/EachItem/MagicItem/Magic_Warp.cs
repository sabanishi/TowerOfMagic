using System;
public class Magic_Warp : Item
{
    public Magic_Warp()
    {
        itemData = new ItemData(ItemEnum.Magic_Warp, "起動符:ワープ", "・御札に込められた魔力を用いて呪文を唱える。\n" +
            SkillConstractParent.ReturnExplainFromSkillEnum(SkillEnum.Warp) + "\n" +
            SkillConstractParent.ReturnSkillEffectExplainFromSkillEnum(SkillEnum.Warp, 1));
    }

    public override void Action()
    {
        GameManager.instance.GetPlayerController().SkillByItem(SkillEnum.Warp,1);
    }
}
