using System;
using System.Collections.Generic;

public class SkillConstract
{
    public SkillEnum skillEnum { get; private set; }
    public int skillLv { get; set; }
    public string Name { get; private set; }
    public string Explain { get; private set; }
    public int[] NeedSkillLv { get; private set; }//Fire,Wind,Ground,Windの順
    public int MaxLv { get; private set; }
    public int[] NeedMps { get; private set; }
    public float[] Scales { get; set; }
    public string ExplainForAssignSkill { get; private set; }
    public string SkillEffectExplain { get; private set; }
    public List<Attribute> attributes = new List<Attribute>();

    public SkillConstract(SkillEnum _skillEnum,string _Name,int _MaxLv,int[] _NeedSkillLv,float[] _Scales,int[] _NeedMps ,string _Explain,string skillExplain)
    {
        skillEnum = _skillEnum;
        skillLv = 0;
        Name = _Name;
        Explain = _Explain;
        NeedSkillLv = _NeedSkillLv;
        Scales = _Scales;
        NeedMps = _NeedMps;
        MaxLv = _MaxLv;
        string NeedText = "";
        if (NeedSkillLv[0] != 0)
        {
            NeedText += "火:" + NeedSkillLv[0]+" ";
            attributes.Add(Attribute.Fire);
        }
        if (NeedSkillLv[1] != 0)
        {
            NeedText += "水:" + NeedSkillLv[1] + " ";
            attributes.Add(Attribute.Water);
        }
        if (NeedSkillLv[2] != 0)
        {
            NeedText += "地:" + NeedSkillLv[2] + " ";
            attributes.Add(Attribute.Ground);
        }
        if (NeedSkillLv[3] != 0)
        {
            NeedText += "風:" + NeedSkillLv[3] + " ";
            attributes.Add(Attribute.Wind);
        }
        ExplainForAssignSkill = Explain + "\n必要マスタリ　" + NeedText;

        SkillEffectExplain = skillExplain;
    }
}
