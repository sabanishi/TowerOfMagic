using System;
using System.Collections.Generic;

public static class CreateEnemyData
{
	private static List<string> nameList = new List<string>() {"ファウスト","マーリン","エリフィス","アレイスター","マグレガー","ダイアン","オースティン","ケネス","ウィリアム",
																"バトラー","キャロル","キルケー","メーディア","テオドル","フランツ"};
    public static void InitializeEnemy(Enemy enemy,int level)
    {
		MovingObjectInfomation info=SetEnemyData(enemy, level);

		info.Name = NameEnemy();
		info.ChoiceMaterialForEnemy();
		enemy.SetNowMovingObjectInfo(info);
		
	}

	private static MovingObjectInfomation SetEnemyData(Enemy enemy,int level)
    {
		int randomCount=0;
		MovingObjectInfomation info = null;
		switch (level)
		{
			case 1:
				info = new MovingObjectInfomation(1);
				info.SetMasteryPoint(0, 0, 0, 0);
				enemy.EnemySetting(1.0f, new List<SkillEnum> { });
				break;
			case 2:
				randomCount = Utils.GetRandomInt(0, 3);
				info=CreateLevel2(randomCount,enemy,info);
				break;
			case 3:
				randomCount = Utils.GetRandomInt(0, 7);
                if (randomCount < 4)
                {
					info=CreateLevel3(randomCount, enemy, info);
                }
                else
                {
					info=CreateLevel2(randomCount - 4, enemy, info);
                }
				break;
			case 4:
				randomCount = Utils.GetRandomInt(0, 7);
				if (randomCount < 4)
				{
					info = CreateLevel4(randomCount, enemy, info);
				}
				else
				{
					info = CreateLevel3(randomCount - 4, enemy, info);
				}
				break;
			case 5:
				randomCount = Utils.GetRandomInt(0, 7);
				if (randomCount < 4)
				{
					info = CreateLevel5(randomCount, enemy, info);
				}
				else
				{
					info = CreateLevel4(randomCount - 4, enemy, info);
				}
				break;
			case 6:
				randomCount = Utils.GetRandomInt(0, 7);
				if (randomCount < 4)
				{
					info = CreateLevel6(randomCount, enemy, info);
				}
				else
				{
					info = CreateLevel5(randomCount - 4, enemy, info);
				}
				break;
			case 7:
				randomCount = Utils.GetRandomInt(0, 7);
				if (randomCount < 4)
				{
					info = CreateLevel7(randomCount, enemy, info);
				}
				else
				{
					info = CreateLevel6(randomCount - 4, enemy, info);
				}
				break;
			case 8:
				randomCount = Utils.GetRandomInt(0, 7);
				if (randomCount < 4)
				{
					info = CreateLevel8(randomCount, enemy, info);
				}
				else
				{
					info = CreateLevel7(randomCount - 4, enemy, info);
				}
				break;
			case 9:
				randomCount = Utils.GetRandomInt(0, 7);
				if (randomCount < 4)
				{
					info = CreateLevel9(randomCount, enemy, info);
				}
				else
				{
					info = CreateLevel8(randomCount - 4, enemy, info);
				}
				break;
			case 10:
				randomCount = Utils.GetRandomInt(0, 7);
				if (randomCount < 4)
				{
					info = CreateLevel10(randomCount, enemy, info);
				}
				else
				{
					info = CreateLevel9(randomCount - 4, enemy, info);
				}
				break;
			case 11:
				randomCount = Utils.GetRandomInt(0, 7);
				if (randomCount < 4)
				{
					info = CreateLevel11(randomCount, enemy, info);
				}
				else
				{
					info = CreateLevel10(randomCount - 4, enemy, info);
				}
				break;
			case 12:
				randomCount = Utils.GetRandomInt(0, 7);
				if (randomCount < 4)
				{
					info = CreateLevel12(randomCount, enemy, info);
				}
				else
				{
					info = CreateLevel11(randomCount - 4, enemy, info);
				}
				break;
			case 13:
				randomCount = Utils.GetRandomInt(0, 7);
				if (randomCount < 4)
				{
					info = CreateLevel13(randomCount, enemy, info);
				}
				else
				{
					info = CreateLevel12(randomCount - 4, enemy, info);
				}
				break;
			case 14:
				randomCount = Utils.GetRandomInt(0, 9);
				if (randomCount < 6)
				{
					info = CreateLevel14(randomCount, enemy, info);
				}
				else
				{
					info = CreateLevel13(randomCount - 6, enemy, info);
				}
				break;
			case 15:
				randomCount = Utils.GetRandomInt(0, 11);
				if (randomCount < 6)
				{
					info = CreateLevel15(randomCount, enemy, info);
				}
				else
				{
					info = CreateLevel14(randomCount - 6, enemy, info);
				}
				break;
			case 16:
				randomCount = Utils.GetRandomInt(0, 11);
				if (randomCount < 6)
				{
					info = CreateLevel16(randomCount, enemy, info);
				}
				else
				{
					info = CreateLevel15(randomCount - 6, enemy, info);
				}
				break;
			case 17:
				randomCount = Utils.GetRandomInt(0, 11);
				if (randomCount < 6)
				{
					info = CreateLevel17(randomCount, enemy, info);
				}
				else
				{
					info = CreateLevel16(randomCount - 6, enemy, info);
				}
				break;
			case 18:
				randomCount = Utils.GetRandomInt(0, 11);
				if (randomCount < 6)
				{
					info = CreateLevel18(randomCount, enemy, info);
				}
				else
				{
					info = CreateLevel17(randomCount - 6, enemy, info);
				}
				break;
			case 19:
				randomCount = Utils.GetRandomInt(0, 11);
				if (randomCount < 6)
				{
					info = CreateLevel19(randomCount, enemy, info);
				}
				else
				{
					info = CreateLevel18(randomCount - 6, enemy, info);
				}
				break;
			case 20:
				randomCount = Utils.GetRandomInt(0, 11);
				if (randomCount < 6)
				{
					info = CreateLevel20(randomCount, enemy, info);
				}
				else
				{
					info = CreateLevel19(randomCount - 6, enemy, info);
				}
				break;
			case 21:
				randomCount = Utils.GetRandomInt(0, 11);
				if (randomCount < 6)
				{
					info = CreateLevel21(randomCount, enemy, info);
				}
				else
				{
					info = CreateLevel20(randomCount - 6, enemy, info);
				}
				break;
			case 22:
				randomCount = Utils.GetRandomInt(0, 11);
				if (randomCount < 6)
				{
					info = CreateLevel22(randomCount, enemy, info);
				}
				else
				{
					info = CreateLevel20(randomCount - 6, enemy, info);
				}
				break;
			case 23:
				randomCount = Utils.GetRandomInt(0, 1);
				if (randomCount ==0)
				{
					int r = Utils.GetRandomInt(0, 11);
					info = CreateLevel23(r, enemy, info);
				}
				else
				{
					int r = Utils.GetRandomInt(0, 5);
					info = CreateLevel22(r, enemy, info);
				}
				break;
			case 24:
				randomCount = Utils.GetRandomInt(0, 1);
				if (randomCount == 0)
				{
					int r = Utils.GetRandomInt(0, 11);
					info = CreateLevel24(r, enemy, info);
				}
				else
				{
					int r = Utils.GetRandomInt(0, 11);
					info = CreateLevel23(r, enemy, info);
				}
				break;
			case 25:
				randomCount = Utils.GetRandomInt(0, 1);
				if (randomCount == 0)
				{
					int r = Utils.GetRandomInt(0, 11);
					info = CreateLevel25(r, enemy, info);
				}
				else
				{
					int r = Utils.GetRandomInt(0, 11);
					info = CreateLevel24(r, enemy, info);
				}
				break;
			case 26:
				randomCount = Utils.GetRandomInt(0, 1);
				if (randomCount == 0)
				{
					int r = Utils.GetRandomInt(0, 7);
					info = CreateLevel26(r, enemy, info);
				}
				else
				{
					int r = Utils.GetRandomInt(0, 11);
					info = CreateLevel25(r, enemy, info);
				}
				break;
			case 27:
				randomCount = Utils.GetRandomInt(0, 1);
				if (randomCount == 0)
				{
					int r = Utils.GetRandomInt(0, 7);
					info = CreateLevel27(r, enemy, info);
				}
				else
				{
					int r = Utils.GetRandomInt(0, 7);
					info = CreateLevel26(r, enemy, info);
				}
				break;
			case 28:
				randomCount = Utils.GetRandomInt(0, 1);
				if (randomCount == 0)
				{
					int r = Utils.GetRandomInt(0, 3);
					info = CreateLevel28(r, enemy, info);
				}
				else
				{
					int r = Utils.GetRandomInt(0, 7);
					info = CreateLevel27(r, enemy, info);
				}
				break;
			case 29:
				randomCount = Utils.GetRandomInt(0, 1);
				if (randomCount == 0)
				{
					int r = Utils.GetRandomInt(0, 3);
					info = CreateLevel29(r, enemy, info);
				}
				else
				{
					int r = Utils.GetRandomInt(0, 3);
					info = CreateLevel28(r, enemy, info);
				}
				break;
			case 30:
				randomCount = Utils.GetRandomInt(0, 2);
				if (randomCount == 0)
				{
					info = CreateLevel31( enemy, info);
				}
				else if(randomCount==1)
				{
					info = CreateLevel30(enemy, info);
                }
                else
				{
					int r = Utils.GetRandomInt(0, 3);
					info = CreateLevel29(r, enemy, info);
				}
				break;
		}
		return info;
	}


