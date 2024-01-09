using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Meteo: Skill
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
            seType = SoundManager.SE_Type.meteo1;
        }
        else
        {
            seType = SoundManager.SE_Type.meteo2;
        }
        SoundManager.instance.PlaySE(seType);
        for (int i = 0; i < targets.Count; i++)
        {
            Instantiate(AttackStock.instance.damageEffect, targets[i].transform.position, Quaternion.identity);
        }

        SoundManager.instance.PlaySE(SoundManager.SE_Type.stoneSE);
        Vector3 posion =client.transform.position;
        posion.y += 0.6f;
        transform.position = posion;

        client.Mahouzin.SetActive(true);
        client.Mahouzin.GetComponent<SpriteRenderer>().material = MaterialStock.instance.RandY;
        Hashtable clientHash = new Hashtable();
        clientHash.Add("time", 1.14f);
        clientHash.Add("delay", 0);
        clientHash.Add("oncompletetarget", client.gameObject);
        clientHash.Add("oncomplete", "FinishSkill");
        iTween.MoveTo(client.gameObject, clientHash);

        Hashtable animationHash = new Hashtable();
        animationHash.Add("time", 0.62f);
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

    public static bool CanMeteoByEnemy(Enemy enemy)
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
            enemy.SetPrepareSkillEnum(SkillEnum.Meteo);
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

    public static bool MeteoByPlayer(Player player)
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
      
        DialogManager.instance.AttackFormat(player.GetNowMovingObjectInfo().Name, "メテオ");
        return false;
    }

    public static void MeteoByEnemy(Enemy client, MovingObject target)
    {
        SetSkill(client, new List<MovingObject> { target }, false);
    }

    public static void SetSkill(MovingObject client, List<MovingObject> targets, bool isPlayer)
    {
        DialogManager.instance.AttackFormat(client.GetNowMovingObjectInfo().Name, "メテオ");
       
        Skill skill = Instantiate(AttackStock.instance.meteo).GetComponent<Skill>();
        skill.skillEnum = SkillEnum.Meteo;
        skill.SetClient(client);
        skill.SetTargets(targets);
        skill.AttackDeal();

       
    }
}

