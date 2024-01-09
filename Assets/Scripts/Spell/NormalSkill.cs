using UnityEngine;
using System.Collections;

public class NormalSkill : Skill
{
    public override void Start()
    {
        base.Start();
        skillEnum = SkillEnum.NormalAttack;
    }
    public override void Attack()
    {
        SoundManager.instance.PlaySE(SoundManager.SE_Type.chant);
        if (targets[0] != null)
        {
            transform.position = targets[0].transform.position;
        }
        else
        {
            Debug.Log("targetsに敵が入ってない");
        }
        client.Mahouzin.SetActive(true);
        client.Mahouzin.GetComponent<SpriteRenderer>().material = MaterialStock.instance.White;
        Hashtable clientHash = new Hashtable();
        clientHash.Add("time", 0.85f);
        clientHash.Add("delay", 0);
        clientHash.Add("oncompletetarget", client.gameObject);
        clientHash.Add("oncomplete", "FinishSkill");
        iTween.MoveTo(client.gameObject, clientHash);

        Hashtable animationHash = new Hashtable();
        animationHash.Add("time", 0.35f);
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

    public static bool CanNormalAttackByEnemy(Enemy enemy)
    {
        MovingObject[,] map = GameManager.instance.MovingObjectMap();
        int x = (int)enemy.transform.position.x;
        int y = (int)enemy.transform.position.y;
        if (NormalSkill.CanNormalAttack(x + 1, y,map,enemy))
        {
            enemy.SetFaceDirection(Direction.Right);
        }
        else if (NormalSkill.CanNormalAttack(x - 1, y, map, enemy))
        {
            enemy.SetFaceDirection(Direction.Left);
        }
        else if (NormalSkill.CanNormalAttack(x, y + 1, map, enemy))
        {
            enemy.SetFaceDirection(Direction.Up);
        }
        else if (NormalSkill.CanNormalAttack(x, y - 1, map, enemy))
        {
            enemy.SetFaceDirection(Direction.Down);
        }
        else
        {
            return false;
        }
        enemy.SetPrepareSkillEnum(SkillEnum.NormalAttack);
        return true;
    }

    private static bool CanNormalAttack(int x, int y,MovingObject[,] movingObjectMap,Enemy enemy)
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

    public static bool NormalAttackByPlayer(Player player)
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
                NormalSkill.SetSkill(player, movingObjectMap[x, y], true);
                return true;
            }
        }
        DialogManager.instance.AttackFormat(player.GetNowMovingObjectInfo().Name, "通常攻撃");//失敗しても表示する
        return false;
    }

    public static void NormalAttackByEnemy(Enemy client,MovingObject target)
    {
        NormalSkill.SetSkill(client, target, false);
    }

    public static void SetSkill(MovingObject client, MovingObject target, bool isPlayer)
    {
        DialogManager.instance.AttackFormat(client.GetNowMovingObjectInfo().Name, "通常攻撃");
        SoundManager.SE_Type seType;
        if (isPlayer)
        {
            seType = SoundManager.SE_Type.normalAttack1;
        }
        else
        {
            seType = SoundManager.SE_Type.normalAttack2;
        }
        
        SoundManager.instance.PlaySE(seType);
        Skill skill = Instantiate(AttackStock.instance.normalSkill).GetComponent<Skill>();
        skill.SetClient(client);
        skill.SetTarget(target);
        skill.Attack();
        Instantiate(AttackStock.instance.damageEffect, target.transform.position, Quaternion.identity);
    }
}
