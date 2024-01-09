using UnityEngine;
using System.Collections;

public class HealVeil: Skill
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
            seType = SoundManager.SE_Type.healveil1;
        }
        else
        {
            seType = SoundManager.SE_Type.healveil2;
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
            level = client.GetNowMovingObjectInfo().skillLvMemory.GetSkillLV(SkillEnum.HealVeil);
        }
        else
        {
            level = ((Player)client).itemSkillLV;
        }
        float healProperity = SkillConstractParent.ReturnScaleFromSkillEnum(SkillEnum.HealVeil,level);

        client.AddCondition(PlayerCondition.HealVeil);
        client.HealVeilProperity = healProperity;
    }
    

    public static bool CanHealVeilByEnemy(Enemy enemy)
    {
        MovingObject[,] map = GameManager.instance.MovingObjectMap();
        bool canAttack = false;
        int x = (int)enemy.transform.position.x;
        int y = (int)enemy.transform.position.y;
        for (int posX = -3; posX <= 3; posX++)
        {
            for (int posY = -3; posY <= 3; posY++)
            {
                if (CanHealVeilAttack(x + posX, y + posY, map, enemy))
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

    private static bool CanHealVeilAttack(int x, int y, MovingObject[,] movingObjectMap, Enemy enemy)
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

    public static bool HealVeilByPlayer(Player player)
    {
        SetSkill(player, true);
        return true;
    }

    public static void HealVeilByEnemy(Enemy client)
    {
        SetSkill(client, false);
    }

    public static void SetSkill(MovingObject client, bool isPlayer)
    {
        DialogManager.instance.AttackFormat(client.GetNowMovingObjectInfo().Name, "ヒールベール");
        
        Skill skill = Instantiate(AttackStock.instance.healVeil).GetComponent<Skill>();
        skill.skillEnum = SkillEnum.HealVeil;
        skill.SetClient(client);
        skill.AttackDeal();
    }
}
