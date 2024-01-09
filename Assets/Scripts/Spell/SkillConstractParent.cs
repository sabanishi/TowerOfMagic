using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public static class SkillConstractParent
{
    public static SkillConstract Fire =
            new SkillConstract(SkillEnum.Fire, "ファイヤ",5,new int[] {1,　0,　0,　0 },
            new float[] {0,1.3f,1.4f,1.5f,1.6f,1.7f,1.8f,1.9f,2.0f,2.2f,2.4f },
            new int[] {0,4 ,6 ,8 ,10 ,12 ,16 ,20 ,24 ,28,32 },
            "前方一マスに火属性攻撃","攻撃倍率:");

    public static SkillConstract Water =
            new SkillConstract(SkillEnum.Water, "ウォーター", 10,new int[] {0, 1, 0, 0 },
            new float[] { 0,1.2f, 1.3f, 1.4f, 1.5f, 1.6f, 1.8f, 1.9f, 2.0f, 2.1f, 2.2f },
            new int[] { 0, 4, 6, 8, 10, 12, 16, 20, 24, 28, 32 },
            "周囲一マスに水属性攻撃", "攻撃倍率:");

    public static SkillConstract Stone =
            new SkillConstract(SkillEnum.Stone, "ストーン", 10,new int[] { 0, 0, 1, 0 },
            new float[] { 0,1.4f, 1.5f, 1.6f, 1.8f, 1.9f, 2.0f, 2.1f, 2.2f, 2.4f,2.6f },
            new int[] { 0, 4, 6, 8, 10, 12, 16, 20, 24, 28, 32 },
            "斜め一マスに地属性攻撃", "攻撃倍率:");

    public static SkillConstract Wind =
            new SkillConstract(SkillEnum.Wind, "ウィンド",10, new int[] { 0, 0, 0, 1 },
            new float[] { 0, 1.2f, 1.3f, 1.4f, 1.5f, 1.6f, 1.8f, 1.9f, 2.0f, 2.1f, 2.2f },
            new int[] { 0, 4, 6, 8, 10, 12, 16, 20, 24, 28, 32 },
            "前方三マスに風属性攻撃", "攻撃倍率:");

    public static SkillConstract Burn =
        new SkillConstract(SkillEnum.Burn, "バーン", 10,new int[] { 6, 0, 0, 0 },
          new float[] { 0, 1.7f, 1.8f, 1.9f, 2.0f, 2.2f, 2.3f, 2.4f, 2.6f, 2.8f, 3.0f},
          new int[] { 0, 7, 9, 11, 15, 19, 23, 27, 31, 35, 40 },
          "前方一マスに火属性攻撃", "攻撃倍率:");

    public static SkillConstract Sprash =
            new SkillConstract(SkillEnum.Sprash, "スプラッシュ",10, new int[] { 0, 6, 0, 0 },
            new float[] { 0,1.3f,1.4f, 1.5f, 1.6f, 1.7f, 1.8f, 2.0f, 2.2f ,2.4f,2.6f},
            new int[] { 0, 7, 9, 11, 15, 19, 23, 27, 31, 35, 40 },
            "周囲二マスに水属性攻撃", "攻撃倍率:");

    public static SkillConstract Quake =
            new SkillConstract(SkillEnum.Quake, "クエイク", 10,new int[] { 0, 0, 6, 0 },
            new float[] { 0, 1.5f, 1.6f, 1.7f, 1.8f, 1.9f, 2.0f, 2.2f, 2.4f, 2.6f, 2.8f },
            new int[] { 0, 7, 9, 11, 15, 19, 23, 27, 31, 35, 40 },
            "周囲一マスに地属性攻撃", "攻撃倍率:");

    public static SkillConstract Blast =
            new SkillConstract(SkillEnum.Blast, "ブラスト",10, new int[] { 0, 0, 0, 6 },
            new float[] { 0, 1.3f, 1.4f, 1.5f, 1.6f, 1.8f, 2.0f, 2.2f, 2.4f, 2.6f,2.8f},
            new int[] { 0, 7, 9, 11, 15, 19, 23, 27, 31, 35, 40 },
            "前方五マスに風属性攻撃。", "攻撃倍率:");

    public static SkillConstract AttackUp =
            new SkillConstract(SkillEnum.AttackUp, "アタックアップ", 5, new int[] { 3, 0, 0, 0 },
            new float[] { 0, 1.2f, 1.2f, 1.5f,1.5f, 1.5f, },
            new int[] { 0,15,10,15,10, 8 },
            "20ターンの間攻撃力を上げる", "攻撃力UP:");

    public static SkillConstract DefenceUp =
            new SkillConstract(SkillEnum.DefenceUp, "ディフェンスアップ", 5, new int[] { 0, 3, 0, 0 },
            new float[] { 0, 1.2f, 1.2f, 1.5f, 1.5f, 1.5f, },
            new int[] { 0, 15, 10, 15, 10, 8 },
            "20ターンの間防御力を上げる", "防御力UP:");

    public static SkillConstract AgilityUp =
            new SkillConstract(SkillEnum.AgilityUp, "アジリティアップ", 5, new int[] { 0, 0, 0, 3 },
            new float[] { 0, 1.2f, 1.2f, 1.5f, 1.5f, 1.5f, },
            new int[] { 0, 15, 10, 15, 10, 8 },
            "20ターンの間回避率を上げる", "回避率UP:");

    public static SkillConstract Heal =
            new SkillConstract(SkillEnum.Heal, "ヒール", 5, new int[] { 0, 0, 3, 0 },
            new float[] { 0, 0.2f, 0.5f, 0.6f,0.8f, 1.0f, },
            new int[] { 0, 4, 6, 8, 10, 12 },
            "自分のHPを回復する", "回復量:最大HPの");

    public static SkillConstract Phoenix =
            new SkillConstract(SkillEnum.Phoenix, "フェニックス", 5, new int[] { 10, 0,0 ,0 },
            new float[] { 0, 0.4f, 0.8f, 1.2f, 1.6f, 2.0f, },
            new int[] { 0,1, 1, 1, 1, 1 },
            "自分のHPを０にする事で\n仲間のHP,MPを限界以上回復する","回復量:最大HP,MPの");

    public static SkillConstract Freeze =
            new SkillConstract(SkillEnum.Freeze, "フリーズ", 5, new int[] { 0, 10, 0, 0 },
            new float[] { 0, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, },
            new int[] { 0,40,35,30,25,20 },
            "周囲一マスの敵の行動速度を10ターンの間下げる", "行動速度:");

    public static SkillConstract SpeedUp =
            new SkillConstract(SkillEnum.SpeedUp, "スピードアップ", 5, new int[] { 0, 0, 0, 10 },
            new float[] { 0, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, },
            new int[] { 0, 40, 35, 30, 25, 20 },
            "20ターンの間行動速度を上げる", "行動速度:");

    public static SkillConstract AreaHeal =
            new SkillConstract(SkillEnum.AreaHeal, "エリアヒール", 5, new int[] { 0, 0, 10, 0 },
            new float[] { 0, 0.2f, 0.5f, 0.6f, 0.8f, 1.0f, },
            new int[] { 0, 10, 15, 20, 25, 30 },
            "味方全体のHPを回復する", "回復量:最大HPの");

    public static SkillConstract Explosion =
           new SkillConstract(SkillEnum.Explosion, "エクスプロージョン", 10, new int[] { 5, 5, 0, 0 },
           new float[] { 0, 1.7f, 1.8f, 1.9f, 2.0f, 2.2f, 2.3f, 2.4f, 2.6f, 2.8f, 3.0f },
           new int[] { 0, 9, 11, 15, 19, 23, 27, 32, 37, 42, 50 },
           "周囲一マスに火+水属性攻撃", "攻撃倍率");

    public static SkillConstract BigBurn =
          new SkillConstract(SkillEnum.BigBurn, "ビッグバン", 10, new int[] { 8, 8, 0, 0 },
          new float[] { 0, 2.0f, 2.2f, 2.3f, 2.4f, 2.6f, 2.8f, 3.0f ,3.2f,3.4f,3.8f },
          new int[] { 0,16, 18, 22, 26, 30, 35 ,40,45,50,60},
          "周囲二マスに火+水属性攻撃", "攻撃倍率");

    public static SkillConstract Comet =
        new SkillConstract(SkillEnum.Comet, "インフェルノ", 10, new int[] { 5, 0, 0, 5 },
        new float[] { 0, 1.7f, 1.8f, 1.9f, 2.0f, 2.2f, 2.3f, 2.4f, 2.6f, 2.8f, 3.0f },
        new int[] { 0, 9, 11, 15, 19, 23, 27, 32, 37, 42, 50 },
        "前方三マスに火+風属性攻撃", "攻撃倍率:");

    public static SkillConstract SuperNova =
         new SkillConstract(SkillEnum.SuperNova, "スーパーノヴァ", 10, new int[] { 8, 0, 0, 8 },
         new float[] { 0, 2.6f, 2.8f, 3.0f, 3.2f, 3.4f, 3.8f ,4.0f,4.3f,4.6f,5.0f},
         new int[] { 0, 16, 18, 22, 26, 30, 35, 40, 45, 50, 60 },
         "自分のHPを半分削り、\n周囲一マスに強力な火+風属性攻撃", "攻撃倍率");

    public static SkillConstract Volcano =
         new SkillConstract(SkillEnum.Volcano, "ボルケイノ", 10, new int[] { 5, 0, 5, 0 },
        new float[] { 0, 2.0f, 2.2f, 2.3f, 2.4f, 2.6f, 2.8f, 3.0f ,3.2f,3.4f,3.6f},
        new int[] { 0, 9, 10, 12, 14, 16, 18, 20, 22, 24, 26 },
        "斜め一マスに火+地属性攻撃", "攻撃倍率:");

    public static SkillConstract Meteo =
        new SkillConstract(SkillEnum.Meteo, "メテオ", 10, new int[] { 8, 0, 8, 0 },
        new float[] { 0, 2.0f, 2.2f, 2.3f, 2.4f, 2.6f, 2.8f, 3.0f, 3.2f, 3.4f, 3.6f },
        new int[] { 0, 16, 18, 22, 26, 30, 35, 40, 45, 50, 60 },
        "周囲一マスに火+地属性攻撃", "攻撃倍率:");

    public static SkillConstract Stome =
        new SkillConstract(SkillEnum.Stome, "ストーム", 10, new int[] { 0, 5, 0, 5 },
        new float[] { 0, 1.3f, 1.4f, 1.5f, 1.6f, 1.7f, 1.8f, 2.0f, 2.2f, 2.4f, 2.6f },
        new int[] { 0, 9, 11, 15, 19, 23, 27, 32, 37, 42, 50 },
        "周囲三マスに水+風属性攻撃", "攻撃倍率:");

    public static SkillConstract Thundar =
        new SkillConstract(SkillEnum.Thundar, "サンダー", 10, new int[] { 0, 8, 0, 8 },
        new float[] { 0, 1.3f, 1.4f, 1.5f, 1.6f, 1.7f, 1.8f, 2.0f, 2.2f, 2.4f, 2.6f },
        new int[] { 0, 16, 18, 22, 26, 30, 35, 40, 45, 50, 60 },
        "周囲五マスに水+風属性攻撃", "攻撃倍率:");

    public static SkillConstract Ressurection =
        new SkillConstract(SkillEnum.Resurrection, "リザレクション", 5, new int[] { 0, 5, 5, 0 },
        new float[] { 0, 0.1f, 0.2f, 0.4f, 0.6f, 1.0f, },
        new int[] { 0, 15,20, 25,30,40 },
        "他の仲間を蘇生", "蘇生時HP:");

    public static SkillConstract HealVeil =
        new SkillConstract(SkillEnum.HealVeil, "ヒールベール", 5, new int[] { 0, 8, 8, 0 },
        new float[] { 0, 0.05f, 0.1f, 0.15f, 0.2f, 0.25f, },
        new int[] { 0, 16, 20, 24, 28, 32 },
        "20ターンの間HPを小回復", "回復量:");

    public static SkillConstract Craft =
  new SkillConstract(SkillEnum.Craft, "クラフト", 5, new int[] { 0, 0, 5, 5 },
  new float[] { 0, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, },
  new int[] { 0, 10,12,14,16,20 },
  "ランダムでアイテムを一つ入手する", "アイテムレアリティ:");

    public static SkillConstract Warp =
        new SkillConstract(SkillEnum.Warp, "ワープ", 5, new int[] { 0, 0, 8, 8 },
        new float[] { 0, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, },
        new int[] { 0,200, 160, 120, 80, 40 },
        "階段の近くにワープ", "成功確立:");

    private static SkillConstract[] Skills = new SkillConstract[] { Fire,AttackUp, Burn,Phoenix,
                                                                    Water,DefenceUp, Sprash,Freeze,
                                                                    Stone,Heal, Quake,AreaHeal,
                                                                    Wind, Blast,SpeedUp
                                                                    ,Explosion,BigBurn,Comet,SuperNova,Volcano,Meteo,Stome,Thundar
                                                                    ,Ressurection ,HealVeil,Craft,Warp};

    public static SkillConstract[] GetAllSkill()
    {
        return Skills.ToArray();
    }

    public static string ReturnNameFromSkillEnum(SkillEnum skillEnum)
    {
        foreach (var skill in Skills)
        {
            if (skill.skillEnum == skillEnum)
            {
                return skill.Name;
            }
        }
        return "SkillConstractParentでバグ";
    }

    public static SkillEnum ReturnSkillEnumForName(string skillName)
    {
        foreach (var skill in Skills)
        {
            if (skill.Name == skillName)
            {
                return skill.skillEnum;
            }
        }
        return SkillEnum.None;
    }

    public static string ReturnExplainFromName(string skillName)
    {
        foreach (var skill in Skills)
        {
            if (skill.Name == skillName)
            {
                return skill.Explain;
            }
        }
        return "SkillConstractParentでバグ";
    }

    public static string ReturnExplainFromSkillEnum(SkillEnum skillEnum)
    {
        foreach (var skill in Skills)
        {
            if (skill.skillEnum == skillEnum)
            {
                return skill.Explain;
            }
        }
        return "SkillConstractParentでバグ";
    }

    public static string ReturnExplainForAssignSkillFromName(string skillName)
    {
        foreach (var skill in Skills)
        {
            if (skill.Name == skillName)
            {
                return skill.ExplainForAssignSkill;
            }
        }
        return "SkillConstractParentでバグ";
    }

    public static string ReturnSkillEffectExplainFromName(string skillName, int level)
    {
        foreach (var skill in Skills)
        {
            if (skill.Name == skillName)
            {
                int num = (int)(skill.Scales[level] * 100);
                return skill.SkillEffectExplain + num + "%";
            }
        }
        return "SkillConstractParentでバグ";

    }

    public static string ReturnSkillEffectExplainFromSkillEnum(SkillEnum skillEnum,int level)
    {
        foreach (var skill in Skills)
        {
            if (skill.skillEnum == skillEnum)
            {
                int num = (int)(skill.Scales[level] * 100);
                return skill.SkillEffectExplain + num + "%";
            }
        }
        return "SkillConstractParentでバグ";
    }

    public static bool ReturnCanLvUpFromName(string skillName,int[] nowSkill)
    {
        foreach (var skill in Skills)
        {
            if (skill.Name == skillName)
            {
                if(nowSkill[0]>=skill.NeedSkillLv[0]&& nowSkill[1] >= skill.NeedSkillLv[1]&&
                    nowSkill[2] >= skill.NeedSkillLv[2]&& nowSkill[3] >= skill.NeedSkillLv[3])
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }

    public static float ReturnScaleFromName(string skillName,int level)
    {
        foreach (var skill in Skills)
        {
            if (skill.Name == skillName)
            {
                if (level < skill.Scales.Length)
                {
                    return skill.Scales[level];
                }
                else
                {
                    return 0;
                }
            }
        }
        return 0;
    }

    public static int ReturnNeedMpFromName(string skillName, int level)
    {
        foreach (var skill in Skills)
        {
            if (skill.Name == skillName)
            {
                if (level < skill.NeedMps.Length)
                {
                    return skill.NeedMps[level];
                }
                else
                {
                    return 0;
                }
            }
        }
        return 0;
    }

    public static float ReturnScaleFromSkillEnum(SkillEnum skillEnum,int level)
    {
        foreach (var skill in Skills)
        {
            if (skill.skillEnum == skillEnum)
            {
                if (level < skill.Scales.Length)
                {
                    return skill.Scales[level];
                }
                else
                {
                    return 0;
                }
            }
        }
        return 0;
    }

    public static int ReturnNeedMpFromSkillEnum(SkillEnum skillEnum,int level)
    {
        foreach (var skill in Skills)
        {
            if (skill.skillEnum == skillEnum)
            {
                if (level < skill.NeedMps.Length)
                {
                    return skill.NeedMps[level];
                }
                else
                {
                    return 0;
                }
            }
        }
        return 0;
    }

    public static int ReturnMaxLvFromName(string skillName)
    {
        foreach (var skill in Skills)
        {
            if (skill.Name == skillName)
            {
                return skill.MaxLv;
            }
        }
        return 0;
    }

    public static List<Attribute> ReturnAttributeListFromSkillEnum(SkillEnum skillEnum)
    {
        foreach (var skill in Skills)
        {
            if (skill.skillEnum == skillEnum)
            {
                return skill.attributes;
            }
        }
        return null;
     }
}
