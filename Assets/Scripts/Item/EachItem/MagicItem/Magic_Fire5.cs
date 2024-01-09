using System;
public class Magic_Fire5: Item
{
    
    public Magic_Fire5()
    {
        itemData = new ItemData(ItemEnum.Magic_Fire5, "起動符:ファイヤ", "・御札に込められた魔力を用いて呪文を唱える\n" +
            SkillConstractParent.ReturnExplainFromSkillEnum(SkillEnum.Fire) + "\n" +
            SkillConstractParent.ReturnSkillEffectExplainFromSkillEnum(SkillEnum.Fire, 5));
    }
    public override void Action()
    {
        GameManager.instance.GetPlayerController().SkillByItem(SkillEnum.Fire, 5);
    }
}
