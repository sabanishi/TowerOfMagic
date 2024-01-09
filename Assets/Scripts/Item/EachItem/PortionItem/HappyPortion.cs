public class HappyPortion : UsingAbstractItem
{
    public HappyPortion()
    {
        itemData = new ItemData(ItemEnum.HappyPortion, "幸せ薬", "・レベルを1上げる");
    }

    public override void Effect()
    {
        MovingObjectInfomation info = GameManager.instance.GetPlayerController().GetPlayer().GetNowMovingObjectInfo();
        DialogManager.instance.AddExpFormat(info.Name, info.NeedExpData[info.Level] - info.NowExp);
        info.AddExp(info.NeedExpData[info.Level] - info.NowExp);
        int r = Utils.GetRandomInt(1, 3);
        if (r == 1)
        {
            SoundManager.instance.PlaySE(SoundManager.SE_Type.levelUp1);
        }
        else if (r == 2)
        {
            SoundManager.instance.PlaySE(SoundManager.SE_Type.levelUp2);
        }
        else
        {
            SoundManager.instance.PlaySE(SoundManager.SE_Type.levelUp3);
        }
        DialogManager.instance.LevelUpFormat(info.Name, info.Level);
    }
}
