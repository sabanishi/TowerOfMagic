using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion: Skill
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
            seType = SoundManager.SE_Type.explosion1;
        }
        else
        {
            seType = SoundManager.SE_Type.explosion2;
        }
        SoundManager.instance.PlaySE(seType);
        for (int i = 0; i < targets.Count; i++)
        {
            Instantiate(AttackStock.instance.damageEffect, targets[i].transform.position, Quaternion.identity);
        }

        SoundManager.instance.PlaySE(SoundManager.SE_Type.fireSE);
      
        transform.position = client.transform.position;
        client.Mahouzin.SetActive(true);
        client.Mahouzin.GetComponent<SpriteRenderer>().material = MaterialStock.instance.RandB;
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

    public static bool CanExplosionByEnemy(Enemy enemy)
    {
        MovingObject[,] map = GameManager.instance.MovingObjectMap();
        int x = (int)enemy.transform.position.x;
        int y = (int)enemy.transform.position.y;
        bool canAttack=false;
        if (CanExplosionAttack(x + 1, y, map, enemy))
        {
            canAttack = true;
        }
        if (CanExplosionAttack(x - 1, y, map, enemy))
        {
            canAttack = true;
        }
        if (CanExplosionAttack(x, y + 1, map, enemy))
        {
            canAttack = true;
        }
        if (CanExplosionAttack(x, y - 1, map, enemy))
        {
            canAttack = true;
        }
        if (CanExplosionAttack(x + 1, y+1, map, enemy))
        {
            canAttack = true;
        }
        if (CanExplosionAttack(x - 1, y+1, map, enemy))
        {
            canAttack = true;
        }
        if (CanExplosionAttack(x+1, y - 1, map, enemy))
        {
            canAttack = true;
        }
        if (CanExplosionAttack(x-1, y - 1, map, enemy))
        {
            canAttack = true;
        }
        if (canAttack)
        {
            enemy.SetPrepareSkillEnum(SkillEnum.Explosion);
        }
        return canAttack;
    }

    private static bool CanExplosionAttack(int x, int y, MovingObject[,] movingObjectMap, Enemy enemy)
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

    public static bool ExplsoionByPlayer(Player player)
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
      
        DialogManager.instance.AttackFormat(player.GetNowMovingObjectInfo().Name, "エクスプロージョン");
        return false;
    }

    public static void ExplosionByEnemy(Enemy client, MovingObject target)
    {
        SetSkill(client, new List<MovingObject> { target }, false);
    }

    public static void SetSkill(MovingObject client, List<MovingObject> targets, bool isPlayer)
    {
        DialogManager.instance.AttackFormat(client.GetNowMovingObjectInfo().Name, "エクスプロージョン");
       
        Explosion skill = Instantiate(AttackStock.instance.explosion).GetComponent<Explosion>();
        skill.skillEnum = SkillEnum.Explosion;
        skill.SetClient(client);
        skill.SetTargets(targets);
        skill.AttackDeal();
        foreach(var target in targets)
        {
            GameObject node = Instantiate(skill.Prefab, target.transform.position, Quaternion.identity);
            skill.Nodes.Add(node);
            node.transform.parent = skill.transform;
        }
    }
}

