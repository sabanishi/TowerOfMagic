using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.UI;

public class Enemy:MovingObject
{
    public int playerDamage = 5;

    private Transform target;

    private float resumePosint;

    private Position Destination;
    private bool isReverse;

    private int targetX, targetY;
    public void SetTargetXY(int _targetX,int _targetY)
    {
        targetX = _targetX;
        targetY=_targetY;
    }

    MovingObject[,] movingObjectMap;
    FloorChip[,] floorMap;

    private float probabilityForNormalAttack;
   
    private List<SkillEnum> EffectiveSkillList = new List<SkillEnum>();

    [SerializeField] private Slider hpBar;

    public void EnemySetting(float normalAttackProbability,List<SkillEnum> skillList)
    {
        probabilityForNormalAttack = normalAttackProbability;
        EffectiveSkillList = skillList;
    }

    protected override void Start()
    {
        GameManager.instance.AddEnemyToList(this);
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        hpBar.value = (float)(GetNowMovingObjectInfo().Hp) / GetNowMovingObjectInfo().MaxHp;
    }

    public override bool Move(int xDir, int yDir)
    {
        bool isTrue=base.Move(xDir, yDir);
        return isTrue;
    }

    public void MoveEnemy()
    {
        int xDir = 0;
        int yDir = 0;
        Vector3 pos = transform.position;
        int x = (int)(pos.x);
        int y = (int)(pos.y);
        movingObjectMap = GameManager.instance.MovingObjectMap();
        floorMap = GameManager.instance.FloorMap();
        Position moveDirection;

        //プレイヤーが近くにいるかどうかの判定
        for(int _x = -3; _x <= 3; _x++)
        {
            for (int _y = -3; _y <= 3; _y++)
            {
                int newX = x + _x;
                int newY = y + _y;
                if (newX >= 0 && newX < movingObjectMap.GetLength(1))
                {
                    if (newY >= 0 && newY < movingObjectMap.GetLength(0))
                    {
                        if (movingObjectMap[newX, newY] != null)
                        {
                            if (movingObjectMap[newX, newY].IsPlayer())
                            {
                                Destination = new Position(newX, newY);
                                //攻撃できるか？　攻撃可能なら攻撃する
                                if (CanAttack((Player)movingObjectMap[newX, newY]))
                                {
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        if (Destination == null)
        {
            switch (floorMap[(int)pos.x, (int)pos.y].FloorEnum)
            {
                case FloorEnum.ENTRANCE:
                    Destination = CalculateDestinationFromEntrance(x, y);
                    break;
                case FloorEnum.ROOM:
                    Destination = CalculateDestinationFromRoom(x, y);
                    break;
                default:
                    break;
            }
            if (Destination != null)
            {
                moveDirection = CalculateDestinationDirection(x, y);
                isReverse = Utils.RandomJadge(0.5f);
            }
            else
            {
                if(floorMap[(int)pos.x, (int)pos.y].FloorEnum == FloorEnum.ENTRANCE)
                {
                    switch (faceDirection)
                    {
                        case Direction.Up:
                            faceDirection = Direction.Down;
                            break;
                        case Direction.Down:
                            faceDirection = Direction.Up;
                            break;
                        case Direction.Right:
                            faceDirection = Direction.Left;
                            break;
                        case Direction.Left:
                            faceDirection = Direction.Right;
                            break;
                    }
                }
                moveDirection = CalculateMoveDirection(x, y);
            }
        }else if (Destination.X == x && Destination.Y == y)
        {
            Destination = null;
            moveDirection = CalculateMoveDirection(x, y);
        }
        else
        {
            moveDirection = CalculateDestinationDirection(x, y);
        }

        if (moveDirection != null)
        {
            xDir = moveDirection.X;
            yDir = moveDirection.Y;
        }
        else
        {
            Debug.Log("EnemyでmoveDirectionを決められない");
        }

        if (xDir != 0 || yDir != 0)
        {
            DecideDirection(xDir, yDir);
            switch (faceDirection)
            {
                case Direction.Up:
                    GetAnimator().Play("Enemy_Up_Walk");
                    break;
                case Direction.Down:
                    GetAnimator().Play("Enemy_Down_Walk");
                    break;
                case Direction.Right:
                    GetAnimator().Play("Enemy_Right_Walk");
                    break;
                case Direction.Left:
                    GetAnimator().Play("Enemy_Left_Walk");
                    break;
            }
        }
        else
        {
            switch (faceDirection)
             {
                 case Direction.Up:
                     GetAnimator().Play("Enemy_Up_Wait");
                     break;
                 case Direction.Down:
                     GetAnimator().Play("Enemy_Down_Wait");
                     break;
                 case Direction.Right:
                     GetAnimator().Play("Enemy_Right_Wait");
                     break;
                 case Direction.Left:
                     GetAnimator().Play("Enemy_Left_Wait");
                     break;
             }
        }
        if (!Move(xDir, yDir))
        {
            SetIsMoveFinish(true);
        }
    }

    private Position CalculateDestinationDirection(int x,int y)
    {
        int xDirection = Destination.X - x;
        int yDirection = Destination.Y - y;
        if (Mathf.Abs(xDirection) > Mathf.Abs(yDirection))
        {
            Position position = HorizontalMoveAttempt(x, y, xDirection);
            if (position != null)
            {
                return position;
            }
            position = VerticalMoveAttempt(x, y, yDirection);
            if (position != null)
            {
                return position;
            }
        }
        else
        {
            Position position = VerticalMoveAttempt(x, y, yDirection);
            if (position != null)
            {
                return position;
            }
            position = HorizontalMoveAttempt(x, y, xDirection);
            if (position != null)
            {
                return position;
            }
        }
        return CalculateMoveDirection(x, y);
    }

    private Position HorizontalMoveAttempt(int x,int y,int xDirection)
    {
        //横方向に移動
        if (xDirection > 0)
        {
            if (CanMove(x + 1, y)) { return new Position(1, 0); }
        }
        else if(xDirection<0)
        {
            if (CanMove(x - 1, y)) { return new Position(-1, 0); }
        }
        return null;
    }

    private Position VerticalMoveAttempt(int x,int y,int yDirection)
    {
        //縦方向に移動
        if (yDirection > 0)
        {
            if (CanMove(x, y + 1)) { return new Position(0, 1); }
        }
        else if(yDirection<0)
        {
            if (CanMove(x, y - 1)) { return new Position(0, -1); }
        }
        return null;
    }

    private Position CalculateDestinationFromEntrance(int x,int y)
    {
        List<FloorChip>[] Rooms = GameManager.instance.Rooms();
        int count = floorMap[x, y].RoomNum;
        Rooms[count] = Rooms[count].OrderBy(a => Guid.NewGuid()).ToList();
        foreach (var chip in Rooms[count])
        {
            if (chip.FloorEnum == FloorEnum.ENTRANCE)
            {
                if (chip.X != x || chip.Y != y)
                {
                    return new Position(chip.X, chip.Y);
                }
            }
        }
        return null;
    }

    private Position CalculateDestinationFromRoom(int x, int y)
    {
        List<FloorChip>[] Rooms = GameManager.instance.Rooms();
        int count = floorMap[x, y].RoomNum;
        Rooms[count] = Rooms[count].OrderBy(a => Guid.NewGuid()).ToList();
        foreach(var chip in Rooms[count])
        {
            if (chip.FloorEnum == FloorEnum.ENTRANCE)
            {
                return new Position(chip.X, chip.Y);
            }
        }
        return null;
    }

    private Position CalculateMoveDirection(int x,int y)
    {
        if (isReverse)
        {
            return CalculateMoveDirectionReverse(x, y);
        }
        switch (faceDirection)
        {
            case Direction.Up:
                if (CanMove(x, y +1)) { return new Position(0,1); }
                if (CanMove(x - 1, y)) { return new Position(- 1,0); }
                if (CanMove(x + 1, y)) { return new Position(1,0); }
                return new Position(0, -1);
            case Direction.Down:
                if (CanMove(x, y - 1)) { return new Position(0,-1); }
                if (CanMove(x + 1, y)) { return new Position(1, 0); }
                if (CanMove(x - 1, y)) { return new Position(-1, 0); }
                return new Position(0, 1);
            case Direction.Right:
                if (CanMove(x + 1, y)) { return new Position(1, 0); }
                if (CanMove(x, y + 1)) { return new Position(0, 1); }
                if (CanMove(x, y - 1)) { return new Position(0, -1); }
                return new Position(-1, 0);
            case Direction.Left:
                if (CanMove(x - 1, y)) { return new Position(-1, 0); }
                if (CanMove(x, y - 1)) { return new Position(0, -1); }
                if (CanMove(x, y + 1)) { return new Position(0, 1); }
                return new Position(1, 0);
            default:
                return null;
        }
    }

    private Position CalculateMoveDirectionReverse(int x,int y)
    {
        switch (faceDirection)
        {
            case Direction.Left:
                if (CanMove(x, y + 1)) { return new Position(0, 1); }
                if (CanMove(x - 1, y)) { return new Position(-1, 0); }
                if (CanMove(x, y - 1)) { return new Position(0, -1); }
                return new Position(1,0);
            case Direction.Right:
                if (CanMove(x, y - 1)) { return new Position(0, -1); }
                if (CanMove(x + 1, y)) { return new Position(1, 0); }
                if (CanMove(x, y + 1)) { return new Position(0, 1); }
                return new Position(-1,0);
            case Direction.Up:
                if (CanMove(x + 1, y)) { return new Position(1, 0); }
                if (CanMove(x, y + 1)) { return new Position(0, 1); }
                if (CanMove(x - 1, y)) { return new Position(-1, 0); }
                return new Position(0,-1);
            case Direction.Down:
                if (CanMove(x - 1, y)) { return new Position(-1, 0); }
                if (CanMove(x, y - 1)) { return new Position(0, -1); }
                if (CanMove(x + 1, y)) { return new Position(1, 0); }
                return new Position(0,1);
            default:
                return null;
        }
    }

    private bool CanMove(int x,int y)
    {
        return movingObjectMap[x, y] == null && !(floorMap[x, y].FloorEnum == FloorEnum.Wall);
    }


    private bool CanAttack(Player player)
    {
        //もし可能なら
        if (Utils.RandomJadge(probabilityForNormalAttack))
        {
            if (NormalSkill.CanNormalAttackByEnemy(this))
            {
                this.Finish();
                isRequestAttack = true;
                isActingAttack = true;
                return true;
            }
        }
        EffectiveSkillList = EffectiveSkillList.OrderBy(a => Guid.NewGuid()).ToList();
        //foreach(var skillEnum in EffectiveSkillList)
        if (EffectiveSkillList.Count >= 1)
        {
            SkillEnum skillEnum = EffectiveSkillList[0];
            switch (skillEnum)
            {
                case SkillEnum.Fire:
                    if (Fire.CanFireByEnemy(this))
                    {
                        PrepareAttack();
                        return true;
                    }
                    break;
                case SkillEnum.Burn:
                    if (Burn.CanBurnByEnemy(this))
                    {
                        PrepareAttack();
                        return true;
                    }
                    break;
                case SkillEnum.Water:
                    if (Water.CanWaterByEnemy(this))
                    {
                        PrepareAttack();
                        return true;
                    }
                    break;
                case SkillEnum.Sprash:
                    if (Sprash.CanSprashByEnemy(this))
                    {
                        PrepareAttack();
                        return true;
                    }
                    break;
                case SkillEnum.Wind:
                    if (Wind.CanWindByEnemy(this))
                    {
                        PrepareAttack();
                        return true;
                    }
                    break;
                case SkillEnum.Blast:
                    if (Blast.CanBlastByEnemy(this))
                    {
                        PrepareAttack();
                        return true;
                    }
                    break;
                case SkillEnum.Stone:
                    if (Stone.CanStoneByEnemy(this))
                    {
                        PrepareAttack();
                        return true;
                    }
                    break;
                case SkillEnum.Quake:
                    if (Quake.CanQuakeByEnemy(this))
                    {
                        PrepareAttack();
                        return true;
                    }
                    break;
                case SkillEnum.AttackUp:
                    if (AttackUp.CanAttackUpByEnemy(this))
                    {
                        PrepareAttack();
                        return true;
                    }
                    break;
                case SkillEnum.DefenceUp:
                    if (DefenceUp.CanDefenceUpByEnemy(this))
                    {
                        PrepareAttack();
                        return true;
                    }
                    break;
                case SkillEnum.AgilityUp:
                    if (AgilityUp.CanAgilityUpByEnemy(this))
                    {
                        PrepareAttack();
                        return true;
                    }
                    break;
                case SkillEnum.Heal:
                    if (Heal.CanHealByEnemy(this))
                    {
                        PrepareAttack();
                        return true;
                    }
                    break;
                case SkillEnum.Phoenix:
                    if (Phoenix.CanPhoenixByEnemy(this))
                    {
                        PrepareAttack();
                        return true;
                    }
                    break;
                case SkillEnum.Freeze:
                    if (Freeze.CanFreezeByEnemy(this))
                    {
                        PrepareAttack();
                        return true;
                    }
                    break;
                case SkillEnum.SpeedUp:
                    if (SpeedUp.CanSpeedUpByEnemy(this))
                    {
                        PrepareAttack();
                        return true;
                    }
                    break;
                case SkillEnum.Comet:
                    if (Comet.CanCometByEnemy(this))
                    {
                        PrepareAttack();
                        return true;
                    }
                    break;
                case SkillEnum.AreaHeal:
                    if (AreaHeal.CanAreaHealByEnemy(this))
                    {
                        PrepareAttack();
                        return true;
                    }
                    break;
                case SkillEnum.Explosion:
                    if (Explosion.CanExplosionByEnemy(this))
                    {
                        PrepareAttack();
                        return true;
                    }
                    break;
                case SkillEnum.BigBurn:
                    if (BigBurn.CanBigBurnByEnemy(this))
                    {
                        PrepareAttack();
                        return true;
                    }
                    break;
                case SkillEnum.SuperNova:
                    if (SuperNova.CanSuperNovaByEnemy(this))
                    {
                        PrepareAttack();
                        return true;
                    }
                    break;
                case SkillEnum.Volcano:
                    if (Volcano.CanVolcanoByEnemy(this))
                    {
                        PrepareAttack();
                        return true;
                    }
                    break;
                case SkillEnum.Meteo:
                    if (Meteo.CanMeteoByEnemy(this))
                    {
                        PrepareAttack();
                        return true;
                    }
                    break;
                case SkillEnum.Stome:
                    if (Stome.CanStomeByEnemy(this))
                    {
                        PrepareAttack();
                        return true;
                    }
                    break;
                case SkillEnum.Thundar:
                    if (Thundar.CanThundarByEnemy(this))
                    {
                        PrepareAttack();
                        return true;
                    }
                    break;
                case SkillEnum.Resurrection:
                    if (Resurrection.CanResurrectionByEnemy(this))
                    {
                        PrepareAttack();
                        return true;
                    }
                    break;
                case SkillEnum.HealVeil:
                    if (HealVeil.CanHealVeilByEnemy(this))
                    {
                        PrepareAttack();
                        return true;
                    }
                    break;
                default:
                    Debug.Log("EnemyのCanAttackでバグ" + skillEnum);
                    break;
            }
        }
        //}
       
        return false;
    }

    private void PrepareAttack()
    {
        this.Finish();
        isRequestAttack = true;
        isActingAttack = true;
    }

    public void EnemyWaitAnimation()
    {
        switch (faceDirection)
        {
            case Direction.Up:
                GetAnimator().Play("Enemy_Up_Wait");
                break;
            case Direction.Down:
                GetAnimator().Play("Enemy_Down_Wait");
                break;
            case Direction.Right:
                GetAnimator().Play("Enemy_Right_Wait");
                break;
            case Direction.Left:
                GetAnimator().Play("Enemy_Left_Wait");
                break;
        }
    }

    public void Attack()
    {
        switch (faceDirection)
        {
            case Direction.Up:
                GetAnimator().Play("Enemy_Up_Skill");
                break;
            case Direction.Down:
                GetAnimator().Play("Enemy_Down_Skill");
                break;
            case Direction.Right:
                GetAnimator().Play("Enemy_Right_Skill");
                break;
            case Direction.Left:
                GetAnimator().Play("Enemy_Left_Skill");
                break;
        }

        switch (prepareSkillEnum)
        {
            case SkillEnum.NormalAttack:
                NormalSkill.NormalAttackByEnemy(this, movingObjectMap[targetX, targetY]);
                break;
            case SkillEnum.Fire:
                Fire.FireByEnemy(this,movingObjectMap[targetX, targetY]);
                break;
            case SkillEnum.Burn:
                Burn.BurnByEnemy(this, movingObjectMap[targetX, targetY]);
                break;
            case SkillEnum.Water:
                Water.WaterByEnemy(this,movingObjectMap[targetX,targetY]);
                break;
            case SkillEnum.Sprash:
                Sprash.SprashByEnemy(this, movingObjectMap[targetX, targetY]);
                break;
            case SkillEnum.Wind:
                Wind.WindByEnemy(this,movingObjectMap[targetX, targetY]);
                break;
            case SkillEnum.Stone:
                Stone.StoneByEnemy(this, movingObjectMap[targetX, targetY]);
                break;
            case SkillEnum.Quake:
                Quake.QuakeByEnemy(this, movingObjectMap[targetX, targetY]);
                break;
            case SkillEnum.Blast:
                Blast.BlastByEnemy(this, movingObjectMap[targetX, targetY]);
                break;
            case SkillEnum.AttackUp:
                AttackUp.AttackUpByEnemy(this);
                break;
            case SkillEnum.DefenceUp:
                DefenceUp.DefenceUpByEnemy(this);
                break;
            case SkillEnum.AgilityUp:
                AgilityUp.AgilityUpByEnemy(this);
                break;
            case SkillEnum.Heal:
                Heal.HealByEnemy(this);
                break;
            case SkillEnum.Phoenix:
                Phoenix.PhoenixByEnemy(this);
                break;
            case SkillEnum.Freeze:
                Freeze.FreezeByEnemy(this, movingObjectMap[targetX, targetY]);
                break;
            case SkillEnum.SpeedUp:
                SpeedUp.SpeedUpByEnemy(this);
                break;
            case SkillEnum.Comet:
                Comet.CometByEnemy(this, movingObjectMap[targetX, targetY]);
                break;
            case SkillEnum.AreaHeal:
                AreaHeal.AreaHealByEnemy(this);
                break;
            case SkillEnum.Explosion:
                Explosion.ExplosionByEnemy(this, movingObjectMap[targetX, targetY]);
                break;
            case SkillEnum.BigBurn:
                BigBurn.BigBurnByEnemy(this, movingObjectMap[targetX, targetY]);
                break;
            case SkillEnum.SuperNova:
                SuperNova.SuperNovaByEnemy(this, movingObjectMap[targetX, targetY]);
                break;
            case SkillEnum.Volcano:
                Volcano.VolcanoByEnemy(this, movingObjectMap[targetX, targetY]);
                break;
            case SkillEnum.Meteo:
                Meteo.MeteoByEnemy(this, movingObjectMap[targetX, targetY]);
                break;
            case SkillEnum.Stome:
                Stome.StomeByEnemy(this, movingObjectMap[targetX, targetY]);
                break;
            case SkillEnum.Thundar:
                Thundar.ThundarByEnemy(this, movingObjectMap[targetX, targetY]);
                break;
            case SkillEnum.Resurrection:
                Resurrection.RessurectionByEnemy(this);
                break;
            case SkillEnum.HealVeil:
                HealVeil.HealVeilByEnemy(this);
                break;
            case SkillEnum.None:
                Debug.Log("EnemyのAttackでバグ");
                return;
        }
        prepareSkillEnum = SkillEnum.None;
        StartCoroutine(InsuranceOfAttack());

    }

    private IEnumerator InsuranceOfAttack()
    {
        yield return new WaitForSeconds(5.0f);
        this.FinishSkill();
    }

}
