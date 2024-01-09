using UnityEngine;
using System.Collections;

public class AgilityUp : Skill
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
            seType = SoundManager.SE_Type.agilityUp1;
        }
        else
        {
            seType = SoundManager.SE_Type.agilityUp2;
        }
        SoundManager.instance.PlaySE(seType);

        SoundManager.instance.PlaySE(SoundManager.SE_Type.aura);
        transform.position = client.transform.position;
        client.Mahouzin.SetActive(true);
        client.Mahouzin.GetComponent<SpriteRenderer>().material = MaterialStock.instance.G;
        Hashtable clientHash = new Hashtable();
        clientHash.Add("time", 1.3f);
        clientHash.Add("delay", 0);
        clientHash.Add("oncompletetarget", client.gameObject);
        clientHash.Add("oncomplete", "FinishSkill");
        iTween.MoveTo(client.gameObject, clientHash);

        Hashtable animationHash = new Hashtable();
        animationHash.Add("time", 0.8f);
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
            level=  client.GetNowMovingObjectInfo().skillLvMemory.GetSkillLV(SkillEnum.AgilityUp);
        }
        else
        {
            level = ((Player)client).itemSkillLV;
        }

        if (SkillConstractParent.ReturnScaleFromSkillEnum(SkillEnum.AgilityUp,level)== 1.2f)
        {
            client.AddCondition(PlayerCondition.Eva1p2);
        }
        else
        {
            client.AddCondition(PlayerCondition.Eva1p5);
        }
    }

    public static bool CanAgilityUpByEnemy(Enemy enemy)
    {
        MovingObject[,] map = GameManager.instance.MovingObjectMap();
        bool canAttack = false;
        int x = (int)enemy.transform.position.x;
        int y = (int)enemy.transform.position.y;
        for (int posX = -3; posX <= 3; posX++)
        {
            for (int posY = -3; posY <= 3; posY++)
            {
                if (CanAgilityUpAttack(x + posX, y + posY, map, enemy))
                {
                    canAttack = true;
                }
            }
        }

        if (canAttack)
        {
            enemy.SetPrepareSkillEnum(SkillEnum.AgilityUp);
        }
        return canAttack;
    }

    private static bool CanAgilityUpAttack(int x, int y, MovingObject[,] movingObjectMap, Enemy enemy)
    {
        if (enemy.IsCondition(PlayerCondition.Atk1p2) || enemy.IsCondition(PlayerCondition.Atk1p5))
        {
            return false;
        }
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

    public static bool AgilityUpByPlayer(Player player)
    {
        SetSkill(player);
        return true;
    }

    public static void AgilityUpByEnemy(Enemy client)
    {
        SetSkill(client);
    }

    public static void SetSkill(MovingObject client)
    {
        DialogManager.instance.AttackFormat(client.GetNowMovingObjectInfo().Name, "アジリティアップ");
   
        Skill skill = Instantiate(AttackStock.instance.agilityUp).GetComponent<Skill>();
        skill.skillEnum = SkillEnum.AgilityUp;
        skill.SetClient(client);
        skill.AttackDeal();
    }
}
