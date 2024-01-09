using System;
using System.Collections.Generic;

public class SkillLvMemeory
{
    private MovingObjectInfomation parent;
    private Dictionary<SkillEnum, int> SkillLvConstract = new Dictionary<SkillEnum, int>()
    {
        {SkillEnum.Fire,0},
        {SkillEnum.Water,0 },
        {SkillEnum.Wind,0 },
        {SkillEnum.Stone,0 },
        {SkillEnum.Burn,0 },
        {SkillEnum.Sprash,0 },
        {SkillEnum.Blast,0 },
        {SkillEnum.Quake,0 },
        {SkillEnum.AttackUp,0 },
        {SkillEnum.DefenceUp,0 },
        {SkillEnum.AgilityUp,0 },
        {SkillEnum.Heal,0 },
        {SkillEnum.Phoenix,0 },
        {SkillEnum.Freeze,0 },
        {SkillEnum.SpeedUp,0 },
        {SkillEnum.Comet,0 },
        {SkillEnum.AreaHeal,0 },
        {SkillEnum.Explosion,0 },
        {SkillEnum.BigBurn,0 },
        {SkillEnum.SuperNova,0 },
        {SkillEnum.Volcano,0 },
        {SkillEnum.Meteo,0 },
        {SkillEnum.Stome,0 },
        {SkillEnum.Thundar,0 },
        {SkillEnum.Resurrection,0 },
        {SkillEnum.HealVeil,0 },
        {SkillEnum.Craft,0 },
        {SkillEnum.Warp,0 }
    };

    public int GetSkillLV(SkillEnum skillEnum)
    {
        return SkillLvConstract[skillEnum];
    }

    public void SetSkillLV(SkillEnum skillEnum,int level)
    {
        SkillLvConstract[skillEnum] = level;
    }

    public void AddSkillLv(SkillEnum skillEnum,int addPoint)
    {
        SkillLvConstract[skillEnum] += addPoint;
        parent.SkillPoint -= addPoint;
    }

    public SkillLvMemeory(MovingObjectInfomation _parent)
    {
        parent = _parent;
    }
}
