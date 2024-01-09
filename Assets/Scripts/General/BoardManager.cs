using UnityEngine;
using System;
using System.Collections.Generic;
using Cinemachine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
	//カウント用のクラスを設定
	[Serializable]
	public class Count
	{
		public int minimum;
		public int maximum;

		public Count(int min, int max)
		{
			minimum = min;
			maximum = max;
		}
	}

	public int columns = 10;//縦列
	public int rows = 10;//横列
	public int maxRoom = 20;//部屋の最大個数

	public GameObject Player;
	public GameObject Exit;
	public GameObject[] floorTiles;
	public GameObject enemyTile;
	public GameObject[] wallTiles;
	public GameObject magicItemTile;
	public GameObject portionItemTile;
	private Transform boardHolder;
	private List<Vector3> gridPositions = new List<Vector3>();

	private MapGenerator mapGenerator;

	private CinemachineVirtualCamera _camera;

	private Vector3 RandomPosition()
	{
		//0〜36からランダムで1つ決定し、位置情報を確定
		int randomIndex = Random.Range(0, gridPositions.Count);
		Vector3 randomPosition = gridPositions[randomIndex];
		//ランダムで決定した数値は削除
		gridPositions.RemoveAt(randomIndex);
		//確定した位置情報を返す
		return randomPosition;
	}

	public void SetupScene(int level)
	{
		_camera = GameObject.Find("CMvcam1").GetComponent<CinemachineVirtualCamera>();

		//Boardというオブジェクトを作成し、transform情報をboardHolderに保存
		boardHolder = new GameObject("Board").transform;
		gridPositions.Clear();

		mapGenerator = new MapGenerator();
		FloorChip[,] map = mapGenerator.GenerateMap(columns, rows, maxRoom);

		for (int x = 0; x < map.GetLength(0); x++)
		{
			for (int y = 0; y < map.GetLength(1); y++)
			{
				GameObject toInstantiate=null;
				if (map[x, y].FloorEnum == FloorEnum.Wall)
				{
					//壁
					toInstantiate = wallTiles[Random.Range(0, wallTiles.Length)];
				}
				else
				{
					//床
					toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                    if (map[x, y].FloorEnum == FloorEnum.ROOM)
                    {
						gridPositions.Add(new Vector3(x, y, 0f));
					}
				}
				//床or外壁を生成し、instance変数に格納
				GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f),
					Quaternion.identity) as GameObject;
				//生成したinstanceをBoardオブジェクトの子オブジェクトとする
				instance.transform.SetParent(boardHolder);
			}
		}

		for(int x = -10; x < map.GetLength(0)+10; x++)
		{
			for(int y = -6; y <=-1; y++)
			{ 
				GameObject instance = Instantiate(wallTiles[Random.Range(0, wallTiles.Length)], new Vector3(x, y, 0f),
					Quaternion.identity) as GameObject;
				instance.transform.SetParent(boardHolder);
			}

			for(int y = map.GetLength(1); y <= map.GetLength(1) + 5; y++)
            {
				GameObject instance = Instantiate(wallTiles[Random.Range(0, wallTiles.Length)], new Vector3(x, y, 0f),
					Quaternion.identity) as GameObject;
				instance.transform.SetParent(boardHolder);
			}
		}
		for(int y = 0; y < map.GetLength(1); y++)
        {
			for(int x = -10; x < 0; x++)
            {
				GameObject instance = Instantiate(wallTiles[Random.Range(0, wallTiles.Length)], new Vector3(x, y, 0f),
					Quaternion.identity) as GameObject;
				instance.transform.SetParent(boardHolder);
			}
			for(int x = map.GetLength(0); x < map.GetLength(0) + 10; x++)
            {
				GameObject instance = Instantiate(wallTiles[Random.Range(0, wallTiles.Length)], new Vector3(x, y, 0f),
					Quaternion.identity) as GameObject;
				instance.transform.SetParent(boardHolder);
			}
        }

		List<FloorChip>[] rooms = mapGenerator.Rooms;

		MovingObject[,] movingObjectMap = CreateMovingObject(map);

		ItemScriptForField[,] itemMap = CreateItemMap(map,rooms);

		GameManager.instance.SetMap(map, rooms, movingObjectMap, itemMap);
	}

	private ItemScriptForField[,] CreateItemMap(FloorChip[,] floorMap,List<FloorChip>[] rooms)
    {
		ItemScriptForField[,] map = new ItemScriptForField[columns, rows];

		int objectCount = Random.Range(3, 7);//3から6個

		for(int i = 0; i < objectCount; i++)
        {
			Vector3 randomPosition = RandomPosition();
			ItemEnum itemEnum= CreateItemData.ReturnRandomItem(GameManager.instance.GetLevel());
			GameObject chip = SetItemIllust(itemEnum);
			GameObject item = Instantiate(chip, randomPosition, Quaternion.identity);
			ItemScriptForField script = item.GetComponent<ItemScriptForField>();
			map[(int)randomPosition.x, (int)randomPosition.y] = script;
			script.itemEnum = itemEnum;
			item.GetComponent<SpriteRenderer>().material = ItemConstractParent.ReturnMaterial(script.itemEnum);

		}
		foreach(var room in rooms)
        {
            if (room[0].IsMonsterHouse)
            {
				int r= Random.Range(3, 5);
				for(int i = 0; i < r; i++)
                {
					FloorChip randomChip = Utils.GetRandom(room);
                    if (randomChip.FloorEnum == FloorEnum.ROOM)
                    {
						Vector3 randomPosition = new Vector3(randomChip.X, randomChip.Y, 0);
						if(map[(int)randomPosition.x, (int)randomPosition.y] == null)
						{
							ItemEnum itemEnum = CreateItemData.ReturnRandomItemForMonsterHouse(GameManager.instance.GetLevel());
							GameObject chip = SetItemIllust(itemEnum);
							GameObject item = Instantiate(chip, randomPosition, Quaternion.identity);
							ItemScriptForField script = item.GetComponent<ItemScriptForField>();
							map[(int)randomPosition.x, (int)randomPosition.y] = script;
							script.itemEnum = itemEnum;
							item.GetComponent<SpriteRenderer>().material = ItemConstractParent.ReturnMaterial(script.itemEnum);
						}
					}
				}
			}
        }
		
		return map;
    }

	private GameObject SetItemIllust(ItemEnum itemEnum)
    {
		GameObject chip=null;
		switch (itemEnum)
		{
			case ItemEnum.Portion:
				chip = portionItemTile;
				break;
			case ItemEnum.SecondSight:
				chip = portionItemTile;
				break;
			case ItemEnum.Ether:
				chip = portionItemTile;
				break;
			case ItemEnum.HappyPortion:
				chip = portionItemTile;
				break;
			case ItemEnum.UnHappyPortion:
				chip = portionItemTile;
				break;
			default:
				chip = magicItemTile;
				break;
		}
		return chip;
	}

	private MovingObject[,] CreateMovingObject(FloorChip[,] floorMap)
    {
		MovingObject[,] map = new MovingObject[columns,rows];

		//Player作成
		bool isTrue = false;
		while (!isTrue)
        {
			Vector3 playerPosition = RandomPosition();
            if (!floorMap[(int)(playerPosition.x), (int)(playerPosition.y)].IsMonsterHouse)
			{
				GameObject player = Instantiate(Player, playerPosition, Quaternion.identity);
				PlayerController playerController = new PlayerController(player.GetComponent<Player>());
				GameManager.instance.AddPlayer(playerController);
				_camera.Follow = player.transform.Find("CameraAimDecoy").gameObject.transform;
				map[(int)playerPosition.x, (int)playerPosition.y] = player.GetComponent<Player>();
				isTrue = true;
			}
		}
		

		Vector3 exitPosition = RandomPosition();
		GameObject exit = Instantiate(Exit, exitPosition, Quaternion.identity);
		floorMap[(int)exitPosition.x, (int)exitPosition.y].FloorEnum = FloorEnum.STRAIRS;

		LayoutEnemyAtRandom(3,6, map);

		return map;
    }

	private void LayoutEnemyAtRandom(int minimum, int maximum, MovingObject[,] map)
	{
		int objectCount = Random.Range(minimum, maximum + 1);
		for (int i = 0; i < objectCount; i++)
		{
			//gridPositionから位置情報を１つ取得
			Vector3 randomPosition = RandomPosition();
			//ランダムで決定した種類・位置でオブジェクトを生成
			Enemy enemy= Instantiate(enemyTile, randomPosition, Quaternion.identity).GetComponent<Enemy>();
			map[(int)randomPosition.x, (int)randomPosition.y] = enemy;
			CreateEnemyData.InitializeEnemy(enemy,GameManager.instance.GetLevel());
		}
	}

	public void CreateEnemy()
    {
		MovingObject[,] objectMap = GameManager.instance.MovingObjectMap();
		bool isCreateEnemy = false;
		int attemptCount = 0;
		Player player = GameManager.instance.GetPlayerController().GetPlayer();
		int playerX = (int)player.transform.position.x;
		int playerY = (int)player.transform.position.y;
        while (!isCreateEnemy && attemptCount <= 20)
        {
			attemptCount++;
			Vector3 randomPosition = RandomPosition();
            if (Mathf.Abs(randomPosition.x - playerX) >= 10 && Mathf.Abs(randomPosition.y - playerY) >= 6)
            {
				if (objectMap[(int)randomPosition.x, (int)randomPosition.y] == null)
				{
					Enemy enemy = Instantiate(enemyTile, randomPosition, Quaternion.identity).GetComponent<Enemy>();
					objectMap[(int)randomPosition.x, (int)randomPosition.y] = enemy;
					CreateEnemyData.InitializeEnemy(enemy,GameManager.instance.GetLevel());
					isCreateEnemy = true;
				}
			}
		}	
    }

	public void CreateEnemyForMonsterHouse(int x,int y)
    {
		MovingObject[,] objectMap = GameManager.instance.MovingObjectMap();
		Vector3 position = new Vector3(x,y,0);
        if (objectMap[x, y] == null)
		{
			Enemy enemy = Instantiate(enemyTile, position, Quaternion.identity).GetComponent<Enemy>();
			objectMap[(int)position.x, (int)position.y] = enemy;
			CreateEnemyData.InitializeEnemy(enemy, GameManager.instance.GetLevel());
		}
	}
}