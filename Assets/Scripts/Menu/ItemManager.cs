using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemManager : ManagerInterface
{
    [SerializeField] private Text[] items;
    [SerializeField] private Text pageText;
    [SerializeField] private Text InfoText;
    [SerializeField] private GameObject DecidePanel;
    [SerializeField] private Text DecideItemName;
    [SerializeField] private GameObject Down;
    [SerializeField] private GameObject Up;

    private bool isDecideItem;
    private int page;
    private const string NullName = "なし";

    private int decideItemNum;

    public void Setting()
    {
        isDecideItem = false;
        DecidePanel.SetActive(false);
        page = 1;
        SetItemList();
        decideItemNum = -1;
        InfoText.text = "";
    }

    private void SetItemList()
    {
        ItemChest itemChest = GameManager.instance.GetPlayerController().GetPlayer().GetItemChest();
        pageText.text = page + "";
        for(int i = 0; i < items.Length; i++)
        {
            ItemEnum itemEnum = itemChest.GetItem(i + (page - 1) * items.Length);
            if (itemEnum != ItemEnum.None)
            {
                items[i].text = ItemConstractParent.ReturnNameFromItemEnum(itemEnum);
            }
            else
            {
                items[i].text =NullName;
            }
        }

        Down.SetActive(true);
        Up.SetActive(true);
        if (page == 1)
        {
            Down.SetActive(false);
        }
        if (page == 4)
        {
            Up.SetActive(false);
        }
    }

    public override void Act()
    {
        if (isDecideItem)
        {
            DecidePanel.SetActive(true);
        }
        else
        {
            DecidePanel.SetActive(false);
        }
    }

    public void UseItem()
    {
        parent.TurnEnumChange(PlayerTurnEnum.Robby);
        parent.Back();
        ItemChest itemChest = GameManager.instance.GetPlayerController().GetPlayer().GetItemChest();
        ItemConstractParent.ReturnItemFromItemEnum(itemChest.RemoveItem(decideItemNum)).Action();
    }

    public void DiscardItem()
    {
        ItemChest itemChest = GameManager.instance.GetPlayerController().GetPlayer().GetItemChest();
        itemChest.RemoveItem(decideItemNum);
        SetItemList();
        isDecideItem = false;
        DeleteExplain();
        SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
    }

    public void Cancel()
    {
        isDecideItem = false;
        DeleteExplain();
        SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
    }

    public void ClickItem(int num)
    {
        if (!isDecideItem)
        {
            decideItemNum = num + (page - 1) * items.Length-1;
            ItemChest itemChest = GameManager.instance.GetPlayerController().GetPlayer().GetItemChest();
            ItemEnum itemEnum = itemChest.GetItem(decideItemNum);
            if (itemEnum != ItemEnum.None)
            {
                DecideItemName.text = ItemConstractParent.ReturnNameFromItemEnum(itemEnum) + "　を";
                isDecideItem = true;
                SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
            }
            else
            {
                SoundManager.instance.PlaySE(SoundManager.SE_Type.cannotClick);
            }
        }
    }

    public void UpClick()
    {
        if (!isDecideItem)
        {
            if (page < 4)
            {
                page++;
                SetItemList();
                SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
            }
        }
    }

    public void DownClick()
    {
        if (!isDecideItem)
        {
            if (page > 1)
            {
                page--;
                SetItemList();
                SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
            }
        }
    }

    public void WriteExplain(int num)
    {
        if (!isDecideItem)
        {
            ItemChest itemChest = GameManager.instance.GetPlayerController().GetPlayer().GetItemChest();
            int writeNum = num + (page - 1) * items.Length-1;
            ItemEnum itemEnum = itemChest.GetItem(writeNum);
            if (itemEnum != ItemEnum.None)
            {
                InfoText.text = ItemConstractParent.ReturnNameFromItemEnum(itemEnum)+"\n"+
                    ItemConstractParent.ReturnExplainTextFormItemEnum(itemEnum);
            }
        }
    }

    public void DeleteExplain()
    {
        if (!isDecideItem)
        {
            InfoText.text = "";
        }
    }

    public void Back()
    {
        parent.TurnEnumChange(PlayerTurnEnum.Robby);
        SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
    }

}
