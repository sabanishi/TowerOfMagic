using System;
public class Magic_Explosion : Item
{
    public Magic_Explosion()
    {
        itemData = new ItemData(ItemEnum.Magic_Explosion, "起動符:エクスプロージョン", "・御札に込められた魔力を用いて呪文を唱える。\n" +
            SkillConstractParent.ReturnExplainFromSkillEnum(SkillEnum.Explosion) + "\n" +
            SkillConstractParent.ReturnSkillEffectExplainFromSkillEnum(SkillEnum.Explosion, 3));
    }

    public override void Action()
    {
        GameManager.instance.GetPlayerController().SkillByItem(SkillEnum.Explosion, 3);
    }
}
