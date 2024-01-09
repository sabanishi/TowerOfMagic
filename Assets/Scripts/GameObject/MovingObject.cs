using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class MovingObject : MonoBehaviour
{
    [SerializeField] protected Text ConditionText;
    public LayerMask blockingLayer;

    private BoxCollider2D boxCollider;
    protected Rigidbody2D rb;
    public GameObject Mahouzin;

    protected Direction faceDirection;
    public Direction GetFaceDirection()
    {
        return faceDirection;
    }
    public void SetFaceDirection(Direction direction)
    {
        faceDirection = direction;
    }

    protected Animator animator;
    public Animator GetAnimator()
    {
        return animator;
    }

    private SpriteRenderer sprite;

    private bool isMove;
    public bool IsMove()
    {
        return isMove;
    }
    public void SetIsMove(bool _isMove)
    {
        isMove = _isMove;
    }

    private bool isPlayer;
    public bool IsPlayer()
    {
        return isPlayer;
    }
    public void SetIsPlayer(bool isPlayer)
    {
        this.isPlayer = isPlayer;
    }

    private bool isMoveFinish;
    public bool IsMoveFinish()
    {
        return isMoveFinish;
    }
    public void SetIsMoveFinish(bool isMoveFinish)
    {
        this.isMoveFinish = isMoveFinish;
    }

    private MovingObjectInfomation nowMovingObjectInfo;
    public void SetNowMovingObjectInfo(MovingObjectInfomation info)
    {
        nowMovingObjectInfo = info;
        sprite.material = info.material;
    }
    public MovingObjectInfomation GetNowMovingObjectInfo()
    {
        return nowMovingObjectInfo;
    }

    protected bool isRequestAttack;
    public bool IsRequestAttack()
    {
        return isRequestAttack;
    }
    public void SetIsRequestAttack(bool isRequest)
    {
        isRequestAttack = isRequest;
    }
    protected bool isActingAttack;
    public bool IsActingAttack()
    {
        return isActingAttack;
    }
    public void SetIsActingAttack(bool isFinish)
    {
        isActingAttack = isFinish;
    }

    public bool IsOnMap;

    protected SkillEnum prepareSkillEnum;
    public void SetPrepareSkillEnum(SkillEnum _skillEnum)
    {
        prepareSkillEnum = _skillEnum;
    }

    private PlayerCondition[] Conditions = new PlayerCondition[3];
    private int[] ConditionTurns = new int[3];
    public PlayerCondition[] GetConditions()
    {
        return Conditions;
    }
    public int[] GetConditionTurns()
    {
        return ConditionTurns;
    }

    public void AddCondition(PlayerCondition condition)
    {
        if (Conditions[0] == PlayerCondition.None)
        {
            Conditions[0] = condition;
            ConditionTurns[0] = 20;
        }else if (Conditions[1] == PlayerCondition.None)
        {
            Conditions[1] = condition;
            ConditionTurns[1] = 20;
        }
        else if (Conditions[2] == PlayerCondition.None)
        {
            Conditions[2] = condition;
            ConditionTurns[2] = 20;
        }
        else
        {
            Conditions[0] = Conditions[1];
            ConditionTurns[0] = ConditionTurns[1];
            Conditions[1] = Conditions[2];
            ConditionTurns[0] = ConditionTurns[2];
            Conditions[2] = condition;
            ConditionTurns[2] = 20;
        }
    }
    public bool IsCondition(PlayerCondition condition)
    {
        if (Conditions[0] == condition || Conditions[1] == condition || Conditions[2] == condition)
        {
            return true;
        }
        return false;
    }

    public bool CureCondition(PlayerCondition condition)
    {
        bool isTrue = IsCondition(condition);
        if (isTrue)
        {
            if (Conditions[0].Equals(condition))
            {
                ConditionTurns[0] = 0;
             }
            if (Conditions[1].Equals(condition))
            {
                ConditionTurns[1] = 0;
            }
            if (Conditions[2].Equals(condition))
            {
                ConditionTurns[2] = 0;
            }
        }

        return isTrue;
    }

    public bool FragForSpeedDown;
    public bool FragForSpeedUp;

    public float HealVeilProperity;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        faceDirection = Direction.Down;
        animator = GetComponent<Animator>();
        gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 1);
        IsOnMap = false;
        prepareSkillEnum = SkillEnum.None;
    }

    public virtual void Update()
    {
        ConditionText.text = "";
        if (Conditions[2] != PlayerCondition.None)
        {
            ConditionText.text += PlayerConditionManager.ReturnShowTextFromPlayerCondition(Conditions[2]);
        }
        if (Conditions[1] != PlayerCondition.None)
        {
            ConditionText.text += PlayerConditionManager.ReturnShowTextFromPlayerCondition(Conditions[1]);
        }
        if (Conditions[0] != PlayerCondition.None)
        {
            ConditionText.text += PlayerConditionManager.ReturnShowTextFromPlayerCondition(Conditions[0]);
        }
        if (ConditionTurns[0] <= 0)
        {
            Conditions[0] = PlayerCondition.None;
            ConditionTurns[0] = 0;
        }
        if (ConditionTurns[1] <= 0)
        {
            Conditions[1] = PlayerCondition.None;
            ConditionTurns[1] = 0;
        }
        if (ConditionTurns[2] <= 0)
        {
            Conditions[2] = PlayerCondition.None;
            ConditionTurns[2] = 0;
        }
    }

    protected void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public virtual bool Move(int xDir, int yDir)
    {
        //現在地
        Vector3 start = transform.position;
        //目的地
        Vector3 end = start + new Vector3(xDir, yDir);

        DecideDirection(xDir, yDir);
        MovingObject[,] movingObjectMap= GameManager.instance.MovingObjectMap();
        FloorChip[,] floorMap = GameManager.instance.FloorMap();
        ItemScriptForField[,] itemMap = GameManager.instance.ItemMap();

        if (movingObjectMap[(int)end.x, (int)end.y] == null&& !(floorMap[(int)end.x, (int)end.y].FloorEnum==FloorEnum.Wall))
        {
            if(itemMap[(int)end.x, (int)end.y] != null)
            {
                if (IsPlayer())
                {
                    StartCoroutine(ItemGet(itemMap[(int)end.x, (int)end.y], (int)end.x, (int)end.y));
                }
            }
            isMove = true;
            Vector3 playerPosition = transform.position;
            Hashtable moveHash = new Hashtable();
            moveHash.Add("position", new Vector3(playerPosition.x +xDir, playerPosition.y +yDir, playerPosition.z));
            moveHash.Add("time", GameManager.instance.turnDelay);
            moveHash.Add("delay", 0);
            moveHash.Add("oncompletetarget", gameObject);
            moveHash.Add("oncomplete", "Finish");
            iTween.MoveTo(gameObject, moveHash);

            movingObjectMap[(int)end.x, (int)end.y] = this;
            movingObjectMap[(int)start.x, (int)start.y] = null;
            return true;
        }
        return false;
    }

    private IEnumerator ItemGet(ItemScriptForField script,int x,int y)
    {
        yield return new WaitForSeconds(GameManager.instance.turnDelay / 5 * 3);
        if (((Player)this).GetItemChest().AddItem(script.itemEnum))
        {
            DialogManager.instance.ItemgetFormat(ItemConstractParent.ReturnNameFromItemEnum(script.itemEnum));
            int r = Utils.GetRandomInt(1, 3);
            if (r == 1)
            {
                SoundManager.instance.PlaySE(SoundManager.SE_Type.itemGet1);
            }
            else if (r == 2)
            {
                SoundManager.instance.PlaySE(SoundManager.SE_Type.itemGet2);
            }
            else
            {
                SoundManager.instance.PlaySE(SoundManager.SE_Type.itemGet3);
            }
            Destroy(script.gameObject);
            GameManager.instance.ItemMap()[x, y] = null;
        }
        else
        {
            SoundManager.instance.PlaySE(SoundManager.SE_Type.cannotPickUpItem);
            DialogManager.instance.SetText("アイテム:<color=#00ff00>" + ItemConstractParent.ReturnNameFromItemEnum(script.itemEnum) + "</color>　を拾おうとした");
            DialogManager.instance.SetText("しかし　持ち物が　いっぱいで拾えなかった!");
        }
    }

    public void SkipTurn()
    {
        isMove = true;

        Hashtable skipHash = new Hashtable();
        skipHash.Add("time", GameManager.instance.turnDelay/2);
        skipHash.Add("oncompletetarget", gameObject);
        skipHash.Add("oncomplete", "FinishForSkipTurn");
        iTween.MoveTo(gameObject,skipHash);
    }

    private void FinishForSkipTurn()
    {
        isMove = false;
        isMoveFinish = true;
    }

    public virtual void Finish()
    {
        isMove = false;
        isMoveFinish = true;
        if (FragForSpeedDown)
        {
            FragForSpeedDown = false;
        }
    }

    public virtual void FinishSkill()
    {
        Debug.Log("FinishSkill");
        isActingAttack = false;
        Mahouzin.SetActive(false);
    }

    public void DecideDirection(int horizontal, int vertical)
    {
        Direction newface = faceDirection;
        if (horizontal == 1)
        {
            newface = Direction.Right;
        }
        else if (horizontal == -1)
        {
            newface = Direction.Left;
        }
        if (vertical == 1)
        {
            newface = Direction.Up;
        }
        else if (vertical == -1)
        {
            newface = Direction.Down;
        }
        faceDirection = newface;
    }

    public void OnDamage(int damage)
    {
        if (GetNowMovingObjectInfo().Hp > 0)
        {
            int afterHp = GetNowMovingObjectInfo().Hp - damage;
            if (afterHp < 0)
            {
                afterHp = 0;
            }
            GetNowMovingObjectInfo().Hp = afterHp;
            if (afterHp <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        int random = Random.Range(0, 3);
        DialogManager.instance.DieFormat(GetNowMovingObjectInfo().Name);
        if (this.gameObject.tag == "Player")
        {
            if (random == 0)
            {
                SoundManager.instance.PlaySE(SoundManager.SE_Type.playerDie1);
            }
            else if (random == 1)
            {
                SoundManager.instance.PlaySE(SoundManager.SE_Type.playerDie2);
            }
            else
            {
                SoundManager.instance.PlaySE(SoundManager.SE_Type.playerDie3);
            }
            if (!GameManager.instance.PlayerDieDeal())
            {
                //全員死んでた時
                GameManager.instance.GameOverDeal();
            }
            FragForSpeedDown = true;
            GameManager.instance.DieDealForPlayer();
        }
        else
        {
            if (!EnemyDieExpDeal()){
                if (random == 0)
                {
                    SoundManager.instance.PlaySE(SoundManager.SE_Type.enemyDie1);
                }
                else if (random == 1)
                {
                    SoundManager.instance.PlaySE(SoundManager.SE_Type.enemyDie2);
                }
                else
                {
                    SoundManager.instance.PlaySE(SoundManager.SE_Type.enemyDie3);
                }
            }

            GameManager.instance.GetEnemies().Remove((Enemy)this);

            StartCoroutine(EnemyDieEffect());
           
        }
    }

    private bool EnemyDieExpDeal()
    {
        bool isLevelUp=false;
        MovingObjectInfomation info1 = GameManager.instance.GetPlayerInfo1();
        MovingObjectInfomation info2 = GameManager.instance.GetPlayerInfo2();
        MovingObjectInfomation info3 = GameManager.instance.GetPlayerInfo3();
        if (info1.Hp > 0)
        {
            if (AddExp(info1))
            {
                isLevelUp = true;
            }
            
        }
        if (info2.Hp > 0)
        {
            if (AddExp(info2))
            {
                isLevelUp = true;
            }
        }
        if (info3.Hp > 0)
        {
            if (AddExp(info3))
            {
                isLevelUp = true;
            }
        }
        if (isLevelUp)
        {
            int r = Utils.GetRandomInt(1, 3);
            if (r == 1)
            {
                SoundManager.instance.PlaySE(SoundManager.SE_Type.levelUp1);
            }else if (r == 2)
            {
                SoundManager.instance.PlaySE(SoundManager.SE_Type.levelUp2);
            }
            else
            {
                SoundManager.instance.PlaySE(SoundManager.SE_Type.levelUp3);
            }
        }
        return isLevelUp;
    }

    private bool AddExp(MovingObjectInfomation info)
    {
        long exp= GetNowMovingObjectInfo().DropExpData[GetNowMovingObjectInfo().Level];
        if (info.Level > GetNowMovingObjectInfo().Level+1)
        {
            exp /= 2;
        }
        if(info.Level > GetNowMovingObjectInfo().Level + 2)
        {
            exp /= 2;
        }
        if(info.Level > GetNowMovingObjectInfo().Level + 3)
        {
            exp /= 2;
        }
        if(info.Level > GetNowMovingObjectInfo().Level + 4)
        {
            exp /= 2;
        }
        DialogManager.instance.AddExpFormat(info.Name,exp);
        if (info.AddExp(exp))
        {
            DialogManager.instance.LevelUpFormat(info.Name, info.Level);
            return true;
        }
        return false;
    }

    private IEnumerator EnemyDieEffect()
    {
        for(float f = 1.0f; f >= 0; f -= Time.deltaTime)
        {
            int fadeCount = (int)(f * 1000);
            int count = fadeCount % 200;
            float alpha = 0;
            if (count >= 100)
            {
                alpha = (float)(count - 100) / 100;
            }
            else
            {
                alpha = (float)(100 - count) / 100;
            }
            sprite.color = new Color(alpha,alpha,alpha,1);
            yield return null;
        }
        Destroy(gameObject);
    }

    protected void AttackMiss()
    {
        SoundManager.instance.PlaySE(SoundManager.SE_Type.cannotAttack);
        SoundManager.instance.PlaySE(SoundManager.SE_Type.missChant);
        Hashtable attackMissHash = new Hashtable();
        attackMissHash.Add("time", 0.5f);
        attackMissHash.Add("delay", 0);
        attackMissHash.Add("oncompletetarget", gameObject);
        attackMissHash.Add("oncomplete", "FinishSkill");
        iTween.MoveTo(gameObject, attackMissHash);
        DialogManager.instance.SetText("しかし　攻撃は失敗した！");
    }

    public void AttackMissForMp()
    {
        SoundManager.instance.PlaySE(SoundManager.SE_Type.excessMp);
        SoundManager.instance.PlaySE(SoundManager.SE_Type.missChant);
        Hashtable attackMissHash = new Hashtable();
        attackMissHash.Add("time", 0.5f);
        attackMissHash.Add("delay", 0);
        attackMissHash.Add("oncompletetarget", gameObject);
        attackMissHash.Add("oncomplete", "FinishSkill");
        iTween.MoveTo(gameObject, attackMissHash);
        DialogManager.instance.SetText("しかし　MPが足りない！");
    }

    public void HealHp(int healPoint)
    {
        int beforeHp = GetNowMovingObjectInfo().Hp;
        if (GetNowMovingObjectInfo().Hp < GetNowMovingObjectInfo().MaxMp)
        {
            GetNowMovingObjectInfo().Hp += healPoint;
            if (GetNowMovingObjectInfo().Hp > GetNowMovingObjectInfo().MaxHp)
            {
                GetNowMovingObjectInfo().Hp = GetNowMovingObjectInfo().MaxHp;
            }
            DialogManager.instance.HealFormat(GetNowMovingObjectInfo().Name, GetNowMovingObjectInfo().Hp - beforeHp);
            SoundManager.instance.PlaySE(SoundManager.SE_Type.healEffect);
        }
    }

    public void HealMp(int healMp)
    {
        int beforeMp = GetNowMovingObjectInfo().Mp;
        if (GetNowMovingObjectInfo().Mp < GetNowMovingObjectInfo().MaxMp)
        {
            GetNowMovingObjectInfo().Mp += healMp;
            if (GetNowMovingObjectInfo().Mp > GetNowMovingObjectInfo().MaxMp)
            {
                GetNowMovingObjectInfo().Mp = GetNowMovingObjectInfo().MaxMp;
            }
            DialogManager.instance.HealMpFormat(GetNowMovingObjectInfo().Name, GetNowMovingObjectInfo().Mp - beforeMp);
            SoundManager.instance.PlaySE(SoundManager.SE_Type.healEffect);
        }
    }

    private IEnumerator WarpDeal()
    {
        yield return new WaitForSeconds(1.25f);
        FinishSkill();
    }
}
