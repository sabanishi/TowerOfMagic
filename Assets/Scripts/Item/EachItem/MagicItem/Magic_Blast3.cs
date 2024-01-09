using System;
public class Magic_Blast3:Item
{
    public Magic_Blast3()
    {
        itemData = new ItemData(ItemEnum.Magic_Blast3, "起動符:ブラスト", "・御札に込められた魔力を用いて呪文を唱える。\n" +
            SkillConstractParent.ReturnExplainFromSkillEnum(SkillEnum.Blast) + "\n" +
            SkillConstractParent.ReturnSkillEffectExplainFromSkillEnum(SkillEnum.Blast, 3));
    }
    public override void Action()
    {
        GameManager.instance.GetPlayerController().SkillByItem(SkillEnum.Blast,3);
    }
}
