using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stome:Skill
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
            seType = SoundManager.SE_Type.stome1;
        }
        else
        {
            seType = SoundManager.SE_Type.stome2;
        }
        SoundManager.instance.PlaySE(seType);
        for (int i = 0; i < targets.Count; i++)
        {
            Instantiate(AttackStock.instance.damageEffect, targets[i].transform.position, Quaternion.identity);
        }

        SoundManager.instance.PlaySE(SoundManager.SE_Type.windSE);

        transform.position = client.transform.position;
        client.Mahouzin.SetActive(true);
        client.Mahouzin.GetComponent<SpriteRenderer>().material = MaterialStock.instance.BandG;
        Hashtable clientHash = new Hashtable();
        clientHash.Add("time", 1.12f);
        clientHash.Add("delay", 0);
        clientHash.Add("oncompletetarget", client.gameObject);
        clientHash.Add("oncomplete", "FinishSkill");
        iTween.MoveTo(client.gameObject, clientHash);

        Hashtable animationHash = new Hashtable();
        animationHash.Add("time", 0.62f);
        animationHash.Add("delay", 0.2);
        animationHash.Add("oncompletetarget", gameObject);
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

    public static bool CanStomeByEnemy(Enemy enemy)
    {
        MovingObject[,] map = GameManager.instance.MovingObjectMap();
        int x = (int)enemy.transform.position.x;
        int y = (int)enemy.transform.position.y;
        bool canAttack = false;
        for (int posX = -3; posX <= 3; posX++)
        {
            for (int posY = -3; posY <= 3; posY++)
            {
                if (CanSprashAttack(x + posX, y + posY, map, enemy))
                {
                    canAttack = true;
                }
            }
        }

        if (canAttack)
        {
            enemy.SetPrepareSkillEnum(SkillEnum.Stome);
        }
        return canAttack;
    }

    private static bool CanSprashAttack(int x, int y, MovingObject[,] movingObjectMap, Enemy enemy)
    {
        if (x >= 0 && x < movingObjectMap.GetLength(0) && y >= 0 && y < movingObjectMap.GetLength(1))
        {
            if (movingObjectMap[x, y] != null)
            {
                if (movingObjectMap[x, y].IsPlayer())
                {
                    enemy.SetTargetXY(x, y);
                    return true;
                }
            }
        }
        return false;
    }

    public static bool StomeByPlayer(Player player)
    {
        int x = (int)player.transform.position.x;
        int y = (int)player.transform.position.y;
        MovingObject[,] movingObjectMap = GameManager.instance.MovingObjectMap();

        List<MovingObject> targets = new List<MovingObject>();
        int count = 0;
        for (int i = -3; i <= 3; i++)
        {
            for (int j = -3; j <= 3; j++)
            {
                if (x + i >= 0 && x + i < movingObjectMap.GetLength(0) && y + j >= 0 && y + j < movingObjectMap.GetLength(1))
                {
                    if (movingObjectMap[x + i, y + j] != null)
                    {
                        if (!movingObjectMap[x + i, y + j].IsPlayer())
                        {
                            targets.Add(movingObjectMap[x + i, y + j]);
                            count++;
                        }
                    }
                }
            }
        }
        if (count >= 1)
        {
            SetSkill(player, targets);
            return true;
        }

        DialogManager.instance.AttackFormat(player.GetNowMovingObjectInfo().Name, "ストーム");
        return false;
    }

    public static void StomeByEnemy(Enemy client, MovingObject target)
    {
        SetSkill(client, new List<MovingObject> { target });
    }

    public static void SetSkill(MovingObject client, List<MovingObject> targets)
    {
        DialogManager.instance.AttackFormat(client.GetNowMovingObjectInfo().Name, "ストーム");

        Skill skill = Instantiate(AttackStock.instance.stome).GetComponent<Skill>();
        skill.skillEnum = SkillEnum.Stome;
        skill.SetClient(client);
        skill.SetTargets(targets);
        skill.AttackDeal();
    }
}


