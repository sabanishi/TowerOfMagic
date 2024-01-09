using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Player : MovingObject
{
    public int pointsPerFood = 10;
    public int pointsPerSoda;
    public float restartlevelDelay = 1f;
    private bool isItemSkill;
    public void SetIsItemSKill(bool _isItemSkill)
    {
        isItemSkill = _isItemSkill;
    }
    public bool IsItemSkill()
    {
        return isItemSkill;
    }
    public int itemSkillLV;

   
    public ItemChest GetItemChest()
    {
        return GameManager.instance.ItemChest;
    }

    protected override void Start()
    {
        base.Start();
        SetIsPlayer(true);
    }

    private void OnDisable()
    {
       
    }

    public override bool Move(int xDir, int yDir)
    {
        bool isTrue = base.Move(xDir, yDir);
        return isTrue;
    }

    public void Attack()
    {
        switch (prepareSkillEnum)
        {
            case SkillEnum.NormalAttack:
                if (!NormalSkill.NormalAttackByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.Fire:
                if (!Fire.FireByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.Burn:
                if (!Burn.BurnByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.Water:
                if (!Water.WaterByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.Sprash:
                if (!Sprash.SprashByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.Wind:
                if (!Wind.WindByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.Blast:
                if (!Blast.BlastByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.Stone:
                if (!Stone.StoneByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.Quake:
                if (!Quake.QuakeByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.AttackUp:
                if (!AttackUp.AttackUpByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.DefenceUp:
                if (!DefenceUp.DefenceUpByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.AgilityUp:
                if (!AgilityUp.AgilityUpByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.Heal:
                if (!Heal.HealByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.Phoenix:
                if (!Phoenix.PhoenixByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.Freeze:
                if (!Freeze.FreezeByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.SpeedUp:
                if (!SpeedUp.SpeedUpByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.Comet:
                if (!Comet.CometByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.AreaHeal:
                if (!AreaHeal.AreaHealByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.Explosion:
                if (!Explosion.ExplsoionByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.BigBurn:
                if (!BigBurn.BigBurnByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.SuperNova:
                if (!SuperNova.SuperNovaByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.Volcano:
                if (!Volcano.VolcanoByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.Meteo:
                if (!Meteo.MeteoByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.Stome:
                if (!Stome.StomeByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.Thundar:
                if (!Thundar.ThundarByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.Resurrection:
                if (!Resurrection.RessurectionByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.HealVeil:
                if (!HealVeil.HealVeilByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.Craft:
                if (!Craft.CraftByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.Warp:
                if (!Warp.WarpByPlayer(this))
                {
                    AttackMiss();
                }
                break;
            case SkillEnum.None:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Exit")
        {
            Invoke("Restart", restartlevelDelay);
            enabled = false;
        }
        if (collision.gameObject.tag == "Item")
        {
            Debug.Log(collision.gameObject.GetComponent<ItemScriptForField>().itemEnum);
            Destroy(collision.gameObject);
        }
    }

    private void PlayerWaitAnimation()
    {
        switch (GetFaceDirection())
        {
            case Direction.Up:
                GetAnimator().Play("Player_Up_Wait");
                break;
            case Direction.Down:
                GetAnimator().Play("Player_Down_Wait");
                break;
            case Direction.Right:
                GetAnimator().Play("Player_Right_Wait");
                break;
            case Direction.Left:
                GetAnimator().Play("Player_Left_Wait");
                break;
        }
    }

    public override void FinishSkill()
    {
        isItemSkill = false;
        base.FinishSkill();
        PlayerWaitAnimation();
    }

   public void Restart()
    {
        SoundManager.instance.PlaySE(SoundManager.SE_Type.exitSE);
        if (GameManager.instance.turnCount >= 100)
        {
            SoundManager.instance.PlaySE(SoundManager.SE_Type.exit2);
        }
        else
        {
            SoundManager.instance.PlaySE(SoundManager.SE_Type.exit);
        }
        GameManager.instance.NowPlayerInfo = GetNowMovingObjectInfo();
        GameManager.instance.menuManager.gameObject.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}