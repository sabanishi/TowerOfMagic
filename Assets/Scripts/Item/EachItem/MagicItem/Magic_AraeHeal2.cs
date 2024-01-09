using System;
public class Magic_AreaHeal2: Item
{
    public Magic_AreaHeal2()
    {
        itemData = new ItemData(ItemEnum.Magic_AreaHeal2, "起動符:エリアヒール", "・御札に込められた魔力を用いて呪文を唱える。\n"+
            SkillConstractParent.ReturnExplainFromSkillEnum(SkillEnum.AreaHeal) +"\n"+
            SkillConstractParent.ReturnSkillEffectExplainFromSkillEnum(SkillEnum.AreaHeal,2));
    }

    public override void Action()
    {
        GameManager.instance.GetPlayerController().SkillByItem(SkillEnum.AreaHeal,2);
    }
}
