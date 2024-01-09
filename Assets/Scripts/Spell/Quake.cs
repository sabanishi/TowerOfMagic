using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Quake : Skill
{
    [SerializeField] private List<GameObject> Nodes = new List<GameObject>();
    public override void Start()
    {
        foreach(var obj in Nodes)
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
            seType = SoundManager.SE_Type.quake2;
        }
        else
        {
            seType = SoundManager.SE_Type.quake1;
        }
        SoundManager.instance.PlaySE(seType);

        for (int i = 0; i < targets.Count; i++)
        {
            Instantiate(AttackStock.instance.damageEffect, targets[i].transform.position, Quaternion.identity);
        }

        SoundManager.instance.PlaySE(SoundManager.SE_Type.stoneSE);

        transform.position = client.transform.position;
        client.Mahouzin.SetActive(true);
        client.Mahouzin.GetComponent<SpriteRenderer>().material = MaterialStock.instance.Y;
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

    public static bool CanQuakeByEnemy(Enemy enemy)
    {
        MovingObject[,] map = GameManager.instance.MovingObjectMap();
        int x = (int)enemy.transform.position.x;
        int y = (int)enemy.transform.position.y;
        bool canAttack = false;
        if (CanQuakeAttack(x + 1, y, map, enemy))
        {
            canAttack = true;
        }
        if (CanQuakeAttack(x - 1, y, map, enemy))
        {
            canAttack = true;
        }
        if (CanQuakeAttack(x, y + 1, map, enemy))
        {
            canAttack = true;
        }
        if (CanQuakeAttack(x, y - 1, map, enemy))
        {
            canAttack = true;
        }
        if (CanQuakeAttack(x + 1, y + 1, map, enemy))
        {
            canAttack = true;
        }
        if (CanQuakeAttack(x - 1, y + 1, map, enemy))
        {
            canAttack = true;
        }
        if (CanQuakeAttack(x + 1, y - 1, map, enemy))
        {
            canAttack = true;
        }
        if (CanQuakeAttack(x - 1, y - 1, map, enemy))
        {
            canAttack = true;
        }
        if (canAttack)
        {
            enemy.SetPrepareSkillEnum(SkillEnum.Quake);
        }
        return canAttack;
    }

    private static bool CanQuakeAttack(int x, int y, MovingObject[,] movingObjectMap, Enemy enemy)
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

    public static bool QuakeByPlayer(Player player)
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
        if (count >= 1)
        {
            SetSkill(player, targets, true);
            return true;
        }

        DialogManager.instance.AttackFormat(player.GetNowMovingObjectInfo().Name, "クエイク");
        return false;
    }

    public static void QuakeByEnemy(Enemy client, MovingObject target)
    {
        SetSkill(client, new List<MovingObject> { target }, false);
    }

    public static void SetSkill(MovingObject client, List<MovingObject> targets, bool isPlayer)
    {
        DialogManager.instance.AttackFormat(client.GetNowMovingObjectInfo().Name, "クエイク");
       
        Skill skill = Instantiate(AttackStock.instance.quake).GetComponent<Skill>();
        skill.skillEnum = SkillEnum.Quake;
        skill.SetClient(client);
        skill.SetTargets(targets);
        skill.AttackDeal();

        
    }
}


