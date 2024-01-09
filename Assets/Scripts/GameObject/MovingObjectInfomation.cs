using System;
using UnityEngine;

public class MovingObjectInfomation
{
    //プレイヤー・敵のレベル毎のデータ　基本このデータに基づき、場合によりこれにボーナスを付与する
    public readonly int[] MaxHpData = new int[] { 0, 15, 20, 25, 30, 35, 41, 47, 53, 59, 76, 83, 90, 98,106,114,123,133,143,153,164,175,186,198,210,223,235,248,261,275,289,304,319,335,351,368,385,403,421,440,460 };
    public readonly int[] MaxMpData = new int[] { 0, 20, 30, 40, 50, 60, 70, 80, 90,100,110,120,130,140,150,160,170,180,190,200,210,220,230,240,250,260,270,280,290,300,310,320,330,340,350,360,370,380,390,400,410 };
    public readonly int[] AtkData = new int[]   { 0, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50 };
    public readonly int[] DefData = new int[]   { 0, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50 };
    public readonly long[] DropExpData = new long[] {0,5,10,30,60,100,150,230,350,500,700,950,1200,1500,1800,2300,2800,3500,4200,5000,6000,7000,8000,10000,13000,16000,20000,25000,30000,36000,42000,48000,54000,60000,70000,80000,90000,100000,115000,130000,1450000};
    public readonly long[] NeedExpData = new long []{0, 10, 30, 60, 100, 150, 230, 350, 500, 700, 950, 1200, 1500, 1800, 2300, 2800, 3500, 4200, 5000, 6000, 7000, 8000, 10000, 13000, 16000, 20000, 25000, 30000, 36000, 42000, 48000, 54000, 60000, 70000, 80000, 90000, 100000, 115000, 130000, 1450000, 160000 };
    private const int LevelCap = 40;

    public string Name { get; set; }
    public int Level { get; set; }
    public int Hp{ get; set; }
    public int MaxHp { get; private set; }
    public int Mp { get; set; }
    public int MaxMp { get; private set; }
    public int Atk { get; private set; }
    public int Def { get; private set; }

    public int FireLevel { get; private set; }
    public int WaterLevel { get; private set; }
    public int WindLevel { get; private set; }
    public int GroundLevel { get; private set; }
    public int MasteryLevelMax { get; } = 10;

    public SkillLvMemeory skillLvMemory { get; private set; }

    public SkillEnum selectSkill { get; set; }

    public int SkillPoint { get; set; }
    public long NowExp { get; set; }

    public Material material { get; private set; }

    public MovingObjectInfomation(int level)
    {
        Level = level;
        FireLevel = 0;
        WaterLevel = 0;
        WindLevel = 0;
        GroundLevel = 0;
        SkillPoint = 2+2*level;
        SetParameter(level);
        NowExp = 0;
        skillLvMemory = new SkillLvMemeory(this);
        selectSkill = SkillEnum.None;
    }

    public bool AddExp(long exp)
    {
        NowExp += exp;
        bool isLevelUp=false;
        while(NowExp >= NeedExpData[Level])
        {
            LevelUp();
            isLevelUp = true;
        }
        return isLevelUp;
    }

    private void LevelUp()
    {
        NowExp -= NeedExpData[Level];
        if (Level < LevelCap)
        {
            Level++;//レベルキャップ作るならここ
            SetParameter(Level);
            SkillPoint += 2;
        }
    }

    public void SetMasteryPoint(int fireLevel,int waterLevel,int groundLevel,int windLevel)
    {
        FireLevel = fireLevel;
        WaterLevel = waterLevel;
        WindLevel = windLevel;
        GroundLevel = groundLevel;
    }

    public void AddFirePoint(int point)
    {
        FireLevel += point;
        SkillPoint -= point;
    }

    public void AddWaterePoint(int point)
    {
        WaterLevel += point;
        SkillPoint -= point;
    }

    public void AddGroundPoint(int point)
    {
        GroundLevel += point;
        SkillPoint -= point;
    }

    public void AddWindPoint(int point)
    {
        WindLevel += point;
        SkillPoint -= point;
    }

    public void SetParameter(int level)
    {
        MaxHp = MaxHpData[level];
        MaxMp = MaxMpData[level];
        if (Hp < MaxHp)
        {
            Hp = MaxHp;
        }
        if (Mp < MaxMp)
        {
            Mp = MaxMp;
        }
        Atk= AtkData[level] ;
        Def = DefData[level] ;
    }

