using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thundar:Skill
{
    [SerializeField] private GameObject Prefab;
    private List<GameObject> Nodes = new List<GameObject>();

    public override void Start()
    {
        foreach (var obj in Nodes)
        {
            obj.GetComponent<Animator>().enabled = false;
            obj.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public override void StartSkill()
    {
        foreach (var obj in Nodes)
        {
            obj.GetComponent<Animator>().enabled = true;
            obj.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public override void Attack()
    {
        SoundManager.SE_Type seType;
        if (client.IsPlayer())
        {
            seType = SoundManager.SE_Type.thundar1;
        }
        else
        {
            seType = SoundManager.SE_Type.thudar2;
        }
        SoundManager.instance.PlaySE(seType);
        for (int i = 0; i < targets.Count; i++)
        {
            Instantiate(AttackStock.instance.damageEffect, targets[i].transform.position, Quaternion.identity);
        }

        SoundManager.instance.PlaySE(SoundManager.SE_Type.thundarSE);

        transform.position = client.transform.position;
        client.Mahouzin.SetActive(true);
        client.Mahouzin.GetComponent<SpriteRenderer>().material = MaterialStock.instance.BandG;
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

    public static bool CanThundarByEnemy(Enemy enemy)
    {
        MovingObject[,] map = GameManager.instance.MovingObjectMap();
        bool canAttack = false;
        int x = (int)enemy.transform.position.x;
        int y = (int)enemy.transform.position.y;
        for (int posX = -5; posX <= 5; posX++)
        {
            for (int posY = -5; posY <= 5; posY++)
            {
                if (CanThundarAttack(x + posX, y + posY, map, enemy))
                {
                    canAttack = true;
                }
            }
        }


        if (canAttack)
        {
            enemy.SetPrepareSkillEnum(SkillEnum.Thundar);
        }
        return canAttack;
    }

    private static bool CanThundarAttack(int x, int y, MovingObject[,] movingObjectMap, Enemy enemy)
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

    public static bool ThundarByPlayer(Player player)
    {
        int x = (int)player.transform.position.x;
        int y = (int)player.transform.position.y;
        MovingObject[,] movingObjectMap = GameManager.instance.MovingObjectMap();
        List<MovingObject> targets = new List<MovingObject>();
        int count = 0;
        for (int i = -5; i <= 5; i++)
        {
            for (int j = -5; j <= 5; j++)
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

        DialogManager.instance.AttackFormat(player.GetNowMovingObjectInfo().Name, "サンダー");
        return false;
    }

    public static void ThundarByEnemy(Enemy client, MovingObject target)
    {
        SetSkill(client, new List<MovingObject> { target });
    }

    public static void SetSkill(MovingObject client, List<MovingObject> targets)
    {
        DialogManager.instance.AttackFormat(client.GetNowMovingObjectInfo().Name, "サンダー");

        Thundar skill = Instantiate(AttackStock.instance.thudar).GetComponent<Thundar>();
        skill.skillEnum = SkillEnum.Thundar;
        skill.SetClient(client);
        skill.SetTargets(targets);
        skill.AttackDeal();

        foreach (var target in targets)
        {
            GameObject node = Instantiate(skill.Prefab, target.transform.position, Quaternion.identity);
            skill.Nodes.Add(node);
            node.transform.parent = skill.transform;
        }
    }
}
