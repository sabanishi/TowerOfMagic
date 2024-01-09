using System;
public class Magic_SuperNova : Item
{
    public Magic_SuperNova()
    {
        itemData = new ItemData(ItemEnum.Magic_SuperNova, "起動符:スーパーノヴァ", "・御札に込められた魔力を用いて呪文を唱える。\n" +
            SkillConstractParent.ReturnExplainFromSkillEnum(SkillEnum.SuperNova) + "\n" +
            SkillConstractParent.ReturnSkillEffectExplainFromSkillEnum(SkillEnum.SuperNova, 3));
    }

    public override void Action()
    {
        GameManager.instance.GetPlayerController().SkillByItem(SkillEnum.SuperNova, 3);
    }
}

