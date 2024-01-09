using UnityEngine;
using System.Collections;

public class Phoenix : Skill
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
            seType = SoundManager.SE_Type.phoenix1;
        }
        else
        {
            seType = SoundManager.SE_Type.phoenix2;
        }

        SoundManager.instance.PlaySE(seType);

        SoundManager.instance.PlaySE(SoundManager.SE_Type.fireSE);
        transform.position = client.transform.position;
        client.Mahouzin.SetActive(true);
        client.Mahouzin.GetComponent<SpriteRenderer>().material = MaterialStock.instance.R;
        Hashtable clientHash = new Hashtable();
        clientHash.Add("time", 1.3f);
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
            level = client.GetNowMovingObjectInfo().skillLvMemory.GetSkillLV(SkillEnum.Phoenix);
        }
        else
        {
            level = ((Player)client).itemSkillLV;
        }

        float healProperity = SkillConstractParent.ReturnScaleFromSkillEnum(SkillEnum.Phoenix,level);
        if (!GameManager.instance.GetPlayerInfo1().Equals(client.GetNowMovingObjectInfo()))
        {
            GameManager.instance.GetPlayerInfo1().Hp += (int)(GameManager.instance.GetPlayerInfo1().MaxHp * healProperity);
            GameManager.instance.GetPlayerInfo1().Mp += (int)(GameManager.instance.GetPlayerInfo1().MaxMp * healProperity);
        }
        if (!GameManager.instance.GetPlayerInfo2().Equals(client.GetNowMovingObjectInfo()))
        {
            GameManager.instance.GetPlayerInfo2().Hp += (int)(GameManager.instance.GetPlayerInfo2().MaxHp * healProperity);
            GameManager.instance.GetPlayerInfo2().Mp += (int)(GameManager.instance.GetPlayerInfo2().MaxMp * healProperity);
        }
        if (!GameManager.instance.GetPlayerInfo3().Equals(client.GetNowMovingObjectInfo()))
        {
            GameManager.instance.GetPlayerInfo3().Hp += (int)(GameManager.instance.GetPlayerInfo3().MaxHp * healProperity);
            GameManager.instance.GetPlayerInfo3().Mp += (int)(GameManager.instance.GetPlayerInfo3().MaxMp * healProperity);
        }
        client.OnDamage(9999999);
    }

    public static bool CanPhoenixByEnemy(Enemy enemy)
    {
        MovingObject[,] map = GameManager.instance.MovingObjectMap();
        bool canAttack = false;
        int x = (int)enemy.transform.position.x;
        int y = (int)enemy.transform.position.y;
        for (int posX = -3; posX <= 3; posX++)
        {
            for (int posY = -3; posY <= 3; posY++)
            {
                if (CanPhoenixAttack(x + posX, y + posY, map, enemy))
                {
                    canAttack = true;
                }
            }
        }

        if (canAttack)
        {
            enemy.SetPrepareSkillEnum(SkillEnum.Phoenix);
        }
        return canAttack;
    }

    private static bool CanPhoenixAttack(int x, int y, MovingObject[,] movingObjectMap, Enemy enemy)
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

    public static bool PhoenixByPlayer(Player player)
    {
        SetSkill(player, true);
        return true;
    }

    public static void PhoenixByEnemy(Enemy client)
    {
        SetSkill(client, false);
    }

    public static void SetSkill(MovingObject client, bool isPlayer)
    {
        DialogManager.instance.AttackFormat(client.GetNowMovingObjectInfo().Name, "フェニックス");
       
        Skill skill = Instantiate(AttackStock.instance.phoenix).GetComponent<Skill>();
        skill.skillEnum = SkillEnum.Phoenix;
        skill.SetClient(client);
        skill.AttackDeal();
    }
}
