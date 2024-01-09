public class Portion: UsingAbstractItem
{
    public Portion()
    {
        itemData = new ItemData(ItemEnum.Portion, "ポーション", "・HPを50回復する。");
    }

    public override void Effect()
    {
        SoundManager.instance.PlaySE(SoundManager.SE_Type.portionVoie);
        GameManager.instance.GetPlayerController().GetPlayer().HealHp(50);
    }
}

