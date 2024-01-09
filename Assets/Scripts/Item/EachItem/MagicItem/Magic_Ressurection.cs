using System;
public class Magic_Ressurection : Item
{
    public Magic_Ressurection()
    {
        itemData = new ItemData(ItemEnum.Magic_Ressurection, "起動符:リザレクション", "・御札に込められた魔力を用いて呪文を唱える。\n" +
            SkillConstractParent.ReturnExplainFromSkillEnum(SkillEnum.Resurrection) + "\n" +
            SkillConstractParent.ReturnSkillEffectExplainFromSkillEnum(SkillEnum.Resurrection, 2));
    }

    public override void Action()
    {
        GameManager.instance.GetPlayerController().SkillByItem(SkillEnum.Resurrection, 2);
    }
}
