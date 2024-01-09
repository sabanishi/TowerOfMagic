using System;
using System.Collections.Generic;

public static class CreateItemData
{
    private static List<List<ItemEnum>> ItemLists = new List<List<ItemEnum>>()
    {
       new List<ItemEnum>(){ItemEnum.Magic_Fire5,ItemEnum.Magic_Water5,ItemEnum.Magic_Wind5,ItemEnum.Magic_Stone5},     //0
       new List<ItemEnum>(){ItemEnum.Magic_Burn3,ItemEnum.Magic_Sprash3,ItemEnum.Magic_Blast3,ItemEnum.Magic_Quake3},   //1
       new List<ItemEnum>(){ItemEnum.Magic_Explosion,ItemEnum.Magic_Ressurection,ItemEnum.Magic_Inferno,ItemEnum.Magic_Volcano,ItemEnum.Magic_Stome},   //2
       new List<ItemEnum>(){ItemEnum.Magic_BigBurn,ItemEnum.Magic_SuperNova,ItemEnum.Magic_Meteo,ItemEnum.Magic_Thundar,ItemEnum.Magic_HealVeil},   //3


       new List<ItemEnum>(){ItemEnum.Magic_AttackUp,ItemEnum.Magic_DefenceUp,ItemEnum.Magic_Heal2},     //4
       new List<ItemEnum>(){ItemEnum.Magic_Phoenix,ItemEnum.Magic_Freeze,ItemEnum.Magic_SpeedUp,ItemEnum.Magic_AreaHeal2},      //5

       new List<ItemEnum>(){ItemEnum.Portion,ItemEnum.Ether,ItemEnum.SecondSight},      //6
       new List<ItemEnum>(){ItemEnum.HappyPortion, ItemEnum.UnHappyPortion,ItemEnum.Magic_Warp}     //7
    };

    public static ItemEnum ReturnRandomItem(int level)
    {
        if (level <= 5)
        {
            return ReturnItemEnumForLists(new List<List<ItemEnum>>() { ItemLists[0],ItemLists[4],ItemLists[6] });
        }else if (level <= 15)
        {
            return ReturnItemEnumForLists(new List<List<ItemEnum>>() { ItemLists[1], ItemLists[4],ItemLists[5],ItemLists[6] });
        }
        else if(level <= 20)
        {
            return ReturnItemEnumForLists(new List<List<ItemEnum>>() { ItemLists[2], ItemLists[4],ItemLists[5],ItemLists[6] });
        }
        else
        {
            return ReturnItemEnumForLists(new List<List<ItemEnum>>() { ItemLists[3], ItemLists[5], ItemLists[6] });
        }
    }

    public static ItemEnum ReturnRandomItemForMonsterHouse(int level)
    {
        if (Utils.RandomJadge(0.5f))
        {
            return Utils.GetRandom(ItemLists[7]);
        }
        else
        {
            return ReturnRandomItem(level);
        }
    }

    public static ItemEnum ReturnItemEnumForLists(List<List<ItemEnum>> lists)
    {
        List<ItemEnum> randomList = Utils.GetRandom(lists);
        return Utils.GetRandom(randomList);
    }
}
