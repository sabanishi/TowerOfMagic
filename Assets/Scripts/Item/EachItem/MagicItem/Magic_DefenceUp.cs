using System;
public class Magic_DefenceUp: Item
{
    public Magic_DefenceUp()
    {
        itemData = new ItemData(ItemEnum.Magic_DefenceUp, "起動符:ディフェンスアップ", "・御札に込められた魔力を用いて呪文を唱える。\n" +
            SkillConstractParent.ReturnExplainFromSkillEnum(SkillEnum.DefenceUp) + "\n" +
            SkillConstractParent.ReturnSkillEffectExplainFromSkillEnum(SkillEnum.DefenceUp,5));
    }

    public override void Action()
    {
        GameManager.instance.GetPlayerController().SkillByItem(SkillEnum.DefenceUp,5);
    }
}