    public void ChoiceMaterialForPlayer()
    {
        ChoiceMaterialForPlayer(material);
    }

    public void ChoiceMaterialForPlayer(Material material)
    {
        int count=0;
        if (FireLevel != 0){  count++; }
        if (WaterLevel != 0) { count++; }
        if (WindLevel != 0) { count++; }
        if (GroundLevel != 0) { count++; }

        if (count == 0)
        {
            material.SetColor("_KeyColor", new Color(1,1,1));
            this.material = material;
            return;
        }

        int r=0, g=0, b=0;
        if (FireLevel >= 8)
        {
            if (count == 1)
            {
                r += 255;
            }
            else
            {
                r += 100;
            }
        }else if (FireLevel >= 5)
        {
            r += 200; g += 50;b += 50;
        }
        else if(FireLevel>=1)
        {
            r += 255;g += 180;b += 180;
        }

        if (WaterLevel >= 8)
        {
            if (count == 1)
            {
                b += 255;
            }
            else
            {
                b += 100;
            }
        }
        else if (WaterLevel >= 5)
        {
            r += 50;g += 50;b += 200;
        }
        else if(WaterLevel>=1)
        {
            r += 180;g += 180;b += 255;
        }

        if (WindLevel >= 8)
        {
            if (count == 1)
            {
                g += 255;
            }
            else
            {
                g += 100;
            }
        }else if (WindLevel >= 5)
        {
            r += 50;g += 200;b += 50;
        }
        else if(WindLevel>=1)
        {
            r += 180;g += 255;b += 180;
        }

        if (GroundLevel >= 8)
        {
            if (count == 1)
            {
                r += 255;g += 241;b += 0;
            }
            else
            {
                r += 126; g += 118; b += 4;
            }
        }else if (GroundLevel >= 5)
        {
            r += 200;g += 192;b += 74;
        }
        else if(GroundLevel>=1)
        {
            r += 248;g += 242;b += 151;
        }

        material.SetColor("_KeyColor",new Color((float)r / (255*count), (float)g /( 255*count), (float)b/ (255*count)));
        this.material = material;
    }

    public void ChoiceMaterialForEnemy()
    {
        if (FireLevel != 0)
        {
            if (WaterLevel != 0)
            {
                if (GroundLevel != 0)
                {
                    if (WindLevel != 0)
                    {
                        material = MaterialStock.instance.Black;
                    }
                    else
                    {
                        material = MaterialStock.instance.RandBandY;
                    }
                }
                else
                {
                    if (WindLevel != 0)
                    {
                        material = MaterialStock.instance.RandBandG;
                    }
                    else
                    {
                        material = MaterialStock.instance.RandB;
                    }
                }
            }
            else
            {
                if (GroundLevel != 0)
                {
                    if (WindLevel != 0)
                    {
                        material = MaterialStock.instance.RandYandG;
                    }
                    else
                    {
                        material = MaterialStock.instance.RandY;
                    }
                }
                else
                {
                    if (WindLevel != 0)
                    {
                        material = MaterialStock.instance.RandG;
                    }
                    else
                    {
                        material = MaterialStock.instance.R;
                    }
                }
            }
        }
        else
        {
            if (WaterLevel != 0)
            {
                if (GroundLevel != 0)
                {
                    if (WindLevel != 0)
                    {
                        material = MaterialStock.instance.BandYandG;
                    }
                    else
                    {
                        material = MaterialStock.instance.BandY;
                    }
                }
                else
                {
                    if (WindLevel != 0)
                    {
                        material = MaterialStock.instance.BandG;
                    }
                    else
                    {
                        material = MaterialStock.instance.B;
                    }
                }
            }
            else
            {
                if (GroundLevel != 0)
                {
                    if (WindLevel != 0)
                    {
                        material = MaterialStock.instance.YandG;
                    }
                    else
                    {
                        material = MaterialStock.instance.Y;
                    }
                }
                else
                {
                    if (WindLevel != 0)
                    {
                        material = MaterialStock.instance.G;
                    }
                    else
                    {
                        material = MaterialStock.instance.White;
                    }
                }
            }
        }
    }
}
