using System;
public class Magic_HealVeil : Item
{
    public Magic_HealVeil()
    {
        itemData = new ItemData(ItemEnum.Magic_HealVeil, "起動符:ヒールベール", "・御札に込められた魔力を用いて呪文を唱える。\n" +
            SkillConstractParent.ReturnExplainFromSkillEnum(SkillEnum.HealVeil) + "\n" +
            SkillConstractParent.ReturnSkillEffectExplainFromSkillEnum(SkillEnum.HealVeil, 3));
    }

    public override void Action()
    {
        GameManager.instance.GetPlayerController().SkillByItem(SkillEnum.HealVeil, 3);
    }
}
