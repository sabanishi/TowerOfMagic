using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Freeze : Skill
{
    [SerializeField] private List<GameObject> Nodes = new List<GameObject>();
    private bool[] ActiveList = new bool[8];

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
        for(int i = 0; i < Nodes.Count; i++)
        {
            if (ActiveList[i])
            {
                GameObject obj = Nodes[i];
                obj.GetComponent<Animator>().enabled = true;
                obj.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    protected override void CalculateDamage(MovingObject _target)
    {
        foreach(var target in targets)
        {
            if (target.IsCondition(PlayerCondition.SpedUp))
            {
                target.CureCondition(PlayerCondition.SpedUp);
                DialogManager.instance.SetText("<color=#ffff00>" + target.GetNowMovingObjectInfo().Name + "</color>　の素早さが元に戻った");
            }
            else
            {
                if (!target.IsCondition(PlayerCondition.SpeedDown))
                {
                    target.FragForSpeedDown = false;
                }
                target.AddCondition(PlayerCondition.SpeedDown);
                DialogManager.instance.SetText("<color=#ffff00>" + target.GetNowMovingObjectInfo().Name + "</color>　の素早さが下がった");
            }
        }
    }

    public override void Attack()
    {
        SoundManager.SE_Type seType;
        if (client.IsPlayer())
        {
            seType = SoundManager.SE_Type.freeze1;
        }
        else
        {
            seType = SoundManager.SE_Type.freeze2;
        }
        SoundManager.instance.PlaySE(seType);

        SoundManager.instance.PlaySE(SoundManager.SE_Type.ice);

        for(int i = 0; i < targets.Count; i++)
        {
            Instantiate(AttackStock.instance.damageEffect, targets[i].transform.position, Quaternion.identity);
        }

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

    public static bool CanFreezeByEnemy(Enemy enemy)
    {
        MovingObject[,] map = GameManager.instance.MovingObjectMap();
        int x = (int)enemy.transform.position.x;
        int y = (int)enemy.transform.position.y;
        bool canAttack = false;
        if (CanFreezeAttack(x + 1, y, map, enemy))
        {
            canAttack = true;
            
        }
        if (CanFreezeAttack(x - 1, y, map, enemy))
        {
            canAttack = true;
        }
        if (CanFreezeAttack(x, y + 1, map, enemy))
        {
            canAttack = true;
        }
        if (CanFreezeAttack(x, y - 1, map, enemy))
        {
            canAttack = true;
        }
        if (CanFreezeAttack(x + 1, y + 1, map, enemy))
        {
            canAttack = true;
        }
        if (CanFreezeAttack(x - 1, y + 1, map, enemy))
        {
            canAttack = true;
        }
        if (CanFreezeAttack(x + 1, y - 1, map, enemy))
        {
            canAttack = true;
        }
        if (CanFreezeAttack(x - 1, y - 1, map, enemy))
        {
            canAttack = true;
        }
        if (canAttack)
        {
            enemy.SetPrepareSkillEnum(SkillEnum.Freeze);
        }
        return canAttack;
    }

    private static bool CanFreezeAttack(int x, int y, MovingObject[,] movingObjectMap, Enemy enemy)
    {
        if (movingObjectMap[x, y] != null)
        {
            if (movingObjectMap[x, y].IsPlayer())
            {
                if (!movingObjectMap[x, y].IsCondition(PlayerCondition.SpeedDown))
                {
                    enemy.SetTargetXY(x, y);
                    return true;
                }
            
            }
        }
        return false;
    }

    public static bool FreezeByPlayer(Player player)
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
            SetSkill(player, targets);
            return true;
        }

        DialogManager.instance.AttackFormat(player.GetNowMovingObjectInfo().Name, "フリーズ");
        return false;
    }

    public static void FreezeByEnemy(Enemy client, MovingObject target)
    {
        SetSkill(client, new List<MovingObject> { target });
    }

    public static void SetSkill(MovingObject client, List<MovingObject> targets)
    {
        DialogManager.instance.AttackFormat(client.GetNowMovingObjectInfo().Name, "フリーズ");
      
        Freeze skill = Instantiate(AttackStock.instance.freeze).GetComponent<Freeze>();
        skill.skillEnum = SkillEnum.Freeze;
        skill.SetClient(client);
        skill.SetTargets(targets);
        skill.AttackDeal();
        Transform ctf = client.transform;

        for(int i = 0; i < skill.ActiveList.Length; i++)
        {
            skill.ActiveList[i] = false;
        }

        foreach(var target in targets)
        {
            Transform tf = target.transform;
            int x = -(int)(ctf.position.x - tf.position.x);
            int y = -(int)(ctf.position.y - tf.position.y);
            if (y == -1)
            {
                if (x == -1)
                {
                    skill.ActiveList[0] = true;
                }else if (x == 0)
                {
                    skill.ActiveList[7] = true;
                }
               else
                {
                    skill.ActiveList[6] = true;
                }
            }else if (y == 0)
            {
                if (x == -1)
                {
                    skill.ActiveList[1] = true;
                }
                else if (x == 0)
                {

                }
                else
                {
                    skill.ActiveList[5] = true;
                }
            }
            else
            {
                if (x == -1)
                {
                    skill.ActiveList[2] = true;
                }
                else if (x == 0)
                {
                    skill.ActiveList[3] = true;
                }
                else
                {
                    skill.ActiveList[4] = true;
                }
            }
        }
    }
}

