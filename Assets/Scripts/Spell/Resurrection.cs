using System;
using System.Collections;
using UnityEngine;

public class Resurrection : Skill
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
            seType = SoundManager.SE_Type.resurrection1;
        }
        else
        {
            seType = SoundManager.SE_Type.resurrection2;
        }

        SoundManager.instance.PlaySE(seType);

        SoundManager.instance.PlaySE(SoundManager.SE_Type.healEffect);
        transform.position = client.transform.position;
        client.Mahouzin.SetActive(true);
        client.Mahouzin.GetComponent<SpriteRenderer>().material = MaterialStock.instance.BandY;
        Hashtable clientHash = new Hashtable();
        clientHash.Add("time", 1.2f);
        clientHash.Add("delay", 0);
        clientHash.Add("oncompletetarget", client.gameObject);
        clientHash.Add("oncomplete", "FinishSkill");
        iTween.MoveTo(client.gameObject, clientHash);

        Hashtable animationHash = new Hashtable();
        animationHash.Add("time", 0.7f);
        animationHash.Add("delay", 0.2);
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
        int level;
        if (!client.IsPlayer() || !((Player)client).IsItemSkill())
        {
            level = client.GetNowMovingObjectInfo().skillLvMemory.GetSkillLV(SkillEnum.Resurrection);
        }
        else
        {
            level = ((Player)client).itemSkillLV;
        }
        float healProperity = SkillConstractParent.ReturnScaleFromSkillEnum(SkillEnum.Resurrection, level);
        if (GameManager.instance.GetPlayerInfo1().Hp <=0)
        {
            GameManager.instance.GetPlayerInfo1().Hp = (int)(GameManager.instance.GetPlayerInfo1().MaxHp * healProperity);
            DialogManager.instance.SetText("<color=#ffff00>" + GameManager.instance.GetPlayerInfo1().Name + "</color>　は復活した!");
            if (GameManager.instance.GetPlayerInfo1().Hp > GameManager.instance.GetPlayerInfo1().MaxHp)
            {
                GameManager.instance.GetPlayerInfo1().Hp = GameManager.instance.GetPlayerInfo1().MaxHp;
            }
        }
        if (GameManager.instance.GetPlayerInfo2().Hp <=0)
        {
            GameManager.instance.GetPlayerInfo2().Hp = (int)(GameManager.instance.GetPlayerInfo2().MaxHp * healProperity);
            DialogManager.instance.SetText("<color=#ffff00>" + GameManager.instance.GetPlayerInfo2().Name + "</color>　は復活した!");
            if (GameManager.instance.GetPlayerInfo2().Hp > GameManager.instance.GetPlayerInfo2().MaxHp)
            {
                GameManager.instance.GetPlayerInfo2().Hp = GameManager.instance.GetPlayerInfo2().MaxHp;
            }
        }
        if (GameManager.instance.GetPlayerInfo3().Hp <=0)
        {
            GameManager.instance.GetPlayerInfo3().Hp = (int)(GameManager.instance.GetPlayerInfo3().MaxHp * healProperity);
            DialogManager.instance.SetText("<color=#ffff00>" + GameManager.instance.GetPlayerInfo3().Name + "</color>　は復活した!");
            if (GameManager.instance.GetPlayerInfo3().Hp > GameManager.instance.GetPlayerInfo3().MaxHp)
            {
                GameManager.instance.GetPlayerInfo3().Hp = GameManager.instance.GetPlayerInfo3().MaxHp;
            }
        }
    }


    public static bool CanResurrectionByEnemy(Enemy enemy)
    {
        MovingObject[,] map = GameManager.instance.MovingObjectMap();
        bool canAttack = false;
        int x = (int)enemy.transform.position.x;
        int y = (int)enemy.transform.position.y;
        for (int posX = -3; posX <= 3; posX++)
        {
            for (int posY = -3; posY <= 3; posY++)
            {
                if (CanRessurectionAttack(x + posX, y + posY, map, enemy))
                {
                    canAttack = true;
                }
            }
        }

        if (canAttack)
        {
            enemy.SetPrepareSkillEnum(SkillEnum.Resurrection);
        }
        return canAttack;
    }

    private static bool CanRessurectionAttack(int x, int y, MovingObject[,] movingObjectMap, Enemy enemy)
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

    public static bool RessurectionByPlayer(Player player)
    {
        bool isTrue = false ;
        if (GameManager.instance.GetPlayerInfo1().Hp <= 0)
        {
            isTrue = true;
        }
        if (GameManager.instance.GetPlayerInfo2().Hp <= 0)
        {
            isTrue = true;
        }
        if (GameManager.instance.GetPlayerInfo3().Hp <= 0)
        {
            isTrue = true;
        }
        if (isTrue)
        {
            SetSkill(player, true);
        }
        else
        {
            DialogManager.instance.AttackFormat(player.GetNowMovingObjectInfo().Name, "リザレクション");
        }
        return isTrue;
    }

    public static void RessurectionByEnemy(Enemy client)
    {
        SetSkill(client, false);
    }

    public static void SetSkill(MovingObject client, bool isPlayer)
    {
        DialogManager.instance.AttackFormat(client.GetNowMovingObjectInfo().Name, "リザレクション");

        Skill skill = Instantiate(AttackStock.instance.ressrection).GetComponent<Skill>();
        skill.skillEnum = SkillEnum.Resurrection;
        skill.SetClient(client);
        skill.AttackDeal();
    }
}
