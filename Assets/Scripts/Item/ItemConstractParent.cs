using System;
using System.Collections.Generic;
using UnityEngine;

public static class ItemConstractParent
{
    private static Magic_Fire5 fire5 = new Magic_Fire5();
    private static Magic_Water5 water5 = new Magic_Water5();
    private static Magic_Wind5 wind5 = new Magic_Wind5();
    private static Magic_Stone5 stone5 = new Magic_Stone5();
    private static Magic_Burn3 burn3 = new Magic_Burn3();
    private static Magic_Sprash3 sprash3 = new Magic_Sprash3();
    private static Magic_Blast3 blast3 = new Magic_Blast3();
    private static Magic_Quake3 quake3 = new Magic_Quake3();
    private static Magic_AttackUp attackUp = new Magic_AttackUp();
    private static Magic_DefenceUp defenceUp = new Magic_DefenceUp();
    private static Magic_AreaHeal2 areaHeal2 = new Magic_AreaHeal2();
    private static Magic_Heal2 heal2 = new Magic_Heal2();
    private static Magic_Phoenix phoenix = new Magic_Phoenix();
    private static Magic_Freeze freee = new Magic_Freeze();
    private static Magic_SpeedUp speedUp = new Magic_SpeedUp();
    private static Magic_Explosion explosion = new Magic_Explosion();
    private static Magic_BigBurn bigBurn = new Magic_BigBurn();
    private static Magic_Inferno inferno = new Magic_Inferno();
    private static Magic_SuperNova superNova = new Magic_SuperNova();
    private static Magic_Volcano volcano = new Magic_Volcano();
    private static Magic_Meteo meteo = new Magic_Meteo();
    private static Magic_Stome stome = new Magic_Stome();
    private static Magic_Thundar thundar = new Magic_Thundar();
    private static Magic_Ressurection ressurection = new Magic_Ressurection();
    private static Magic_HealVeil healVeil = new Magic_HealVeil();
    private static Magic_Warp warp = new Magic_Warp();

    private static Portion portion = new Portion();
    private static SecondSight secondSight = new SecondSight();
    private static Ether ether = new Ether();
    private static HappyPortion happyPortion=new HappyPortion();
    private static UnHappyPortion unHappyPortion = new UnHappyPortion();

    private static List<Item> ItemList = new List<Item>() { fire5,water5,wind5,stone5,burn3,sprash3,blast3,quake3,attackUp,defenceUp,heal2,areaHeal2,phoenix,freee,speedUp,
                                                            explosion,bigBurn,inferno,superNova,volcano,meteo,stome,thundar,ressurection,healVeil,warp,

                                                            portion,secondSight,ether,happyPortion,unHappyPortion};

    public static Material ReturnMaterial(ItemEnum itemEnum)
    {
        switch (itemEnum)
        {
            case ItemEnum.Magic_Fire5:
                return MaterialStock.instance.R;
            case ItemEnum.Magic_Water5:
                return MaterialStock.instance.B;
            case ItemEnum.Magic_Wind5:
                return MaterialStock.instance.G;
            case ItemEnum.Magic_Stone5:
                return MaterialStock.instance.Y;
            case ItemEnum.Magic_Burn3:
                return MaterialStock.instance.R;
            case ItemEnum.Magic_Sprash3:
                return MaterialStock.instance.B;
            case ItemEnum.Magic_Blast3:
                return MaterialStock.instance.G;
            case ItemEnum.Magic_Quake3:
                return MaterialStock.instance.Y;
            case ItemEnum.Magic_AttackUp:
                return MaterialStock.instance.R;
            case ItemEnum.Magic_DefenceUp:
                return MaterialStock.instance.B;
            case ItemEnum.Magic_Heal2:
                return MaterialStock.instance.Y;
            case ItemEnum.Magic_AreaHeal2:
                return MaterialStock.instance.Y;
            case ItemEnum.Magic_Phoenix:
                return MaterialStock.instance.R;
            case ItemEnum.Magic_Freeze:
                return MaterialStock.instance.B;
            case ItemEnum.Magic_SpeedUp:
                return MaterialStock.instance.G;
            case ItemEnum.Magic_Explosion:
                return MaterialStock.instance.RandB;
            case ItemEnum.Magic_BigBurn:
                return MaterialStock.instance.RandB;
            case ItemEnum.Magic_Inferno:
                return MaterialStock.instance.RandG;
            case ItemEnum.Magic_SuperNova:
                return MaterialStock.instance.RandG;
            case ItemEnum.Magic_Volcano:
                return MaterialStock.instance.RandY;
            case ItemEnum.Magic_Meteo:
                return MaterialStock.instance.RandY;
            case ItemEnum.Magic_Stome:
                return MaterialStock.instance.BandG;
            case ItemEnum.Magic_Thundar:
                return MaterialStock.instance.BandG;
            case ItemEnum.Magic_Ressurection:
                return MaterialStock.instance.BandY;
            case ItemEnum.Magic_HealVeil:
                return MaterialStock.instance.BandY;
            case ItemEnum.Magic_Craft:
                return MaterialStock.instance.YandG;
            case ItemEnum.Magic_Warp:
                return MaterialStock.instance.YandG;
            case ItemEnum.Portion:
                return MaterialStock.instance.R;
            case ItemEnum.SecondSight:
                return MaterialStock.instance.RandBandY;
            case ItemEnum.Ether:
                return MaterialStock.instance.RandB;
            case ItemEnum.HappyPortion:
                return MaterialStock.instance.Y;
            case ItemEnum.UnHappyPortion:
                return MaterialStock.instance.RandYandG;
            default:
                Debug.Log("Itemでバグ");
                break;
        }
        return null;
    }

    public static Item ReturnItemFromItemEnum(ItemEnum itemEnum)
    {
        Item item = null;
        foreach(var obj in ItemList)
        {
            if (obj.itemData.ID == itemEnum)
            {
                return obj;
            }
        }
        return item;
    }

    public static string ReturnExplainTextFormItemEnum(ItemEnum itemEnum)
    {
        foreach (var obj in ItemList)
        {
            if (obj.itemData.ID == itemEnum)
            {
                return obj.itemData.ExplainText;
            }
        }
        return itemEnum+"";
    }

    public static string ReturnNameFromItemEnum(ItemEnum itemEnum)
    {
        foreach (var obj in ItemList)
        {
            if (obj.itemData.ID == itemEnum)
            {
                return obj.itemData.Name;
            }
        }
        return itemEnum+"";
    }

}
