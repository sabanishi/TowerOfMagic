using System;
public class Magic_AttackUp : Item
{
    public Magic_AttackUp()
    {
        itemData = new ItemData(ItemEnum.Magic_AttackUp, "起動符:アタックアップ", "・御札に込められた魔力を用いて呪文を唱える。\n" +
            SkillConstractParent.ReturnExplainFromSkillEnum(SkillEnum.AttackUp) + "\n" +
            SkillConstractParent.ReturnSkillEffectExplainFromSkillEnum(SkillEnum.AttackUp, 5));
    }

    public override void Action()
    {
        GameManager.instance.GetPlayerController().SkillByItem(SkillEnum.AttackUp,5);
    }
}
