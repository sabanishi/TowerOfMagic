using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Skill : MonoBehaviour
{
    public SkillEnum skillEnum;
    protected int StandardDamage;
    protected MovingObject client;
    public void SetClient(MovingObject _client)
    {
        client = _client;
    }

    protected List<MovingObject> targets=new List<MovingObject>();
    public void SetTarget(MovingObject target)
    {
        targets.Add(target);
    }
    public void SetTargets(List<MovingObject> _targets)
    {
        targets.AddRange(_targets);
        _targets = null;
    }

    protected Animator animator;
    protected SpriteRenderer sprite;
    public virtual void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        animator.enabled = false;
        sprite.enabled = false;
    }

    public void AttackDeal()
    {
        if (CanAttack())
        {
            Attack();
        }
        else
        {
            client.AttackMissForMp();
        }
    }

    private bool CanAttack()
    {
        if (!client.IsPlayer())
        {
            return true;
        }

        if (((Player)client).IsItemSkill())
        {
            return true;
        }

        if (client.GetNowMovingObjectInfo().Mp >= SkillConstractParent.ReturnNeedMpFromSkillEnum(skillEnum, client.GetNowMovingObjectInfo().skillLvMemory.GetSkillLV(skillEnum)))
        {
            client.GetNowMovingObjectInfo().Mp -= SkillConstractParent.ReturnNeedMpFromSkillEnum(skillEnum, client.GetNowMovingObjectInfo().skillLvMemory.GetSkillLV(skillEnum));
            return true;
        }
        return false;
    }

    public abstract void Attack();

    public virtual void StartSkill()
    {
        animator.enabled = true;
        sprite.enabled = true;
    }

    public virtual void FinishSkill()
    {
        Destroy(gameObject);
    }

    protected virtual void CalculateDamage(MovingObject _target)
    {
        int damage = ReturnDamage(skillEnum, client, _target);
        DialogManager.instance.DamageFormat(_target.GetNowMovingObjectInfo().Name, damage);
        _target.OnDamage(damage);
    }

    protected int ReturnDamage(SkillEnum skillEnum,MovingObject clinet,MovingObject target)
    {
        MovingObjectInfomation clientInfo = clinet.GetNowMovingObjectInfo();
        MovingObjectInfomation targetInfo = target.GetNowMovingObjectInfo();
        float scale = 1;
        if (!clinet.IsPlayer()||!((Player)client).IsItemSkill())
        {
            if (skillEnum != SkillEnum.None && skillEnum != SkillEnum.NormalAttack)
            {
                scale = SkillConstractParent.ReturnScaleFromSkillEnum(skillEnum, clientInfo.skillLvMemory.GetSkillLV(skillEnum));
            }
        }
        else
        {
            if (skillEnum != SkillEnum.None && skillEnum != SkillEnum.NormalAttack)
            {
                scale = SkillConstractParent.ReturnScaleFromSkillEnum(skillEnum,((Player)client).itemSkillLV);
            }
        }
      

        int atk = clientInfo.Atk;
        int def = targetInfo.Def/2;

        float atkBuff=1, defBuff=1;
        if (clinet.IsCondition(PlayerCondition.Atk1p2))
        {
            atkBuff = 1.2f;
        }else if (client.IsCondition(PlayerCondition.Atk1p5))
        {
            atkBuff = 1.5f;
        }

        if (target.IsCondition(PlayerCondition.Def1p2))
        {
            defBuff = 1.2f;
        }else if (target.IsCondition(PlayerCondition.Def1p5))
        {
            defBuff = 1.5f;
        }

        float damage = atk * atkBuff * scale - def * defBuff;

        List<Attribute> attributes = SkillConstractParent.ReturnAttributeListFromSkillEnum(skillEnum);
        float newDamage=0;
        if (attributes == null)
        {
            newDamage = damage;
        }
        else
        {
            foreach(var attribute in attributes)
            {
                float standardDamage = damage / attributes.Count;
                float attributeScale = 1;
                float weakScale=1;
                switch (attribute)
                {
                    case Attribute.Fire:
                        attributeScale += (float)(clientInfo.FireLevel) / 10;
                        weakScale += (float)(targetInfo.WindLevel) / 20;
                        weakScale -= (float)(targetInfo.WaterLevel) / 20;
                        break;
                    case Attribute.Water:
                        attributeScale += (float)(clientInfo.WaterLevel) / 10;
                        weakScale += (float)(targetInfo.FireLevel) / 20;
                        weakScale -= (float)(targetInfo.GroundLevel) / 20;
                        break;
                    case Attribute.Ground:
                        attributeScale += (float)(clientInfo.GroundLevel) / 10;
                        weakScale += (float)(targetInfo.WaterLevel) / 20;
                        weakScale -= (float)(targetInfo.WindLevel) / 20;
                        break;
                    case Attribute.Wind:
                        attributeScale += (float)(clientInfo.WindLevel) / 10;
                        weakScale += (float)(targetInfo.GroundLevel) / 20;
                        weakScale -= (float)(targetInfo.FireLevel) / 20;
                        break;
                }
                standardDamage = standardDamage * attributeScale * weakScale;
                newDamage += standardDamage;
            }
        }
        if (!client.IsPlayer())
        {
            newDamage *= 0.5f;
        }
        int returnDamage = (int)newDamage;
        if (returnDamage <= 0)
        {
            returnDamage = 1;
        }
        return returnDamage;
    }
}