	private static MovingObjectInfomation CreateLevel31(Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(31);
		info.SetMasteryPoint(10, 10, 10, 10);
		info.skillLvMemory.AddSkillLv(SkillEnum.BigBurn, 2);
		info.skillLvMemory.AddSkillLv(SkillEnum.SuperNova, 2);
		info.skillLvMemory.AddSkillLv(SkillEnum.Thundar, 2);
		info.skillLvMemory.AddSkillLv(SkillEnum.Meteo, 2);
		enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.BigBurn, SkillEnum.SuperNova, SkillEnum.Thundar, SkillEnum.Meteo});
		return info;
	}

	private static MovingObjectInfomation CreateLevel30(Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(30);
		info.SetMasteryPoint(10, 10, 10, 10);
		info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 4);
		info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 4);
		info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 4);
		info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 4);
		info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 2);
		info.skillLvMemory.AddSkillLv(SkillEnum.BigBurn, 2);
		info.skillLvMemory.AddSkillLv(SkillEnum.Stome, 2);
		info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 2);
		info.skillLvMemory.AddSkillLv(SkillEnum.SuperNova, 2);
		info.skillLvMemory.AddSkillLv(SkillEnum.Thundar, 2);
		info.skillLvMemory.AddSkillLv(SkillEnum.Meteo, 2);
		info.skillLvMemory.AddSkillLv(SkillEnum.Volcano, 2);
		enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Burn, SkillEnum.Sprash,SkillEnum.Blast,SkillEnum.Quake, SkillEnum.Explosion, SkillEnum.BigBurn, SkillEnum.Stome, SkillEnum.Comet, SkillEnum.SuperNova, SkillEnum.Thundar,SkillEnum.Meteo,SkillEnum.Volcano });
		return info;
	}

	private static MovingObjectInfomation CreateLevel29(int randomCount, Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(29);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(10, 10, 10, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.BigBurn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Volcano, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Meteo, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Burn, SkillEnum.Sprash, SkillEnum.Explosion, SkillEnum.BigBurn, SkillEnum.Volcano, SkillEnum.Quake, SkillEnum.Meteo });
				break;

			case 1:
				info.SetMasteryPoint(10, 10, 0, 10);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.BigBurn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stome, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.SuperNova, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Thundar, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Burn, SkillEnum.Sprash, SkillEnum.Explosion, SkillEnum.BigBurn, SkillEnum.Stome, SkillEnum.Comet, SkillEnum.SuperNova, SkillEnum.Thundar });
				break;

			case 2:
				info.SetMasteryPoint(0, 10, 10, 10);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 5);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 5);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 5);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stome, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Thundar, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Sprash, SkillEnum.Quake, SkillEnum.Stome, SkillEnum.Thundar });
				break;

			case 3:
				info.SetMasteryPoint(10, 0, 10, 10);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Volcano, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Meteo, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.SuperNova, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Quake, SkillEnum.Burn, SkillEnum.Volcano, SkillEnum.Meteo, SkillEnum.Comet, SkillEnum.Blast, SkillEnum.SuperNova });
				break;

			default:
				break;
		}
		return info;
	}

	private static MovingObjectInfomation CreateLevel28(int randomCount, Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(28);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(9, 9, 9, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.BigBurn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Volcano, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Meteo, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Burn, SkillEnum.Sprash, SkillEnum.Explosion, SkillEnum.BigBurn, SkillEnum.Volcano, SkillEnum.Quake, SkillEnum.Meteo });
				break;

			case 1:
				info.SetMasteryPoint(9, 9, 0, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.BigBurn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stome, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.SuperNova, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Thundar, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Burn, SkillEnum.Sprash, SkillEnum.Explosion, SkillEnum.BigBurn, SkillEnum.Stome, SkillEnum.Comet, SkillEnum.SuperNova, SkillEnum.Thundar });
				break;

			case 2:
				info.SetMasteryPoint(0, 9, 9, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 5);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 5);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 5);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stome, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Thundar, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Sprash, SkillEnum.Quake, SkillEnum.Stome, SkillEnum.Thundar });
				break;

			case 3:
				info.SetMasteryPoint(9, 0, 9, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Volcano, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Meteo, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.SuperNova, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Quake, SkillEnum.Burn, SkillEnum.Volcano, SkillEnum.Meteo, SkillEnum.Comet, SkillEnum.Blast, SkillEnum.SuperNova });
				break;

			default:
				break;
		}
		return info;
	}

	private static MovingObjectInfomation CreateLevel27(int randomCount, Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(27);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(9, 9, 9, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.BigBurn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Volcano, 1);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Meteo, 1);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Burn, SkillEnum.Sprash, SkillEnum.Explosion, SkillEnum.BigBurn, SkillEnum.Volcano, SkillEnum.Quake,SkillEnum.Meteo });
				break;
			case 1:
				info.SetMasteryPoint(9, 9, 9, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Volcano, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Meteo, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 1);
				info.skillLvMemory.AddSkillLv(SkillEnum.BigBurn, 1);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Quake, SkillEnum.Burn, SkillEnum.Volcano, SkillEnum.Meteo, SkillEnum.Explosion, SkillEnum.Sprash,SkillEnum.BigBurn });
				break;

			case 2:
				info.SetMasteryPoint(9, 9, 0, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.BigBurn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stome, 1);
				info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 1);
				info.skillLvMemory.AddSkillLv(SkillEnum.SuperNova, 1);
				info.skillLvMemory.AddSkillLv(SkillEnum.Thundar, 1);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Burn, SkillEnum.Sprash, SkillEnum.Explosion, SkillEnum.BigBurn, SkillEnum.Stome, SkillEnum.Comet,SkillEnum.SuperNova,SkillEnum.Thundar });
				break;
			case 3:
				info.SetMasteryPoint(9, 9, 0, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.SuperNova, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 1);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stome, 1);
				info.skillLvMemory.AddSkillLv(SkillEnum.BigBurn, 1);
				info.skillLvMemory.AddSkillLv(SkillEnum.Thundar, 1);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Burn, SkillEnum.Comet, SkillEnum.SuperNova, SkillEnum.Explosion, SkillEnum.Stome,SkillEnum.BigBurn,SkillEnum.Thundar });
				break;
			case 4:
				info.SetMasteryPoint(9, 9, 0, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stome, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Thundar, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 1);
				info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 1);
				info.skillLvMemory.AddSkillLv(SkillEnum.SuperNova, 1);
				info.skillLvMemory.AddSkillLv(SkillEnum.BigBurn, 1);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Sprash, SkillEnum.Stome, SkillEnum.Thundar, SkillEnum.Explosion, SkillEnum.Comet,SkillEnum.BigBurn });
				break;

			case 5:
				info.SetMasteryPoint(0, 9, 9, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 5);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 5);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 5);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stome, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Thundar, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Sprash, SkillEnum.Quake, SkillEnum.Stome, SkillEnum.Thundar });
				break;

			case 6:
				info.SetMasteryPoint(9, 0, 9, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Volcano, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Meteo, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 1);
				info.skillLvMemory.AddSkillLv(SkillEnum.SuperNova, 1);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Quake, SkillEnum.Burn, SkillEnum.Volcano, SkillEnum.Meteo, SkillEnum.Comet, SkillEnum.Blast,SkillEnum.SuperNova });
				break;
			case 7:
				info.SetMasteryPoint(9, 0, 9, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.SuperNova, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Volcano, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Meteo, 1);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Burn, SkillEnum.Comet, SkillEnum.SuperNova, SkillEnum.Quake, SkillEnum.Volcano,SkillEnum.Meteo });
				break;
			default:
				break;
		}
		return info;
	}

		private static MovingObjectInfomation CreateLevel26(int randomCount, Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(26);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(9, 9, 9, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.BigBurn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Volcano, 1);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 4);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Burn, SkillEnum.Sprash, SkillEnum.Explosion, SkillEnum.BigBurn,SkillEnum.Volcano,SkillEnum.Quake });
				break;
			case 1:
				info.SetMasteryPoint(9, 9, 9, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash,4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Volcano, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Meteo, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 1);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Quake, SkillEnum.Burn, SkillEnum.Volcano, SkillEnum.Meteo,SkillEnum.Explosion,SkillEnum.Sprash });
				break;

			case 2:
				info.SetMasteryPoint(9, 9, 0, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.BigBurn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stome, 1);
				info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 1);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Burn, SkillEnum.Sprash, SkillEnum.Explosion, SkillEnum.BigBurn,SkillEnum.Stome,SkillEnum.Comet });
				break;
			case 3:
				info.SetMasteryPoint(9, 9, 0, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.SuperNova, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 1);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stome, 1);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Burn, SkillEnum.Comet, SkillEnum.SuperNova,SkillEnum.Explosion,SkillEnum.Stome });
				break;
			case 4:
				info.SetMasteryPoint(9, 9, 0, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stome, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Thundar, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 1);
				info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 1);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Sprash, SkillEnum.Stome, SkillEnum.Thundar,SkillEnum.Explosion,SkillEnum.Comet });
				break;

			case 5:
				info.SetMasteryPoint(0, 9, 9, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stome, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Thundar, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Sprash,SkillEnum.Quake, SkillEnum.Stome, SkillEnum.Thundar });
				break;

			case 6:
				info.SetMasteryPoint(9, 0, 9, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Volcano, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Meteo, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 1);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Quake, SkillEnum.Burn, SkillEnum.Volcano, SkillEnum.Meteo,SkillEnum.Comet,SkillEnum.Blast });
				break;
			case 7:
				info.SetMasteryPoint(9, 0, 9, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.SuperNova, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Volcano, 4);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Burn, SkillEnum.Comet, SkillEnum.SuperNova,SkillEnum.Quake,SkillEnum.Volcano });
				break;
			default:
				break;
		}
		return info;
	}


	private static MovingObjectInfomation CreateLevel25(int randomCount, Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(25);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(9, 9, 9, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.BigBurn, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Burn, SkillEnum.Sprash, SkillEnum.Explosion, SkillEnum.BigBurn });
				break;
			case 2:
				info.SetMasteryPoint(9, 9, 9, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 5);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 5);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Sprash, SkillEnum.Quake });
				break;
			case 4:
				info.SetMasteryPoint(9, 9, 9, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Volcano, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Meteo, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Quake, SkillEnum.Burn, SkillEnum.Volcano, SkillEnum.Meteo });
				break;

			case 1:
				info.SetMasteryPoint(9, 9, 0, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.BigBurn, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Burn, SkillEnum.Sprash, SkillEnum.Explosion, SkillEnum.BigBurn });
				break;
			case 6:
				info.SetMasteryPoint(9, 9, 0, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.SuperNova, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Burn, SkillEnum.Comet, SkillEnum.SuperNova });
				break;
			case 8:
				info.SetMasteryPoint(9, 9, 0, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stome, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Thundar, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Sprash, SkillEnum.Stome, SkillEnum.Thundar });
				break;

			case 3:
				info.SetMasteryPoint(0, 9, 9, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 9);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Sprash, SkillEnum.Quake });
				break;
			case 9:
				info.SetMasteryPoint(0, 9, 9, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stome, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Thundar, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Sprash, SkillEnum.Stome, SkillEnum.Thundar });
				break;
			case 11:
				info.SetMasteryPoint(0, 9, 9, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 5);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 5);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Quake });
				break;

			case 5:
				info.SetMasteryPoint(9, 0, 9, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Volcano, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Meteo, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Quake, SkillEnum.Burn, SkillEnum.Volcano, SkillEnum.Meteo });
				break;
			case 7:
				info.SetMasteryPoint(9, 0, 9, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.SuperNova, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Burn, SkillEnum.Comet, SkillEnum.SuperNova });
				break;
			case 10:
				info.SetMasteryPoint(9, 0, 9, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 5);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 5);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Quake });
				break;
			default:
				break;
		}
		return info;
	}

	private static MovingObjectInfomation CreateLevel24(int randomCount, Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(24);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(9, 9, 6, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.BigBurn, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Burn, SkillEnum.Sprash, SkillEnum.Explosion, SkillEnum.BigBurn });
				break;
			case 1:
				info.SetMasteryPoint(9, 9, 0, 6);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.BigBurn, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Burn, SkillEnum.Sprash, SkillEnum.Explosion, SkillEnum.BigBurn });
				break;
			case 2:
				info.SetMasteryPoint(6, 9, 9, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 5);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 5);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Sprash, SkillEnum.Quake });
				break;
			case 3:
				info.SetMasteryPoint(0, 9, 9, 6);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 9);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Sprash, SkillEnum.Quake });
				break;
			case 4:
				info.SetMasteryPoint(9, 6, 9, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Volcano, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Meteo, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Quake, SkillEnum.Burn, SkillEnum.Volcano, SkillEnum.Meteo });
				break;
			case 5:
				info.SetMasteryPoint(9, 0, 9, 6);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Volcano, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Meteo, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Quake, SkillEnum.Burn, SkillEnum.Volcano, SkillEnum.Meteo });
				break;
			case 6:
				info.SetMasteryPoint(9, 6, 0, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.SuperNova, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Burn, SkillEnum.Comet, SkillEnum.SuperNova });
				break;
			case 7:
				info.SetMasteryPoint(9, 0, 6, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.SuperNova, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Burn, SkillEnum.Comet, SkillEnum.SuperNova });
				break;
			case 8:
				info.SetMasteryPoint(6, 9, 0, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stome, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Thundar, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Sprash, SkillEnum.Stome, SkillEnum.Thundar });
				break;
			case 9:
				info.SetMasteryPoint(0, 9, 6, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stome, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Thundar, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Sprash, SkillEnum.Stome, SkillEnum.Thundar });
				break;
			case 10:
				info.SetMasteryPoint(6, 0, 9, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 5);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 5);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Quake });
				break;
			case 11:
				info.SetMasteryPoint(0, 6, 9, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 5);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 5);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Quake });
				break;
			default:
				break;
		}
		return info;
	}

	private static MovingObjectInfomation CreateLevel23(int randomCount, Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(23);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(9, 9, 3, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.BigBurn, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Burn, SkillEnum.Sprash, SkillEnum.Explosion, SkillEnum.BigBurn });
				break;
			case 1:
				info.SetMasteryPoint(9, 9, 0, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.BigBurn, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Burn, SkillEnum.Sprash, SkillEnum.Explosion, SkillEnum.BigBurn });
				break;
			case 2:
				info.SetMasteryPoint(3, 9, 9, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 5);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 5);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Sprash, SkillEnum.Quake });
				break;
			case 3:
				info.SetMasteryPoint(0, 9, 9, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 5);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 5);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Sprash, SkillEnum.Quake });
				break;
			case 4:
				info.SetMasteryPoint(9, 3, 9, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Volcano, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Meteo, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Quake, SkillEnum.Burn, SkillEnum.Volcano, SkillEnum.Meteo });
				break;
			case 5:
				info.SetMasteryPoint(9, 0, 9, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Volcano, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Meteo, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Quake, SkillEnum.Burn, SkillEnum.Volcano, SkillEnum.Meteo });
				break;
			case 6:
				info.SetMasteryPoint(9, 3, 0, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.SuperNova, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Burn, SkillEnum.Comet, SkillEnum.SuperNova });
				break;
			case 7:
				info.SetMasteryPoint(9, 0, 3, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.SuperNova, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Burn, SkillEnum.Comet, SkillEnum.SuperNova });
				break;
			case 8:
				info.SetMasteryPoint(3, 9, 0, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stome, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Thundar, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Sprash, SkillEnum.Stome, SkillEnum.Thundar });
				break;
			case 9:
				info.SetMasteryPoint(0, 9, 3, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stome, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Thundar, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Sprash, SkillEnum.Stome, SkillEnum.Thundar });
				break;
			case 10:
				info.SetMasteryPoint(3, 0, 9, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 5);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 5);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Quake });
				break;
			case 11:
				info.SetMasteryPoint(0, 3, 9, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 5);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 5);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Quake });
				break;
			default:
				break;
		}
		return info;
	}

	private static MovingObjectInfomation CreateLevel22(int randomCount, Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(22);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(9, 9, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.BigBurn, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Burn, SkillEnum.Sprash, SkillEnum.Explosion, SkillEnum.BigBurn });
				break;
			case 1:
				info.SetMasteryPoint(0, 9, 9, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 5);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 5);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Sprash, SkillEnum.Quake });
				break;
			case 2:
				info.SetMasteryPoint(9, 0, 9, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Volcano, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Meteo, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Quake, SkillEnum.Burn, SkillEnum.Volcano, SkillEnum.Meteo });
				break;
			case 3:
				info.SetMasteryPoint(9, 0, 0, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.SuperNova, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Burn, SkillEnum.Comet, SkillEnum.SuperNova });
				break;
			case 4:
				info.SetMasteryPoint(0, 9, 0, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stome, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Thundar, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Sprash, SkillEnum.Stome, SkillEnum.Thundar });
				break;
			case 5:
				info.SetMasteryPoint(0, 0, 9, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 5);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 5);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Quake });
				break;
			default:
				break;
		}
		return info;
	}

	private static MovingObjectInfomation CreateLevel21(int randomCount, Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(21);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(8, 8, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.BigBurn, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Burn, SkillEnum.Sprash, SkillEnum.Explosion, SkillEnum.BigBurn });
				break;
			case 1:
				info.SetMasteryPoint(0, 8, 8, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 5);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 5);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Sprash, SkillEnum.Quake });
				break;
			case 2:
				info.SetMasteryPoint(8, 0, 8, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Volcano, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Meteo, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Quake, SkillEnum.Burn, SkillEnum.Volcano, SkillEnum.Meteo });
				break;
			case 3:
				info.SetMasteryPoint(8, 0, 0, 8);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.SuperNova, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Burn, SkillEnum.Comet, SkillEnum.SuperNova });
				break;
			case 4:
				info.SetMasteryPoint(0, 8, 0, 8);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stome, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Thundar, 2);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Sprash, SkillEnum.Stome, SkillEnum.Thundar });
				break;
			case 5:
				info.SetMasteryPoint(0, 0, 8, 8);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 5);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 5);
				enemy.EnemySetting(0, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Quake });
				break;
			default:
				break;
		}
		return info;
	}

	private static MovingObjectInfomation CreateLevel20(int randomCount, Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(20);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(8, 8, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.BigBurn, 2);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Burn, SkillEnum.Sprash, SkillEnum.Explosion, SkillEnum.BigBurn });
				break;
			case 1:
				info.SetMasteryPoint(0, 8, 8, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 4);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Sprash, SkillEnum.Quake });
				break;
			case 2:
				info.SetMasteryPoint(8, 0, 8, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Volcano, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Meteo, 2);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Quake, SkillEnum.Burn, SkillEnum.Volcano, SkillEnum.Meteo });
				break;
			case 3:
				info.SetMasteryPoint(8, 0, 0, 8);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.SuperNova, 2);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Burn, SkillEnum.Comet, SkillEnum.SuperNova });
				break;
			case 4:
				info.SetMasteryPoint(0, 8, 0, 8);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stome, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Thundar, 2);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Sprash, SkillEnum.Stome, SkillEnum.Thundar });
				break;
			case 5:
				info.SetMasteryPoint(0, 0, 8, 8);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 4);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Quake });
				break;
			default:
				break;
		}
		return info;
	}

	private static MovingObjectInfomation CreateLevel19(int randomCount, Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(19);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(8, 8, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 1);
				info.skillLvMemory.AddSkillLv(SkillEnum.BigBurn, 1);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Burn, SkillEnum.Sprash, SkillEnum.Explosion,SkillEnum.BigBurn });
				break;
			case 1:
				info.SetMasteryPoint(0, 8, 8, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 3);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Sprash, SkillEnum.Quake });
				break;
			case 2:
				info.SetMasteryPoint(8, 0, 8, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Volcano, 1);
				info.skillLvMemory.AddSkillLv(SkillEnum.Meteo, 1);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Quake, SkillEnum.Burn, SkillEnum.Volcano,SkillEnum.Meteo });
				break;
			case 3:
				info.SetMasteryPoint(8, 0, 0, 8);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 1);
				info.skillLvMemory.AddSkillLv(SkillEnum.SuperNova,1);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Burn, SkillEnum.Comet,SkillEnum.SuperNova });
				break;
			case 4:
				info.SetMasteryPoint(0, 8, 0, 8);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stome, 1);
				info.skillLvMemory.AddSkillLv(SkillEnum.Thundar, 1);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Sprash, SkillEnum.Stome,SkillEnum.Thundar });
				break;
			case 5:
				info.SetMasteryPoint(0, 0, 8, 8);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 3);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Quake });
				break;
			default:
				break;
		}
		return info;
	}

	private static MovingObjectInfomation CreateLevel18(int randomCount, Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(18);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(8, 8, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 1);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Burn, SkillEnum.Sprash, SkillEnum.Explosion });
				break;
			case 1:
				info.SetMasteryPoint(0, 8, 8, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 3);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Sprash, SkillEnum.Quake });
				break;
			case 2:
				info.SetMasteryPoint(8, 0, 8, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Volcano, 1);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Quake, SkillEnum.Burn, SkillEnum.Volcano });
				break;
			case 3:
				info.SetMasteryPoint(8, 0, 0, 8);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 1);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Burn, SkillEnum.Comet });
				break;
			case 4:
				info.SetMasteryPoint(0, 8, 0, 8);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stome, 1);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Sprash, SkillEnum.Stome });
				break;
			case 5:
				info.SetMasteryPoint(0, 0, 8, 8);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 3);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Quake });
				break;
			default:
				break;
		}
		return info;
	}

	private static MovingObjectInfomation CreateLevel17(int randomCount, Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(17);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(7, 7, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 1);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Burn, SkillEnum.Sprash, SkillEnum.Explosion });
				break;
			case 1:
				info.SetMasteryPoint(0, 7, 7, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 3);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Sprash, SkillEnum.Quake });
				break;
			case 2:
				info.SetMasteryPoint(7, 0, 7, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Volcano, 1);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Quake, SkillEnum.Burn, SkillEnum.Volcano });
				break;
			case 3:
				info.SetMasteryPoint(7, 0, 0, 7);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 1);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Burn, SkillEnum.Comet });
				break;
			case 4:
				info.SetMasteryPoint(0, 7, 0, 7);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stome, 1);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Sprash, SkillEnum.Stome });
				break;
			case 5:
				info.SetMasteryPoint(0, 0, 7, 7);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 3);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Quake });
				break;
			default:
				break;
		}
		return info;
	}

	private static MovingObjectInfomation CreateLevel16(int randomCount, Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(16);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(6, 6, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Explosion, 1);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Burn, SkillEnum.Sprash,SkillEnum.Explosion });
				break;
			case 1:
				info.SetMasteryPoint(0, 6, 6, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 3);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Sprash, SkillEnum.Quake });
				break;
			case 2:
				info.SetMasteryPoint(6, 0, 6, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Volcano, 1);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Quake, SkillEnum.Burn,SkillEnum.Volcano });
				break;
			case 3:
				info.SetMasteryPoint(6, 0, 0, 6);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Comet, 1);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Burn,SkillEnum.Comet });
				break;
			case 4:
				info.SetMasteryPoint(0, 6, 0, 6);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stome, 1);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Sprash,SkillEnum.Stome });
				break;
			case 5:
				info.SetMasteryPoint(0, 0, 6, 6);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 3);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Quake });
				break;
			default:
				break;
		}
		return info;
	}


	private static MovingObjectInfomation CreateLevel15(int randomCount, Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(15);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(6, 6, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 2);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Burn, SkillEnum.Sprash });
				break;
			case 1:
				info.SetMasteryPoint(0, 6, 6, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 2);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Sprash, SkillEnum.Quake });
				break;
			case 2:
				info.SetMasteryPoint(6, 0, 6, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 2);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Quake, SkillEnum.Burn });
				break;
			case 3:
				info.SetMasteryPoint(6, 0, 0, 6);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 2);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Burn });
				break;
			case 4:
				info.SetMasteryPoint(0, 6, 0, 6);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 2);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Sprash });
				break;
			case 5:
				info.SetMasteryPoint(0, 0, 6, 6);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 2);
				enemy.EnemySetting(0.1f, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Quake });
				break;
			default:
				break;
		}
		return info;
	}

	private static MovingObjectInfomation CreateLevel14(int randomCount, Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(14);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(5, 5, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 2);
				enemy.EnemySetting(0.2f, new List<SkillEnum> { SkillEnum.Burn ,SkillEnum.Sprash});
				break;
			case 1:
				info.SetMasteryPoint(0,5,5, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 2);
				enemy.EnemySetting(0.2f, new List<SkillEnum> { SkillEnum.Sprash ,SkillEnum.Quake});
				break;
			case 2:
				info.SetMasteryPoint(5, 0, 5, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 2);
				enemy.EnemySetting(0.2f, new List<SkillEnum> { SkillEnum.Quake,SkillEnum.Burn });
				break;
			case 3:
				info.SetMasteryPoint(5, 0, 0,5);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 2);
				enemy.EnemySetting(0.2f, new List<SkillEnum> { SkillEnum.Blast,SkillEnum.Burn });
				break;
			case 4:
				info.SetMasteryPoint(0, 5, 0,5);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 2);
				enemy.EnemySetting(0.2f, new List<SkillEnum> { SkillEnum.Blast,SkillEnum.Sprash });
				break;
			case 5:
				info.SetMasteryPoint(0, 0, 5,5);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 2);
				enemy.EnemySetting(0.2f, new List<SkillEnum> { SkillEnum.Blast,SkillEnum.Quake });
				break;
			default:
				break;
		}
		return info;
	}

	private static MovingObjectInfomation CreateLevel13(int randomCount, Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(13);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(10, 0, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.AttackUp, 1);
				enemy.EnemySetting(0.2f, new List<SkillEnum> { SkillEnum.Burn,SkillEnum.AttackUp });
				break;
			case 1:
				info.SetMasteryPoint(0, 10, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 2);
				info.skillLvMemory.AddSkillLv(SkillEnum.DefenceUp, 1);
				enemy.EnemySetting(0.2f, new List<SkillEnum> { SkillEnum.Sprash,SkillEnum.DefenceUp });
				break;
			case 2:
				info.SetMasteryPoint(0, 0, 10, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 2);
				enemy.EnemySetting(0.2f, new List<SkillEnum> { SkillEnum.Quake });
				break;
			case 3:
				info.SetMasteryPoint(0, 0, 0, 10);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 2);
				enemy.EnemySetting(0.2f, new List<SkillEnum> { SkillEnum.Blast });
				break;
			default:
				break;
		}
		return info;
	}

	private static MovingObjectInfomation CreateLevel12(int randomCount, Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(12);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(10, 0, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 2);
				enemy.EnemySetting(0.2f, new List<SkillEnum> { SkillEnum.Burn});
				break;
			case 1:
				info.SetMasteryPoint(0, 10, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 2);
				enemy.EnemySetting(0.2f, new List<SkillEnum> { SkillEnum.Sprash });
				break;
			case 2:
				info.SetMasteryPoint(0, 0, 10, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 2);
				enemy.EnemySetting(0.2f, new List<SkillEnum> { SkillEnum.Quake });
				break;
			case 3:
				info.SetMasteryPoint(0, 0, 0, 10);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 2);
				enemy.EnemySetting(0.2f, new List<SkillEnum> { SkillEnum.Blast});
				break;
			default:
				break;
		}
		return info;
	}

	private static MovingObjectInfomation CreateLevel11(int randomCount, Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(11);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(10, 0, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Fire, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 2);
				enemy.EnemySetting(0.2f, new List<SkillEnum> { SkillEnum.Burn, SkillEnum.Fire });
				break;
			case 1:
				info.SetMasteryPoint(0, 10, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Water, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 2);
				enemy.EnemySetting(0.2f, new List<SkillEnum> { SkillEnum.Sprash, SkillEnum.Water });
				break;
			case 2:
				info.SetMasteryPoint(0, 0, 10, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stone, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 2);
				enemy.EnemySetting(0.2f, new List<SkillEnum> { SkillEnum.Quake, SkillEnum.Stone });
				break;
			case 3:
				info.SetMasteryPoint(0, 0, 0, 10);
				info.skillLvMemory.AddSkillLv(SkillEnum.Wind, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 2);
				enemy.EnemySetting(0.2f, new List<SkillEnum> { SkillEnum.Blast, SkillEnum.Wind });
				break;
			default:
				break;
		}
		return info;
	}

	private static MovingObjectInfomation CreateLevel10(int randomCount, Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(10);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(10, 0, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Fire, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Burn, 1);
				enemy.EnemySetting(0.2f, new List<SkillEnum> { SkillEnum.Burn,SkillEnum.Fire });
				break;
			case 1:
				info.SetMasteryPoint(0, 10, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Water, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Sprash, 1);
				enemy.EnemySetting(0.2f, new List<SkillEnum> { SkillEnum.Sprash,SkillEnum.Water });
				break;
			case 2:
				info.SetMasteryPoint(0, 0, 10, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stone, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Quake, 1);
				enemy.EnemySetting(0.2f, new List<SkillEnum> { SkillEnum.Quake ,SkillEnum.Stone});
				break;
			case 3:
				info.SetMasteryPoint(0, 0, 0, 10);
				info.skillLvMemory.AddSkillLv(SkillEnum.Wind, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Blast, 1);
				enemy.EnemySetting(0.2f, new List<SkillEnum> { SkillEnum.Blast,SkillEnum.Wind });
				break;
			default:
				break;
		}
		return info;
	}

	private static MovingObjectInfomation CreateLevel9(int randomCount, Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(9);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(10, 0, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Fire, 3);
				enemy.EnemySetting(0.2f, new List<SkillEnum> { SkillEnum.Fire });
				break;
			case 1:
				info.SetMasteryPoint(0, 10, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Water, 3);
				enemy.EnemySetting(0.2f, new List<SkillEnum> { SkillEnum.Water });
				break;
			case 2:
				info.SetMasteryPoint(0, 0, 10, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stone, 3);
				enemy.EnemySetting(0.2f, new List<SkillEnum> { SkillEnum.Stone });
				break;
			case 3:
				info.SetMasteryPoint(0, 0, 0, 10);
				info.skillLvMemory.AddSkillLv(SkillEnum.Wind, 3);
				enemy.EnemySetting(0.2f, new List<SkillEnum> { SkillEnum.Wind });
				break;
			default:
				break;
		}
		return info;
	}

	private static MovingObjectInfomation CreateLevel8(int randomCount, Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(8);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(9, 0, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Fire, 3);
				enemy.EnemySetting(0.3f, new List<SkillEnum> { SkillEnum.Fire });
				break;
			case 1:
				info.SetMasteryPoint(0, 9, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Water, 3);
				enemy.EnemySetting(0.3f, new List<SkillEnum> { SkillEnum.Water });
				break;
			case 2:
				info.SetMasteryPoint(0, 0, 9, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stone, 3);
				enemy.EnemySetting(0.3f, new List<SkillEnum> { SkillEnum.Stone });
				break;
			case 3:
				info.SetMasteryPoint(0, 0, 0, 9);
				info.skillLvMemory.AddSkillLv(SkillEnum.Wind, 3);
				enemy.EnemySetting(0.3f, new List<SkillEnum> { SkillEnum.Wind });
				break;
			default:
				break;
		}
		return info;
	}

	private static MovingObjectInfomation CreateLevel7(int randomCount, Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(7);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(8, 0, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Fire, 3);
				enemy.EnemySetting(0.3f, new List<SkillEnum> { SkillEnum.Fire });
				break;
			case 1:
				info.SetMasteryPoint(0, 8, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Water, 3);
				enemy.EnemySetting(0.3f, new List<SkillEnum> { SkillEnum.Water });
				break;
			case 2:
				info.SetMasteryPoint(0, 0, 8, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stone, 3);
				enemy.EnemySetting(0.3f, new List<SkillEnum> { SkillEnum.Stone });
				break;
			case 3:
				info.SetMasteryPoint(0, 0, 0, 8);
				info.skillLvMemory.AddSkillLv(SkillEnum.Wind, 3);
				enemy.EnemySetting(0.3f, new List<SkillEnum> { SkillEnum.Wind });
				break;
			default:
				break;
		}
		return info;
	}

	private static MovingObjectInfomation CreateLevel6(int randomCount, Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(6);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(8, 0, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Fire, 3);
				enemy.EnemySetting(0.3f, new List<SkillEnum> { SkillEnum.Fire });
				break;
			case 1:
				info.SetMasteryPoint(0, 8, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Water, 3);
				enemy.EnemySetting(0.3f, new List<SkillEnum> { SkillEnum.Water });
				break;
			case 2:
				info.SetMasteryPoint(0, 0, 8, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stone, 3);
				enemy.EnemySetting(0.3f, new List<SkillEnum> { SkillEnum.Stone });
				break;
			case 3:
				info.SetMasteryPoint(0, 0, 0, 8);
				info.skillLvMemory.AddSkillLv(SkillEnum.Wind, 3);
				enemy.EnemySetting(0.3f, new List<SkillEnum> { SkillEnum.Wind });
				break;
			default:
				break;
		}
		return info;
	}

	private static MovingObjectInfomation CreateLevel5(int randomCount, Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(5);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(7, 0, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Fire, 3);
				enemy.EnemySetting(0.3f, new List<SkillEnum> { SkillEnum.Fire });
				break;
			case 1:
				info.SetMasteryPoint(0, 7, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Water, 3);
				enemy.EnemySetting(0.3f, new List<SkillEnum> { SkillEnum.Water });
				break;
			case 2:
				info.SetMasteryPoint(0, 0, 7, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stone, 3);
				enemy.EnemySetting(0.3f, new List<SkillEnum> { SkillEnum.Stone });
				break;
			case 3:
				info.SetMasteryPoint(0, 0, 0, 7);
				info.skillLvMemory.AddSkillLv(SkillEnum.Wind, 3);
				enemy.EnemySetting(0.3f, new List<SkillEnum> { SkillEnum.Wind });
				break;
			default:
				break;
		}
		return info;
	}

	private static MovingObjectInfomation CreateLevel4(int randomCount, Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(4);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(6, 0, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Fire, 2);
				enemy.EnemySetting(0.3f, new List<SkillEnum> { SkillEnum.Fire });
				break;
			case 1:
				info.SetMasteryPoint(0, 6, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Water, 2);
				enemy.EnemySetting(0.3f, new List<SkillEnum> { SkillEnum.Water });
				break;
			case 2:
				info.SetMasteryPoint(0, 0, 6, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stone, 2);
				enemy.EnemySetting(0.3f, new List<SkillEnum> { SkillEnum.Stone });
				break;
			case 3:
				info.SetMasteryPoint(0, 0, 0, 6);
				info.skillLvMemory.AddSkillLv(SkillEnum.Wind, 2);
				enemy.EnemySetting(0.3f, new List<SkillEnum> { SkillEnum.Wind });
				break;
			default:
				break;
		}
		return info;
	}

	private static MovingObjectInfomation CreateLevel3(int randomCount, Enemy enemy, MovingObjectInfomation info)
	{
		info = new MovingObjectInfomation(3);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(4, 0, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Fire, 2);
				enemy.EnemySetting(0.5f, new List<SkillEnum> { SkillEnum.Fire });
				break;
			case 1:
				info.SetMasteryPoint(0, 4, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Water, 2);
				enemy.EnemySetting(0.5f, new List<SkillEnum> { SkillEnum.Water });
				break;
			case 2:
				info.SetMasteryPoint(0, 0, 4, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stone, 2);
				enemy.EnemySetting(0.5f, new List<SkillEnum> { SkillEnum.Stone });
				break;
			case 3:
				info.SetMasteryPoint(0, 0, 0, 4);
				info.skillLvMemory.AddSkillLv(SkillEnum.Wind, 2);
				enemy.EnemySetting(0.5f, new List<SkillEnum> { SkillEnum.Wind });
				break;
			default:
				break;
		}
		return info;
	}

	private static MovingObjectInfomation CreateLevel2(int randomCount,Enemy enemy,MovingObjectInfomation info)
    {
		info = new MovingObjectInfomation(2);
		switch (randomCount)
		{
			case 0:
				info.SetMasteryPoint(3, 0, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Fire, 1);
				enemy.EnemySetting(0.7f, new List<SkillEnum> { SkillEnum.Fire });
				break;
			case 1:
				info.SetMasteryPoint(0, 3, 0, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Water, 1);
				enemy.EnemySetting(0.7f, new List<SkillEnum> { SkillEnum.Water });
				break;
			case 2:
				info.SetMasteryPoint(0, 0, 3, 0);
				info.skillLvMemory.AddSkillLv(SkillEnum.Stone, 1);
				enemy.EnemySetting(0.7f, new List<SkillEnum> { SkillEnum.Stone });
				break;
			case 3:
				info.SetMasteryPoint(0, 0, 0, 3);
				info.skillLvMemory.AddSkillLv(SkillEnum.Wind, 1);
				enemy.EnemySetting(0.7f, new List<SkillEnum> { SkillEnum.Wind });
				break;
			default:
				break;
		}
		return info;
	}

	private static string NameEnemy()
	{

		 return Utils.GetRandom(nameList);
	}
}
