using System;
public class ItemChest
{
    private ItemEnum[] items = new ItemEnum[32];
    public ItemChest()
    {
        for(int i = 0; i < items.Length; i++)
        {
            items[i] = ItemEnum.None;
        }
    }

    public bool AddItem(ItemEnum item)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if (items[i] ==ItemEnum.None)
            {
                items[i] = item;
                return true;
            }
        }
        return false;
    }

    public ItemEnum RemoveItem(int num)
    {
        ItemEnum item = items[num];
        if (num < items.Length-1)
        {
            for(int i=num;i<items.Length-1;i++)
            {
                items[i] = items[i + 1];
            }
            items[items.Length - 1] = ItemEnum.None;
        }
        return item;
    }

    public ItemEnum GetItem(int num)
    {
        return items[num];
    }

    public bool CanAddItem()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == ItemEnum.None)
            {
                return true;
            }
        }
        return false;
    }

}
