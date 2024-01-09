using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : Skill
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
            seType = SoundManager.SE_Type.burn1;
        }
        else
        {
            seType = SoundManager.SE_Type.burn2;
        }

        SoundManager.instance.PlaySE(seType);

        SoundManager.instance.PlaySE(SoundManager.SE_Type.fireSE);
        if (targets[0] != null)
        {
            transform.position = targets[0].transform.position;
            Instantiate(AttackStock.instance.damageEffect, targets[0].transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("targetsに敵が入ってない");
        }
        client.Mahouzin.SetActive(true);
        client.Mahouzin.GetComponent<SpriteRenderer>().material = MaterialStock.instance.R;
        Hashtable clientHash = new Hashtable();
        clientHash.Add("time", 1.1f);
        clientHash.Add("delay", 0);
        clientHash.Add("oncompletetarget", client.gameObject);
        clientHash.Add("oncomplete", "FinishSkill");
        iTween.MoveTo(client.gameObject, clientHash);

        Hashtable animationHash = new Hashtable();
        animationHash.Add("time", 0.6f);
        animationHash.Add("delay", 0.2);
        animationHash.Add("oncompletetarget", gameObject);
        animationHash.Add("onstart", "StartSkill");
        animationHash.Add("oncomplete", "FinishSkill");
        iTween.MoveTo(gameObject, animationHash);
    }

    public override void FinishSkill()
    {
        CalculateDamage(targets[0]);
        base.FinishSkill();
    }

    public static bool CanBurnByEnemy(Enemy enemy)
    {
        MovingObject[,] map = GameManager.instance.MovingObjectMap();
        int x = (int)enemy.transform.position.x;
        int y = (int)enemy.transform.position.y;
        if (CanBurnAttack(x + 1, y, map, enemy))
        {
            enemy.SetFaceDirection(Direction.Right);
        }
        else if (CanBurnAttack(x - 1, y, map, enemy))
        {
            enemy.SetFaceDirection(Direction.Left);
        }
        else if (CanBurnAttack(x, y + 1, map, enemy))
        {
            enemy.SetFaceDirection(Direction.Up);
        }
        else if (CanBurnAttack(x, y - 1, map, enemy))
        {
            enemy.SetFaceDirection(Direction.Down);
        }
        else
        {
            return false;
        }
        enemy.SetPrepareSkillEnum(SkillEnum.Burn);
        return true;
    }

    private static bool CanBurnAttack(int x, int y, MovingObject[,] movingObjectMap, Enemy enemy)
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

    public static bool BurnByPlayer(Player player)
    {
        int x = (int)player.transform.position.x;
        int y = (int)player.transform.position.y;

        switch (player.GetFaceDirection())
        {
            case Direction.Up:
                y++;
                break;
            case Direction.Down:
                y--;
                break;
            case Direction.Right:
                x++;
                break;
            case Direction.Left:
                x--;
                break;
        }

        MovingObject[,] movingObjectMap = GameManager.instance.MovingObjectMap();
        if (movingObjectMap[x, y] != null)
        {
            if (!movingObjectMap[x, y].IsPlayer())
            {
                SetSkill(player, movingObjectMap[x, y]);
                return true;
            }
        }
        DialogManager.instance.AttackFormat(player.GetNowMovingObjectInfo().Name, "バーン");
        return false;
    }

    public static void BurnByEnemy(Enemy client, MovingObject target)
    {
        SetSkill(client, target);
    }

    public static void SetSkill(MovingObject client, MovingObject target)
    {
        DialogManager.instance.AttackFormat(client.GetNowMovingObjectInfo().Name, "バーン");
        
        Skill skill = Instantiate(AttackStock.instance.burn).GetComponent<Skill>();
        skill.skillEnum = SkillEnum.Burn;
        skill.SetClient(client);
        skill.SetTarget(target);
        skill.AttackDeal();
        
    }
}
