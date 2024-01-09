public class Ether : UsingAbstractItem
{
    public Ether()
    {
        itemData = new ItemData(ItemEnum.Ether, "エーテル", "・MPを50回復する。");
    }

    public override void Effect()
    {
        SoundManager.instance.PlaySE(SoundManager.SE_Type.ettherVoice);
        GameManager.instance.GetPlayerController().GetPlayer().HealMp(50);
    }
}