using System;
using System.Collections.Generic;

public class PlayerConditionManager
{
    public static PlayerConditionConstract Atk1p2 = new PlayerConditionConstract(PlayerCondition.Atk1p2, "攻撃力UP");
    public static PlayerConditionConstract Atk1p5 = new PlayerConditionConstract(PlayerCondition.Atk1p5, "攻撃力UP");
    public static PlayerConditionConstract Def1p2 = new PlayerConditionConstract(PlayerCondition.Def1p2, "防御力UP");
    public static PlayerConditionConstract Def1p5 = new PlayerConditionConstract(PlayerCondition.Def1p5, "防御力UP");
    public static PlayerConditionConstract Eva1p2 = new PlayerConditionConstract(PlayerCondition.Eva1p2, "回避率UP");
    public static PlayerConditionConstract Eva1p5 = new PlayerConditionConstract(PlayerCondition.Eva1p5, "回避率UP");
    public static PlayerConditionConstract SpeedUp = new PlayerConditionConstract(PlayerCondition.SpedUp, "行動速度UP");
    public static PlayerConditionConstract SpeedDown = new PlayerConditionConstract(PlayerCondition.SpeedDown, "行動速度Down");
    public static PlayerConditionConstract HealVeil = new PlayerConditionConstract(PlayerCondition.HealVeil, "ヒールベール");

    private static List<PlayerConditionConstract> list = new List<PlayerConditionConstract>() { Atk1p2, Atk1p5, Def1p2, Def1p5, Eva1p2, Eva1p5,SpeedUp,SpeedDown,HealVeil };

    public static string ReturnShowTextFromPlayerCondition(PlayerCondition condition)
    {
        foreach(var obj in list)
        {
            if (obj.condition == condition)
            {
                return obj.showText;
            }
        }
        return "PlayerConditionManagerでバグ";
    }
}


public class PlayerConditionConstract
{
    public PlayerCondition condition;
    public string showText;
    public PlayerConditionConstract(PlayerCondition _condition,string _showText)
    {
        condition = _condition;
        showText = _showText;
    }
}
