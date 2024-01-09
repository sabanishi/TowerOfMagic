using UnityEngine;
using System.Collections;

public class AreaHeal : Skill
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
            seType = SoundManager.SE_Type.areaHeal1;
        }
        else
        {
            seType = SoundManager.SE_Type.areaHeal2;
        }

        SoundManager.instance.PlaySE(seType);

        SoundManager.instance.PlaySE(SoundManager.SE_Type.healEffect);
        transform.position = client.transform.position;
        client.Mahouzin.SetActive(true);
        client.Mahouzin.GetComponent<SpriteRenderer>().material = MaterialStock.instance.Y;
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
            level = client.GetNowMovingObjectInfo().skillLvMemory.GetSkillLV(SkillEnum.AreaHeal);
        }
        else
        {
            level = ((Player)client).itemSkillLV;
        }

        float healProperity = SkillConstractParent.ReturnScaleFromSkillEnum(SkillEnum.AreaHeal,level);

        if (GameManager.instance.GetPlayerInfo1().Hp > 1&&GameManager.instance.GetPlayerInfo1().Hp<GameManager.instance.GetPlayerInfo1().MaxHp)
        {
            int beforeHp1 = GameManager.instance.GetPlayerInfo1().Hp;
            GameManager.instance.GetPlayerInfo1().Hp += (int)(GameManager.instance.GetPlayerInfo1().MaxHp * healProperity);
            if (GameManager.instance.GetPlayerInfo1().Hp > GameManager.instance.GetPlayerInfo1().MaxHp)
            {
                GameManager.instance.GetPlayerInfo1().Hp = GameManager.instance.GetPlayerInfo1().MaxHp;
            }
            DialogManager.instance.HealFormat(GameManager.instance.GetPlayerInfo1().Name, GameManager.instance.GetPlayerInfo1().MaxHp - beforeHp1);
        }
        if (GameManager.instance.GetPlayerInfo2().Hp > 1 && GameManager.instance.GetPlayerInfo2().Hp < GameManager.instance.GetPlayerInfo2().MaxHp)
        {
            int beforeHp2 = GameManager.instance.GetPlayerInfo2().Hp;
            GameManager.instance.GetPlayerInfo2().Hp += (int)(GameManager.instance.GetPlayerInfo2().MaxHp * healProperity);
            if (GameManager.instance.GetPlayerInfo2().Hp > GameManager.instance.GetPlayerInfo2().MaxHp)
            {
                GameManager.instance.GetPlayerInfo2().Hp = GameManager.instance.GetPlayerInfo2().MaxHp;
            }
            DialogManager.instance.HealFormat(GameManager.instance.GetPlayerInfo2().Name, GameManager.instance.GetPlayerInfo2().MaxHp - beforeHp2);
        }
        if (GameManager.instance.GetPlayerInfo3().Hp > 1 && GameManager.instance.GetPlayerInfo3().Hp < GameManager.instance.GetPlayerInfo3().MaxHp)
        {
            int beforeHp3 = GameManager.instance.GetPlayerInfo3().Hp;
            GameManager.instance.GetPlayerInfo3().Hp += (int)(GameManager.instance.GetPlayerInfo3().MaxHp * healProperity);
            if (GameManager.instance.GetPlayerInfo3().Hp > GameManager.instance.GetPlayerInfo3().MaxHp)
            {
                GameManager.instance.GetPlayerInfo3().Hp = GameManager.instance.GetPlayerInfo3().MaxHp;
            }
            DialogManager.instance.HealFormat(GameManager.instance.GetPlayerInfo3().Name, GameManager.instance.GetPlayerInfo3().MaxHp - beforeHp3);
        }  
    }

    public static bool CanAreaHealByEnemy(Enemy enemy)
    {
        MovingObject[,] map = GameManager.instance.MovingObjectMap();
        bool canAttack = false;
        int x = (int)enemy.transform.position.x;
        int y = (int)enemy.transform.position.y;
        for (int posX = -3; posX <= 3; posX++)
        {
            for (int posY = -3; posY <= 3; posY++)
            {
                if (CanAreaHealAttack(x + posX, y + posY, map, enemy))
                {
                    canAttack = true;
                }
            }
        }

        if (canAttack)
        {
            enemy.SetPrepareSkillEnum(SkillEnum.Heal);
        }
        return canAttack;
    }

    private static bool CanAreaHealAttack(int x, int y, MovingObject[,] movingObjectMap, Enemy enemy)
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

    public static bool AreaHealByPlayer(Player player)
    {
        SetSkill(player);
        return true;
    }

    public static void AreaHealByEnemy(Enemy client)
    {
        SetSkill(client);
    }

    public static void SetSkill(MovingObject client)
    {
        DialogManager.instance.AttackFormat(client.GetNowMovingObjectInfo().Name, "エリアヒール");
       
        Skill skill = Instantiate(AttackStock.instance.areaHeal).GetComponent<Skill>();
        skill.skillEnum = SkillEnum.AreaHeal;
        skill.SetClient(client);
        skill.AttackDeal();
    }

}