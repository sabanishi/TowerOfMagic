using System;
using System.Collections;
using UnityEngine;

public class Warp: Skill
{
    public override void Start()
    {
        base.Start();

    }
    public override void Attack()
    {
        SoundManager.SE_Type seType;
        if (client.IsPlayer())
        {
            seType = SoundManager.SE_Type.warp1;
        }
        else
        {
            seType = SoundManager.SE_Type.heal2;
        }

        SoundManager.instance.PlaySE(seType);

        SoundManager.instance.PlaySE(SoundManager.SE_Type.warp);
        transform.position = client.transform.position;
        client.Mahouzin.SetActive(true);
        client.Mahouzin.GetComponent<SpriteRenderer>().material = MaterialStock.instance.YandG;
        client.StartCoroutine("WarpDeal");
     
        Hashtable animationHash = new Hashtable();
        animationHash.Add("time", 0.7f);
        animationHash.Add("delay", 0.2f);
        animationHash.Add("oncompletetarget", gameObject);
        animationHash.Add("onstart", "StartSkill");
        animationHash.Add("oncomplete", "FinishSkill");
        iTween.MoveTo(gameObject, animationHash);
    }

   

    public override void FinishSkill()
    {
        CalculateDamage(null);
        base.FinishSkill();
    }

    protected override void CalculateDamage(MovingObject _target)
    {
        FloorChip[,] floorMap = GameManager.instance.FloorMap();
        MovingObject[,] objectMap = GameManager.instance.MovingObjectMap();
        int targetX=0, targetY=0;
        for(int x = 0; x < floorMap.GetLength(0); x++)
        {
            for(int y = 0; y < floorMap.GetLength(1); y++)
            {
                if (floorMap[x, y].FloorEnum == FloorEnum.STRAIRS)
                {
                    targetX = x;
                    targetY = y;
                    break;
                }
            }
        }
        bool canWarp = false;
        int count = 0;
        while (!canWarp)
        {
            count++;
            for(int x = -count; x <= count; x++)
            {
                for(int y = -count; y <= count; y++)
                {
                    if (x != 0 || y != 0)
                    {
                        if (CanWarp(targetX + x, targetY + y, floorMap, objectMap))
                        {
                            objectMap[(int)(client.transform.position.x),(int)(client.transform.position.y)] = null;
                            client.transform.position = new Vector3(targetX + x, targetY + y,client.transform.position.z);
                            objectMap[targetX+x, targetY+y] = client;
                            return;
                        }
                    }
                }
            }
        }

    }

    private bool CanWarp(int x,int y,FloorChip[,] floorMap,MovingObject[,] objectMap)
    {
        if (floorMap[x, y].FloorEnum == FloorEnum.Wall)
        {
            return false;
        }
        if (objectMap[x, y] != null)
        {
            return false;
        }

        return true;
    }


    public static bool CanWarpByEnemy(Enemy enemy)
    {
        MovingObject[,] map = GameManager.instance.MovingObjectMap();
        bool canAttack = false;
        int x = (int)enemy.transform.position.x;
        int y = (int)enemy.transform.position.y;
        for (int posX = -3; posX <= 3; posX++)
        {
            for (int posY = -3; posY <= 3; posY++)
            {
                if (CanHealAttack(x + posX, y + posY, map, enemy))
                {
                    canAttack = true;
                }
            }
        }

        if (canAttack)
        {
            enemy.SetPrepareSkillEnum(SkillEnum.Heal);
        }
        return canAttack;
    }

    private static bool CanHealAttack(int x, int y, MovingObject[,] movingObjectMap, Enemy enemy)
    {
        if (x >= 0 && x < movingObjectMap.GetLength(0) && y >= 0 && y < movingObjectMap.GetLength(1))
        {
            if (movingObjectMap[x, y] != null)
            {
                if (movingObjectMap[x, y].IsPlayer())
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static bool WarpByPlayer(Player player)
    {
        SetSkill(player,true);
        return true;
    }

    public static void CraftByEnemy(Enemy client)
    {
        SetSkill(client, false);
    }

    public static void SetSkill(MovingObject client, bool isPlayer)
    {
        DialogManager.instance.AttackFormat(client.GetNowMovingObjectInfo().Name, "ワープ");
        Skill skill = Instantiate(AttackStock.instance.warp).GetComponent<Skill>();
        skill.skillEnum = SkillEnum.Warp;
        skill.SetClient(client);
        skill.AttackDeal();
    }
}