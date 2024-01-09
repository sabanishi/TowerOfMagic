using UnityEngine;
using System.Collections;

public class AttackUp:Skill
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
            seType = SoundManager.SE_Type.attackUp1;
        }
        else
        {
            seType = SoundManager.SE_Type.attackUp2;
        }

        SoundManager.instance.PlaySE(seType);

        SoundManager.instance.PlaySE(SoundManager.SE_Type.aura);
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
            level = client.GetNowMovingObjectInfo().skillLvMemory.GetSkillLV(SkillEnum.AttackUp);
        }
        else
        {
            level = ((Player)client).itemSkillLV;
        }

        if (SkillConstractParent.ReturnScaleFromSkillEnum(SkillEnum.AttackUp,level) == 1.2f){
            client.AddCondition(PlayerCondition.Atk1p2);
        }
        else
        {
            client.AddCondition(PlayerCondition.Atk1p5);
        }
    }

    public static bool CanAttackUpByEnemy(Enemy enemy)
    {
        MovingObject[,] map = GameManager.instance.MovingObjectMap();
        bool canAttack = false;
        int x = (int)enemy.transform.position.x;
        int y = (int)enemy.transform.position.y;
        for (int posX = -3; posX <= 3; posX++)
        {
            for (int posY = -3; posY <= 3; posY++)
            {
                if (CanAttackUpAttack(x + posX, y + posY, map, enemy))
                {
                    canAttack = true;
                }
            }
        }

        if (canAttack)
        {
            enemy.SetPrepareSkillEnum(SkillEnum.AttackUp);
        }
        return canAttack;
    }

    private static bool CanAttackUpAttack(int x, int y, MovingObject[,] movingObjectMap, Enemy enemy)
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

    public static bool AttackUpByPlayer(Player player)
    {
        SetSkill(player);
        return true;
    }

    public static void AttackUpByEnemy(Enemy client)
    {
        SetSkill(client);
    }

    public static void SetSkill(MovingObject client)
    {
        DialogManager.instance.AttackFormat(client.GetNowMovingObjectInfo().Name, "アタックアップ");
 
        Skill skill = Instantiate(AttackStock.instance.attackUp).GetComponent<Skill>();
        skill.skillEnum = SkillEnum.AttackUp;
        skill.SetClient(client);
        skill.AttackDeal();
    }
}
