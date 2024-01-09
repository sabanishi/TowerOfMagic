public class UnHappyPortion : UsingAbstractItem
{
    public UnHappyPortion()
    {
        itemData = new ItemData(ItemEnum.UnHappyPortion, "死合わせ薬", "・レベルを1下げる");
    }

    public override void Effect()
    {
        MovingObjectInfomation info = GameManager.instance.GetPlayerController().GetPlayer().GetNowMovingObjectInfo();
        if (info.Level > 1)
        {
            info.Level--;
            info.NowExp = 0;
            info.SkillPoint -= 2;
            if (info.SkillPoint < 0)
            {
                info.SkillPoint = 0;
            }
            info.SetParameter(info.Level);
            DialogManager.instance.SetText(" <color=#ffff00>" + info.Name + "</color>　は　LV:<color=#00ffff>" + info.Level + "</color>　に下がってしまった！");
        }
        else
        {
            DialogManager.instance.SetText("しかし　効果が　無かった！");
        }
       
    }
}
