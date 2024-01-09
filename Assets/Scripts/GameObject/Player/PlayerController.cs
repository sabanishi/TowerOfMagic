using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController
{
    private Player player;
    private bool isWaitForStrairs;
    public Player GetPlayer()
    {
        return player;
    }
    private bool isActive;
    public void SetIsActive(bool isActive)
    {
        this.isActive = isActive;
    }
    public bool IsActive()
    {
        return isActive;
    }
    private bool isWaitForMenu;
    public void SetIsWaitForMenu(bool isWait)
    {
        isWaitForMenu = isWait;
    }

    public PlayerController(Player _player)
    {
        player = _player;
        isActive = true;
    }

    public void PlayerMove()
    {
        if (isActive)
        {
            if (isWaitForMenu)
            {
                isWaitForMenu = false;
                return;
            }
            Normal();
        }
        else
        {
            GameManager.instance.menuManager.Act();
        }
        GameManager.instance.CheckIsPlayerInMonsterHouse();
    }

    private void Normal()
    {
        int x = (int)player.transform.position.x;
        int y = (int)player.transform.position.y;
        if (GameManager.instance.FloorMap()[x, y].FloorEnum ==FloorEnum.STRAIRS)
        {
            if (!isWaitForStrairs)
            {
                isWaitForStrairs = true;
                GetPlayer().Invoke("Restart", GameManager.instance.turnDelay);
            }
            return;
        }
        bool isMove = false;
        
        if (Input.GetButton("DirectionChange"))
        {
            int horizontal = (int)Input.GetAxisRaw("Horizontal");
            int vertical = (int)Input.GetAxisRaw("Vertical");
            player.DecideDirection(horizontal, vertical);

            switch (player.GetFaceDirection())
            {
                case Direction.Up:
                    player.GetAnimator().Play("Player_Up_Wait");
                    break;
                case Direction.Down:
                    player.GetAnimator().Play("Player_Down_Wait");
                    break;
                case Direction.Right:
                    player.GetAnimator().Play("Player_Right_Wait");
                    break;
                case Direction.Left:
                    player.GetAnimator().Play("Player_Left_Wait");
                    break;
            }
        }
        else
        {
            isMove = MoveAct();
        }

        if (!isMove)
        {
            if (Input.GetButtonDown("Attack"))
            {
                //攻撃
                NormalAttackAct();
                return;
            }

            if (Input.GetButtonDown("Skill"))
            {
                //特殊技
                Skill();
                return;
            }

            if (Input.GetButtonDown("Menu"))
            {
                isActive = false;
                GameManager.instance.menuManager.Setting();
                SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
                return;
            }
        }
    }

    private void Skill()
    {
        if (GetPlayer().GetNowMovingObjectInfo().selectSkill != SkillEnum.None && GetPlayer().GetNowMovingObjectInfo().selectSkill != SkillEnum.NormalAttack)
        {
            player.SetPrepareSkillEnum(GetPlayer().GetNowMovingObjectInfo().selectSkill);
        }
        else
        {
            SoundManager.instance.PlaySE(SoundManager.SE_Type.cannotClick);
            return;
        }
        switch (player.GetFaceDirection())
        {
            case Direction.Up:
                player.GetAnimator().Play("Player_Up_Skill", 0);
                break;
            case Direction.Down:
                player.GetAnimator().Play("Player_Down_Skill", 0);
                break;
            case Direction.Right:
                player.GetAnimator().Play("Player_Right_Skill", 0);
                break;
            case Direction.Left:
                player.GetAnimator().Play("Player_Left_Skill", 0);
                break;
        }
        player.Finish();
        player.SetIsRequestAttack(true);
        player.SetIsActingAttack(true);
    }

    public void SkillByMenu(SkillEnum skillEnum)
    {
        player.SetPrepareSkillEnum(skillEnum);
        switch (player.GetFaceDirection())
        {
            case Direction.Up:
                player.GetAnimator().Play("Player_Up_Skill", 0);
                break;
            case Direction.Down:
                player.GetAnimator().Play("Player_Down_Skill", 0);
                break;
            case Direction.Right:
                player.GetAnimator().Play("Player_Right_Skill", 0);
                break;
            case Direction.Left:
                player.GetAnimator().Play("Player_Left_Skill", 0);
                break;
        }
        player.Finish();
        player.SetIsRequestAttack(true);
        player.SetIsActingAttack(true);
    }

    public void SkillByItem(SkillEnum skillEnum,int level)
    {
        player.SetIsItemSKill(true);
        player.itemSkillLV = level;
        SkillByMenu(skillEnum);
    }

    private void NormalAttackAct()
    {
        switch (player.GetFaceDirection())
        {
            case Direction.Up:
                player.GetAnimator().Play("Player_Up_Skill",0);
                break;
            case Direction.Down:
                player.GetAnimator().Play("Player_Down_Skill",0);
                break;
            case Direction.Right:
                player.GetAnimator().Play("Player_Right_Skill",0);
                break;
            case Direction.Left:
                player.GetAnimator().Play("Player_Left_Skill",0);
                break;
        }
        player.SetPrepareSkillEnum(SkillEnum.NormalAttack);
        player.Finish();
        player.SetIsRequestAttack(true);
        player.SetIsActingAttack(true);
    }

    private bool MoveAct()
    {
        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");
        //斜め禁止
        if (horizontal != 0)
        {
            vertical = 0;
        }

        if (horizontal != 0 || vertical != 0)
        {
            player.DecideDirection((int)horizontal, (int)vertical);
            bool isTrue = player.Move(horizontal, vertical);

            if (isTrue)
            {
                switch (player.GetFaceDirection())
                {
                    case Direction.Up:
                        player.GetAnimator().Play("Player_Up_Walk");
                        break;
                    case Direction.Down:
                        player.GetAnimator().Play("Player_Down_Walk");
                        break;
                    case Direction.Right:
                        player.GetAnimator().Play("Player_Right_Walk");
                        break;
                    case Direction.Left:
                        player.GetAnimator().Play("Player_Left_Walk");
                        break;
                }
                SoundManager.instance.PlaySE(SoundManager.SE_Type.walk);
                return true;
            }
        }
        switch (player.GetFaceDirection())
        {
            case Direction.Up:
                player.GetAnimator().Play("Player_Up_Wait");
                break;
            case Direction.Down:
                player.GetAnimator().Play("Player_Down_Wait");
                break;
            case Direction.Right:
                player.GetAnimator().Play("Player_Right_Wait");
                break;
            case Direction.Left:
                player.GetAnimator().Play("Player_Left_Wait");
                break;
        }

        return false;
    }
}

