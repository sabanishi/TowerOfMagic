using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Water : Skill
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
            seType = SoundManager.SE_Type.water1;
        }
        else
        {
            seType = SoundManager.SE_Type.water2;
        }
        SoundManager.instance.PlaySE(seType);
        for (int i = 0; i < targets.Count; i++)
        {
            Instantiate(AttackStock.instance.damageEffect, targets[i].transform.position, Quaternion.identity);
        }

        SoundManager.instance.PlaySE(SoundManager.SE_Type.waterSE);
      
        transform.position = client.transform.position;
        client.Mahouzin.SetActive(true);
        client.Mahouzin.GetComponent<SpriteRenderer>().material = MaterialStock.instance.B;
        Hashtable clientHash = new Hashtable();
        clientHash.Add("time", 1.34f);
        clientHash.Add("delay", 0);
        clientHash.Add("oncompletetarget", client.gameObject);
        clientHash.Add("oncomplete", "FinishSkill");
        iTween.MoveTo(client.gameObject, clientHash);

        Hashtable animationHash = new Hashtable();
        animationHash.Add("time", 0.82f);
        animationHash.Add("delay", 0.2);
        animationHash.Add("oncompletetarget", gameObject);
        animationHash.Add("easetype", iTween.EaseType.easeInSine);
        animationHash.Add("onstart", "StartSkill");
        animationHash.Add("oncomplete", "FinishSkill");
        iTween.MoveTo(gameObject, animationHash);
    }

    public override void FinishSkill()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            CalculateDamage(targets[i]);
        }
        base.FinishSkill();
    }

    public static bool CanWaterByEnemy(Enemy enemy)
    {
        MovingObject[,] map = GameManager.instance.MovingObjectMap();
        int x = (int)enemy.transform.position.x;
        int y = (int)enemy.transform.position.y;
        bool canAttack=false;
        if (CanWaterAttack(x + 1, y, map, enemy))
        {
            canAttack = true;
        }
        if (CanWaterAttack(x - 1, y, map, enemy))
        {
            canAttack = true;
        }
        if (CanWaterAttack(x, y + 1, map, enemy))
        {
            canAttack = true;
        }
        if (CanWaterAttack(x, y - 1, map, enemy))
        {
            canAttack = true;
        }
        if (CanWaterAttack(x + 1, y+1, map, enemy))
        {
            canAttack = true;
        }
        if (CanWaterAttack(x - 1, y+1, map, enemy))
        {
            canAttack = true;
        }
        if (CanWaterAttack(x+1, y - 1, map, enemy))
        {
            canAttack = true;
        }
        if (CanWaterAttack(x-1, y - 1, map, enemy))
        {
            canAttack = true;
        }
        if (canAttack)
        {
            enemy.SetPrepareSkillEnum(SkillEnum.Water);
        }
        return canAttack;
    }

    private static bool CanWaterAttack(int x, int y, MovingObject[,] movingObjectMap, Enemy enemy)
    {
        if (movingObjectMap[x, y] != null)
        {
            if (movingObjectMap[x, y].IsPlayer())
            {
                enemy.SetTargetXY(x, y);
                return true;
            }
        }
        return false;
    }

    public static bool WaterByPlayer(Player player)
    {
        int x = (int)player.transform.position.x;
        int y = (int)player.transform.position.y;
        MovingObject[,] movingObjectMap = GameManager.instance.MovingObjectMap();

        List<MovingObject> targets = new List<MovingObject>();
        int count = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (movingObjectMap[x+i, y+j] != null)
                {
                    if (!movingObjectMap[x+i, y+j].IsPlayer())
                    {
                        targets.Add(movingObjectMap[x+i, y+j]);
                        count++;
                    }
                }
            }
        }
        if (count >= 1)
        {
            SetSkill(player, targets, true);
            return true;
        }
      
        DialogManager.instance.AttackFormat(player.GetNowMovingObjectInfo().Name, "ウォーター");
        return false;
    }

    public static void WaterByEnemy(Enemy client, MovingObject target)
    {
        SetSkill(client, new List<MovingObject> { target }, false);
    }

    public static void SetSkill(MovingObject client, List<MovingObject> targets, bool isPlayer)
    {
        DialogManager.instance.AttackFormat(client.GetNowMovingObjectInfo().Name, "ウォーター");
       
        Skill skill = Instantiate(AttackStock.instance.water).GetComponent<Skill>();
        skill.skillEnum = SkillEnum.Water;
        skill.SetClient(client);
        skill.SetTargets(targets);
        skill.AttackDeal();

       
    }
}

