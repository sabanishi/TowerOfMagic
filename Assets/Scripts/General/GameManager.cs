using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public static int TopLevel = 31;

    public float LevelShowTime=2.0f;
    private int level = 0;
    public int GetLevel()
    {
        return level;
    }
    private bool isSetUp;
    private bool isFinish;
    private bool isGameQuit;

    public BoardManager boardManager;

    public int playerFoodPoints = 100;
    [HideInInspector] private bool isPlayerTurn = true;

    public float turnDelay;//Enemyの動作時間
    private List<Enemy> enemies;
    public List<Enemy> GetEnemies()
    {
        return enemies;
    }
    private bool enemiesMoving;
    private PlayerController playerController;
    public PlayerController GetPlayerController()
    {
        return playerController;
    }

    private FloorChip[,] floorMap;
    public FloorChip[,] FloorMap()
    {
        return floorMap;
    }
    private MovingObject[,] movingObjectMap;
    public MovingObject[,] MovingObjectMap()
    {
        return movingObjectMap;
    }
    private List<FloorChip>[] rooms;
    public List<FloorChip>[] Rooms()
    {
        return rooms;
    }

    private ItemScriptForField[,] itemMap;
    public ItemScriptForField[,] ItemMap()
    {
        return itemMap;
    }
    public void SetMap(FloorChip[,] floormap,List<FloorChip>[] _rooms,MovingObject[,] movingObjectmap,ItemScriptForField[,] itemmap)
    {
        floorMap = floormap;
        rooms = _rooms;
        movingObjectMap = movingObjectmap;
        itemMap = itemmap;
    }

    private MovingObjectInfomation playerInfo1; public MovingObjectInfomation GetPlayerInfo1() { return playerInfo1; }
    private MovingObjectInfomation playerInfo2; public MovingObjectInfomation GetPlayerInfo2() { return playerInfo2; }
    private MovingObjectInfomation playerInfo3; public MovingObjectInfomation GetPlayerInfo3() { return playerInfo3; }
    public MovingObjectInfomation NowPlayerInfo;
    public void SetPlayerInfo(MovingObjectInfomation info1, MovingObjectInfomation info2, MovingObjectInfomation info3)
    {
        playerInfo1 = info1;
        playerInfo2 = info2;
        playerInfo3 = info3;
    }

    public MenuManager menuManager;
    public InfoManager infoManager;

    public ItemChest ItemChest { get; private set; }

    [SerializeField] private GameObject GameOverPanelPrefab;
    [SerializeField] private GameObject GameClearPanelPrefab;

    public int turnCount;

    private bool isShouldCreateMonsterHouse;
    private bool isAlreadyCreateMonsterHouse;
    public void CheckIsPlayerInMonsterHouse()
    {
        if (!isAlreadyCreateMonsterHouse)
        {
            Player player = playerController.GetPlayer();
            int posX = (int)player.transform.position.x;
            int posY = (int)player.transform.position.y;

            foreach (var room in rooms)
            {
                if (room[0].IsMonsterHouse)
                {
                    foreach (var chip in room)
                    {
                        if (chip.X == posX && chip.Y == posY)
                        {
                            isShouldCreateMonsterHouse = true;
                            return;
                        }
                    }
                    isShouldCreateMonsterHouse = false;
                    return;
                }
            }
            isShouldCreateMonsterHouse = false;
        }
        isShouldCreateMonsterHouse = false;
    }

    private bool isUpdate = true;

    private void Awake()
    {
        SoundManager.instance.PlayBGM(SoundManager.BGM_Type.Stage);
        if (instance == null)
        {
            instance = this;
            ItemChest = new ItemChest();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        // InitGame();
    }

    private void InitGame()
    {
        isUpdate = true;
        isAlreadyCreateMonsterHouse = false;
        isShouldCreateMonsterHouse = false;
        if (level != TopLevel)
        {
            enemies = new List<Enemy>();

            boardManager = GetComponent<BoardManager>();
            if (GameObject.Find("MenuManager") != null)
            {
                menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
                menuManager.gameObject.SetActive(false);
            }
            if (GameObject.Find("InfoManager") != null)
            {
                infoManager = GameObject.Find("InfoManager").GetComponent<InfoManager>();
                infoManager.SetGameManager(this);
            }

            GameObject LevelPanel = GameObject.Find("LevelPanel");
            Text LevelText = GameObject.Find("LevelText").GetComponent<Text>();
            isSetUp = true;
            LevelText.text = "現在地:" + level + "階";
            LevelPanel.SetActive(true);
            Invoke("HideLevelPanel", LevelShowTime);

            enemies.Clear();
            boardManager.SetupScene(level);//このタイミングでGameManagerのMapも生成されている

            if (level == 1)
            {
               // level = 15;
                SetPlayerInfo(SoundManager.PlayerInfomations[0], SoundManager.PlayerInfomations[1], SoundManager.PlayerInfomations[2]);
                playerController.GetPlayer().SetNowMovingObjectInfo(playerInfo1);
            }
            else
            {
                playerController.GetPlayer().SetNowMovingObjectInfo(NowPlayerInfo);
            }
            turnCount = 1;
        }
        else
        {
            GameObject LevelPanel = GameObject.Find("LevelPanel");
            Text LevelText = GameObject.Find("LevelText").GetComponent<Text>();
            isSetUp = true;
            LevelText.text = "現在地:最上階";
            LevelPanel.SetActive(true);
            GameClearDeal();
        }
    }

    private void HideLevelPanel()
    {
        GameObject LevelPanel = GameObject.Find("LevelPanel");
        LevelPanel.SetActive(false);
        isSetUp = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("QuitGame"))
        {
            isGameQuit = true;
            isFinish = true;
            SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
            SceneManager.LoadScene("title");
            Destroy(GameManager.instance.gameObject);
            return;
        }
        if (isGameQuit)
        {
            return;
        }
        if (!isUpdate)
        {
            return;
        }
        if (!isSetUp)
        {
            if (isPlayerTurn)
            {
                if (!playerController.GetPlayer().IsMove())
                {
                    if (playerController.GetPlayer().IsCondition(PlayerCondition.SpeedDown))
                    {
                        if (!playerController.GetPlayer().IsMoveFinish())
                        {
                            if (!playerController.GetPlayer().FragForSpeedDown)
                            {
                                playerController.GetPlayer().FragForSpeedDown = true;
                                playerController.GetPlayer().SkipTurn();
                            }
                            else
                            {
                                playerController.PlayerMove();
                            }
                        } 
                    }
                    else
                    {
                        if (!playerController.GetPlayer().IsMoveFinish())
                        {
                            playerController.PlayerMove();
                        }
                    }
                }
                if (playerController.GetPlayer().IsMoveFinish())
                {
                    if (playerController.GetPlayer().IsActingAttack())
                    {
                        if (playerController.GetPlayer().IsRequestAttack())
                        {
                            playerController.GetPlayer().Attack();
                            playerController.GetPlayer().SetIsRequestAttack(false);
                        }
                        return;
                    }
                    playerController.GetPlayer().SetIsMoveFinish(false);
                    if (playerController.GetPlayer().IsCondition(PlayerCondition.SpedUp))
                    {
                        if (!playerController.GetPlayer().FragForSpeedUp)
                        {
                            playerController.GetPlayer().FragForSpeedUp = true;
                        }
                        else
                        {
                            playerController.GetPlayer().FragForSpeedUp= false;
                            isPlayerTurn = false;
                            turnCount++;
                            if (isShouldCreateMonsterHouse)
                            {
                                isPlayerTurn = true;
                                CreateMonsterHouse();
                            }
                        }
                    }
                    else
                    {
                        isPlayerTurn = false;
                        turnCount++;
                        if (isShouldCreateMonsterHouse)
                        {
                            isPlayerTurn = true;
                            CreateMonsterHouse();
                        }
                    }

                    if (turnCount % 40 == 0)
                    {
                        CreateEnemy();
                    }
                    if (turnCount % 5 == 0)
                    {
                        if (GetPlayerController().GetPlayer().GetNowMovingObjectInfo().Hp < GetPlayerController().GetPlayer().GetNowMovingObjectInfo().MaxHp)
                        {
                            GetPlayerController().GetPlayer().GetNowMovingObjectInfo().Hp ++;
                        }
                    }
                    if (turnCount % 10 == 0)
                    {
                        if (GetPlayerController().GetPlayer().GetNowMovingObjectInfo().Mp < GetPlayerController().GetPlayer().GetNowMovingObjectInfo().MaxMp)
                        {
                            GetPlayerController().GetPlayer().GetNowMovingObjectInfo().Mp++;
                        }
                    }
                    int[] turns = GetPlayerController().GetPlayer().GetConditionTurns();
                    if (turns[0] > 0)
                    {
                        turns[0]--;
                    }
                    if (turns[1] > 0)
                    {
                        turns[1]--;
                    }
                    if (turns[2] > 0)
                    {
                        turns[2]--;
                    }
                    if (playerController.GetPlayer().IsCondition(PlayerCondition.HealVeil))
                    {
                        playerController.GetPlayer().HealHp((int)(playerController.GetPlayer().GetNowMovingObjectInfo().MaxHp * playerController.GetPlayer().HealVeilProperity));
                    }
                }
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].EnemyWaitAnimation();
                }
            }
            else
            {
                if (enemies.Count != 0)
                {
                    bool isAllMoveFinish = true;
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        Enemy enemy = enemies[i];
                        if (!enemy.IsMove())
                        {
                            if (enemy.IsCondition(PlayerCondition.SpeedDown))
                            {
                                if (!enemy.IsMoveFinish())
                                {
                                    if (!enemy.FragForSpeedDown)
                                    {
                                        enemy.FragForSpeedDown = true;
                                        enemy.SkipTurn();
                                    }
                                    else
                                    {
                                        enemy.FragForSpeedDown = false;
                                        enemy.MoveEnemy();
                                        
                                    }
                                }
                            }
                            else
                            {
                                if (!enemy.IsMoveFinish())
                                {
                                    enemy.MoveEnemy();
                                }
                            }
                        }
                        if (!enemy.IsMoveFinish())
                        {
                            isAllMoveFinish = false;
                        }
                    }
                    if (isAllMoveFinish)
                    {
                        for (int i = 0; i < enemies.Count; i++)
                        {
                            Enemy enemy = enemies[i];
                            if (enemy.IsActingAttack())
                            {
                                if (enemy.IsRequestAttack())
                                {
                                    enemy.SetIsRequestAttack(false);
                                    enemy.Attack();
                                }
                                return;
                            }
                        }
                        isPlayerTurn = true;
                        for (int i = 0; i < enemies.Count; i++)
                        {
                            enemies[i].SetIsMoveFinish(false);
                            int[] turns = enemies[i].GetConditionTurns();
                            if (turns[0] > 0)
                            {
                                turns[0]--;
                            }
                            if (turns[1] > 0)
                            {
                                turns[1]--;
                            }
                            if (turns[2] > 0)
                            {
                                turns[2]--;
                            }
                        }
                        for (int i = 0; i < enemies.Count; i++)
                        {
                            Enemy enemy = enemies[i];
                            if (enemy.IsCondition(PlayerCondition.HealVeil))
                            {
                                enemy.HealHp((int)(enemy.GetNowMovingObjectInfo().MaxHp*enemy.HealVeilProperity));
                            }
                        }
                        if (isShouldCreateMonsterHouse)
                        {
                            CreateMonsterHouse();
                        }
                    }
                }
                else
                {
                    Hashtable delayHash = new Hashtable();
                    delayHash.Add("time", GameManager.instance.turnDelay);
                    delayHash.Add("oncompletetarget", gameObject);
                    delayHash.Add("oncomplete", "DelayAction");
                    iTween.MoveTo(gameObject, delayHash);
                }
            }
        }
    }

    private void CreateMonsterHouse()
    {
        StartCoroutine(CreateMonsterHouseCorutine());
    }

    private IEnumerator CreateMonsterHouseCorutine()
    {
        if (isAlreadyCreateMonsterHouse) { yield break; }

        isUpdate = false;
        isAlreadyCreateMonsterHouse = true;
        foreach (var room in rooms)
        {
            if (room[0].IsMonsterHouse)
            {
                SoundManager.instance.PlaySE(SoundManager.SE_Type.monsterHouseVoice);
                SoundManager.instance.PlayBGM(SoundManager.BGM_Type.MonsterHouse);
                DialogManager.instance.SetText("モンスターハウスだ！！！");
                foreach (var chip in room)
                {
                    if (chip.FloorEnum == FloorEnum.ROOM)
                    {
                        bool isCreate = Utils.RandomJadge(0.6f);
                        if (isCreate)
                        {
                            boardManager.CreateEnemyForMonsterHouse(chip.X, chip.Y);
                            yield return new WaitForSeconds(0.1f);
                        }
                    }
                }
                break;
            }
        }
        isUpdate = true;
    }


    public void DieDealForPlayer()
    {
        playerController.GetPlayer().Finish();
        playerController.GetPlayer().SetIsMoveFinish(false);
        for(int i = 0; i < enemies.Count; i++)
        {
            Enemy enemy = enemies[i];
            enemy.SetIsRequestAttack(false);
            enemy.Finish();
            enemy.SetIsMoveFinish(false);
        }
        isPlayerTurn = true;
    }

    private void CreateEnemy()
    {
        boardManager.CreateEnemy();
    }

    public void DelayAction()
    {
        isPlayerTurn = true;
    }

    public void AddEnemyToList(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public void AddPlayer(PlayerController player)
    {
        this.playerController = player;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void CallbackInitialization()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void OnSceneLoaded(Scene arr0,LoadSceneMode arg1)
    {
        if (instance != null)
        {
            if (!instance.isFinish)
            {
                instance.level++;
                instance.InitGame();
            }
        }
    }

    public bool PlayerDieDeal()
    {
        isPlayerTurn = true;
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].SetIsMoveFinish(false);
        }
        GetPlayerController().SetIsActive(false);
        menuManager.Setting();
        return menuManager.PlayerDieDeal(playerInfo1,playerInfo2,playerInfo3);
    }

    public void GameOverDeal()
    {
        isFinish = true;
        SoundManager.instance.StopBGM();
        Instantiate(instance.GameOverPanelPrefab,GameObject.Find("Canvas").transform);
        Invoke("GameOverVoiceStart", 2.0f);
        Invoke("GoGameOverScene", 10.0f);
    }

    public void GameClearDeal()
    {
        isFinish = true;
        SoundManager.instance.StopBGM();
        Invoke("GameClearVoiceStart", 0.5f);
        //Invoke("GameClearTextShow", 9.0f);

        Invoke("GoGameClearScene", 22.0f);
    }

    public void GameClearVoiceStart()
    {
        SoundManager.instance.PlayBGM(SoundManager.BGM_Type.GameClear);
    }

    public void GameClearTextShow()
    {
        Instantiate(instance.GameClearPanelPrefab, GameObject.Find("Canvas").transform);
    }

    public void GoGameClearScene()
    {
        
        isSetUp = true;
        SceneManager.LoadScene("gameClear");
    }

    public void GameOverVoiceStart()
    {
        SoundManager.instance.PlaySE(SoundManager.SE_Type.gameOverVoice);
    }

    private void GoGameOverScene()
    {
        SoundManager.instance.PlayBGM(SoundManager.BGM_Type.GameOver);
        isSetUp = true;
        SceneManager.LoadScene("gameover");
    }
}
