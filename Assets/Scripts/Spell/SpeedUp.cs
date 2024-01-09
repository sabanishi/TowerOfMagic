using UnityEngine;
using System.Collections;

public class SpeedUp : Skill
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
            seType = SoundManager.SE_Type.speedUp1;
        }
        else
        {
            seType = SoundManager.SE_Type.speedUp2;
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
        if (client.IsCondition(PlayerCondition.SpeedDown))
        {
            client.CureCondition(PlayerCondition.SpeedDown);
            DialogManager.instance.SetText("<color=#ffff00>" + client.GetNowMovingObjectInfo().Name + "</color>　の素早さが元に戻った");
            return;
        }

        if (!client.IsCondition(PlayerCondition.SpedUp))
        {
            client.FragForSpeedUp = false;
        }
        client.AddCondition(PlayerCondition.SpedUp);
        DialogManager.instance.SetText("<color=#ffff00>" + client.GetNowMovingObjectInfo().Name + "</color>　の素早さが上がった");
    }

    public static bool CanSpeedUpByEnemy(Enemy enemy)
    {
        if (enemy.IsCondition(PlayerCondition.SpedUp))
        {
            return false;
        }
        MovingObject[,] map = GameManager.instance.MovingObjectMap();
        bool canAttack = false;
        int x = (int)enemy.transform.position.x;
        int y = (int)enemy.transform.position.y;
        for (int posX = -3; posX <= 3; posX++)
        {
            for (int posY = -3; posY <= 3; posY++)
            {
                if (CanSpeedUpAttack(x + posX, y + posY, map, enemy))
                {
                    canAttack = true;
                }
            }
        }

        if (canAttack)
        {
            enemy.SetPrepareSkillEnum(SkillEnum.SpeedUp);
        }
        return canAttack;
    }

    private static bool CanSpeedUpAttack(int x, int y, MovingObject[,] movingObjectMap, Enemy enemy)
    {
        if (enemy.IsCondition(PlayerCondition.SpedUp))
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

    public static bool SpeedUpByPlayer(Player player)
    {
        SetSkill(player);
        return true;
    }

    public static void SpeedUpByEnemy(Enemy client)
    {
        SetSkill(client);
    }

    public static void SetSkill(MovingObject client)
    {
        DialogManager.instance.AttackFormat(client.GetNowMovingObjectInfo().Name, "スピードアップ");
       
        Skill skill = Instantiate(AttackStock.instance.speedUp).GetComponent<Skill>();
        skill.skillEnum = SkillEnum.SpeedUp;
        skill.SetClient(client);
        skill.AttackDeal();
    }
}
