using System;
using System.Collections;
using UnityEngine;

public class Craft : Skill
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
            seType = SoundManager.SE_Type.craft1;
        }
        else
        {
            seType = SoundManager.SE_Type.heal2;
        }

        SoundManager.instance.PlaySE(seType);

        SoundManager.instance.PlaySE(SoundManager.SE_Type.craft);
        transform.position = client.transform.position;
        client.Mahouzin.SetActive(true);
        client.Mahouzin.GetComponent<SpriteRenderer>().material = MaterialStock.instance.YandG;
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
            level = client.GetNowMovingObjectInfo().skillLvMemory.GetSkillLV(SkillEnum.Craft);
        }
        else
        {
            level = ((Player)client).itemSkillLV;
        }
        int itemLevel = (int)(SkillConstractParent.ReturnScaleFromSkillEnum(SkillEnum.Craft, level));
        switch (itemLevel)
        {
            default:
                break;
        }
        ItemEnum itemEnum;
        if (itemLevel == 5)
        {
            itemEnum = CreateItemData.ReturnRandomItemForMonsterHouse(itemLevel * 6);
        }
        else
        {
            itemEnum = CreateItemData.ReturnRandomItem(itemLevel * 6);
        }
        GameManager.instance.GetPlayerController().GetPlayer().GetItemChest().AddItem(itemEnum);
        DialogManager.instance.SetText("アイテム:<color=#00ff00>" + ItemConstractParent.ReturnNameFromItemEnum(itemEnum) + "</color>　を作り出した！");
    }


    public static bool CanCraftByEnemy(Enemy enemy)
    {
        MovingObject[,] map = GameManager.instance.MovingObjectMap();
        bool canAttack = false;
        int x = (int)enemy.transform.position.x;
        int y = (int)enemy.transform.position.y;
        for (int posX = -3; posX <= 3; posX++)
        {
            for (int posY = -3; posY <= 3; posY++)
            {
                if (CanHealAttack(x + posX, y + posY, map, enemy))
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

    private static bool CanHealAttack(int x, int y, MovingObject[,] movingObjectMap, Enemy enemy)
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

    public static bool CraftByPlayer(Player player)
    {
        if (player.GetItemChest().CanAddItem())
        {
            SetSkill(player, true);
            return true;
        }
        DialogManager.instance.AttackFormat(player.GetNowMovingObjectInfo().Name, "クラフト");
        return false;
    }

    public static void CraftByEnemy(Enemy client)
    {
        SetSkill(client, false);
    }

    public static void SetSkill(MovingObject client, bool isPlayer)
    {
        DialogManager.instance.AttackFormat(client.GetNowMovingObjectInfo().Name, "クラフト");
        Skill skill = Instantiate(AttackStock.instance.craft).GetComponent<Skill>();
        skill.skillEnum = SkillEnum.Craft;
        skill.SetClient(client);
        skill.AttackDeal();
    }
}