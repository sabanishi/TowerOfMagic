using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Skill
{
    [SerializeField]private GameObject UpRight, UpLeft, DownRight, DownLeft;

    public override void Start()
    { 
        UpRight.GetComponent<Animator>().enabled = false;
        UpRight.GetComponent<SpriteRenderer>().enabled = false;
        DownRight.GetComponent<Animator>().enabled = false;
        DownRight.GetComponent<SpriteRenderer>().enabled = false;
        UpLeft.GetComponent<Animator>().enabled = false;
        UpLeft.GetComponent<SpriteRenderer>().enabled = false;
        DownLeft.GetComponent<Animator>().enabled = false;
        DownLeft.GetComponent<SpriteRenderer>().enabled = false;
    }

    public override void StartSkill()
    {
        UpRight.GetComponent<Animator>().enabled = true;
        UpRight.GetComponent<SpriteRenderer>().enabled = true;
        DownRight.GetComponent<Animator>().enabled = true;
        DownRight.GetComponent<SpriteRenderer>().enabled = true;
        UpLeft.GetComponent<Animator>().enabled = true;
        UpLeft.GetComponent<SpriteRenderer>().enabled = true;
        DownLeft.GetComponent<Animator>().enabled = true;
        DownLeft.GetComponent<SpriteRenderer>().enabled = true;
    }


    public override void Attack()
    {
        SoundManager.SE_Type seType;
        if (client.IsPlayer())
        {
            seType = SoundManager.SE_Type.stone1;
        }
        else
        {
            seType = SoundManager.SE_Type.stone2;
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
        clientHash.Add("time", 1.43f);
        clientHash.Add("delay", 0);
        clientHash.Add("oncompletetarget", client.gameObject);
        clientHash.Add("oncomplete", "FinishSkill");
        iTween.MoveTo(client.gameObject, clientHash);

        Hashtable animationHash = new Hashtable();
        animationHash.Add("time", 0.93f);
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

    public static bool CanStoneByEnemy(Enemy enemy)
    {
        MovingObject[,] map = GameManager.instance.MovingObjectMap();
        int x = (int)enemy.transform.position.x;
        int y = (int)enemy.transform.position.y;
        bool canAttack = false;
        
        if (CanStoneAttack(x + 1, y + 1, map, enemy))
        {
            canAttack = true;
        }
        if (CanStoneAttack(x - 1, y + 1, map, enemy))
        {
            canAttack = true;
        }
        if (CanStoneAttack(x + 1, y - 1, map, enemy))
        {
            canAttack = true;
        }
        if (CanStoneAttack(x - 1, y - 1, map, enemy))
        {
            canAttack = true;
        }
        if (canAttack)
        {
            enemy.SetPrepareSkillEnum(SkillEnum.Stone);
        }
        return canAttack;
    }

    private static bool CanStoneAttack(int x, int y, MovingObject[,] movingObjectMap, Enemy enemy)
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

    public static bool StoneByPlayer(Player player)
    {
        int x = (int)player.transform.position.x;
        int y = (int)player.transform.position.y;
        MovingObject[,] movingObjectMap = GameManager.instance.MovingObjectMap();

        List<MovingObject> targets = new List<MovingObject>();
        int count = 0;
        if (movingObjectMap[x + 1, y + 1] != null)
        {
            if (!movingObjectMap[x + 1, y + 1].IsPlayer())
            {
                targets.Add(movingObjectMap[x + 1, y + 1]);
                count++;
            }
        }
        if (movingObjectMap[x + 1, y -1] != null)
        {
            if (!movingObjectMap[x + 1, y -1].IsPlayer())
            {
                targets.Add(movingObjectMap[x + 1, y -1]);
                count++;
            }
        }
        if (movingObjectMap[x-1, y + 1] != null)
        {
            if (!movingObjectMap[x -1, y + 1].IsPlayer())
            {
                targets.Add(movingObjectMap[x -1, y + 1]);
                count++;
            }
        }
        if (movingObjectMap[x -1, y -1] != null)
        {
            if (!movingObjectMap[x -1, y -1].IsPlayer())
            {
                targets.Add(movingObjectMap[x -1, y -1]);
                count++;
            }
        }

        if (count >= 1)
        {
            SetSkill(player, targets, true);
            return true;
        }

        DialogManager.instance.AttackFormat(player.GetNowMovingObjectInfo().Name, "ストーン");
        return false;
    }

    public static void StoneByEnemy(Enemy client, MovingObject target)
    {
        SetSkill(client, new List<MovingObject> { target }, false);
    }

    public static void SetSkill(MovingObject client, List<MovingObject> targets, bool isPlayer)
    {
        DialogManager.instance.AttackFormat(client.GetNowMovingObjectInfo().Name, "ストーン");
     
        Skill skill = Instantiate(AttackStock.instance.stone).GetComponent<Skill>();
        skill.skillEnum = SkillEnum.Stone;
        skill.SetClient(client);
        skill.SetTargets(targets);
        skill.AttackDeal();

       
    }
}

