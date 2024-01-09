using System;
public class Magic_Meteo : Item
{
    public Magic_Meteo()
    {
        itemData = new ItemData(ItemEnum.Magic_Meteo, "起動符:メテオ", "・御札に込められた魔力を用いて呪文を唱える。\n" +
            SkillConstractParent.ReturnExplainFromSkillEnum(SkillEnum.Meteo) + "\n" +
            SkillConstractParent.ReturnSkillEffectExplainFromSkillEnum(SkillEnum.Meteo, 3));
    }

    public override void Action()
    {
        GameManager.instance.GetPlayerController().SkillByItem(SkillEnum.Meteo, 3);
    }
}
