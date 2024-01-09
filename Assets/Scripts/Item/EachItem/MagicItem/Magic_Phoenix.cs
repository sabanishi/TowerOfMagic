using System;
public class Magic_Phoenix : Item
{
    public Magic_Phoenix()
    {
        itemData = new ItemData(ItemEnum.Magic_Phoenix, "起動符:フェニックス", "・御札に込められた魔力を用いて呪文を唱える。\n" +
            SkillConstractParent.ReturnExplainFromSkillEnum(SkillEnum.Phoenix) + "\n" +
            SkillConstractParent.ReturnSkillEffectExplainFromSkillEnum(SkillEnum.Phoenix, 2));
    }

    public override void Action()
    {
        GameManager.instance.GetPlayerController().SkillByItem(SkillEnum.Phoenix, 2);
    }
}
