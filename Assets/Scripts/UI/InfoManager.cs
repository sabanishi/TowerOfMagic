using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class InfoManager : MonoBehaviour
{
    public GameObject InfoPane;
    public Text FloorText;
    public Text PlayerNameText;
    public Text PlayerLvText;
    public Text PlayerHpText;
    public Text PlayerMpText;
    public Canvas InfoMapCanvas;
    public GameObject FloorChip;
    public GameObject ExitChip;
    public GameObject PlayerChip;
    public GameObject EnemyChip;
    public GameObject ItemChip;

    private GameManager gameManager;
    public void SetGameManager(GameManager _gameManager)
    {
        gameManager=_gameManager;
    }

    private bool isCreateMap;
    private GameObject[,] floorChips;
    private FloorChip[,] floorMap;
    private List<GameObject> movingObjectChips = new List<GameObject>();
    private GameObject[,] itemChips;

    public void Update()
    {
        if (gameManager != null)
        {
            if (gameManager.GetPlayerController().IsActive())
            {
                InfoPane.SetActive(true);
            }
            else
            {
                InfoPane.SetActive(false);
            }
            MovingObjectInfomation info = gameManager.GetPlayerController().GetPlayer().GetNowMovingObjectInfo();
            FloorText.text = gameManager.GetLevel() + "階";

            PlayerNameText.text = info.Name;
            PlayerLvText.text = "Lv:" + info.Level;
            PlayerHpText.text = "HP:" + info.Hp + "/" + info.MaxHp;
            PlayerMpText.text = "MP:" + info.Mp + "/" + info.MaxMp;

            if (!isCreateMap)
            {
                isCreateMap = true;
                CreateMap();
            }
            else
            {
                UpdateMap();
            }
        }
    }

    private void UpdateMap()
    {
        floorMap = gameManager.FloorMap();
        MovingObject[,] objectMap = gameManager.MovingObjectMap();
        ItemScriptForField[,] itemMap = gameManager.ItemMap();
        Player player = gameManager.GetPlayerController().GetPlayer();
        int playerX = (int)player.transform.position.x;
        int playerY = (int)player.transform.position.y;

        foreach (var obj in movingObjectChips)
        {
            Destroy(obj);
        }
        movingObjectChips.Clear();
        

        for (int x = playerX - 4; x <= playerX + 4; x++)
        {
            for (int y = playerY - 4; y <= playerY + 4; y++)
            {
                if (x < 0 || x >= floorMap.GetLength(0) || y < 0 || y >= floorMap.GetLength(1))
                {
                    break;
                }
                if (floorMap[x, y] != null)
                {
                    floorMap[x, y].IsOnMap = true;
                }
                if (objectMap[x, y] != null)
                {
                    objectMap[x, y].IsOnMap = true;
                }
                
                if (itemMap[x, y] != null && itemMap[x, y].itemEnum != ItemEnum.None&&itemChips[x,y]==null)
                {
                    GameObject itemChip = Instantiate(ItemChip, Vector3.zero, Quaternion.identity, InfoMapCanvas.transform);
                    itemChip.GetComponent<RectTransform>().localPosition = new Vector3(x * 4, y * 4, 0);
                    itemChip.SetActive(true);
                    itemChips[x, y] = itemChip;
                } 
            }
        }
        for (int x = 0; x < floorMap.GetLength(0); x++)
        {
            for (int y = 0; y < floorMap.GetLength(1); y++)
            {
                if (floorMap[x, y] != null)
                {
                    if (floorMap[x, y].IsOnMap)
                    {
                        if (floorChips[x, y] != null)
                        {
                            floorChips[x, y].SetActive(true);
                        }
                    }
                }
                if (objectMap[x, y] != null)
                {
                    if (objectMap[x, y].IsOnMap)
                    {
                        GameObject gameObject;
                        if (objectMap[x, y].IsPlayer())
                        {
                            gameObject = Instantiate(PlayerChip, Vector3.zero, Quaternion.identity, InfoMapCanvas.transform);
                        }
                        else
                        {
                            gameObject = Instantiate(EnemyChip, Vector3.zero, Quaternion.identity, InfoMapCanvas.transform);
                        }
                        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(x * 4, y * 4, 0);
                        movingObjectChips.Add(gameObject);
                    }
                }
            }
        }
        for (int x = 0; x < itemChips.GetLength(0); x++)
        {
            for (int y = 0; y < itemChips.GetLength(1); y++)
            {
                if (itemChips[x, y] != null && itemMap[x, y] == null)
                {
                    Destroy(itemChips[x, y]);
                    itemChips[x, y] = null;
                }
            }
        }
    }

    private void CreateMap()
    {
        FloorChip[,] floorMap = gameManager.FloorMap();
        itemChips= new GameObject[floorMap.GetLength(0), floorMap.GetLength(1)];
        floorChips = new GameObject[floorMap.GetLength(0), floorMap.GetLength(1)];
        for (int x=0;x<floorMap.GetLength(0);x++)
        {
            for (int y = 0; y < floorMap.GetLength(1); y++)
            {
                if (floorMap[x, y] != null)
                {
                    if (floorMap[x, y].FloorEnum != FloorEnum.Wall)
                    {
                        GameObject gameObject = null;
                        if (floorMap[x, y].FloorEnum == FloorEnum.STRAIRS)
                        {
                            gameObject = Instantiate(ExitChip,Vector3.zero, Quaternion.identity,InfoMapCanvas.transform);
                            gameObject.GetComponent<RectTransform>().localPosition = new Vector3(x * 4, y * 4, 0);
                        }
                        else
                        {
                            gameObject = Instantiate(FloorChip,Vector3.zero, Quaternion.identity,InfoMapCanvas.transform);
                            gameObject.GetComponent<RectTransform>().localPosition = new Vector3(x * 4, y * 4, 0);
                        }
                        gameObject.SetActive(false);
                        floorChips[x, y] = gameObject;
                    }
                }
            }
        }
       
     }

    public enum InfoMapEnum
    {
        Floor,Enemy,Player,Item,Exit
    }

    public void ShowAllItem()
    {
        MovingObject[,] objectMaps = GameManager.instance.MovingObjectMap();
        ItemScriptForField[,] itemMap = GameManager.instance.ItemMap();
        FloorChip[,] floorMap = GameManager.instance.FloorMap();
        for (int x = 0; x < objectMaps.GetLength(0); x++)
        {
            for (int y = 0; y < objectMaps.GetLength(1); y++)
            {
                floorMap[x, y].IsOnMap = true;
                if (objectMaps[x, y] != null)
                {
                    objectMaps[x, y].IsOnMap = true;
                }
                if (itemMap[x, y] != null && itemMap[x, y].itemEnum != ItemEnum.None && itemChips[x, y] == null)
                {
                    GameObject itemChip = Instantiate(ItemChip, Vector3.zero, Quaternion.identity, InfoMapCanvas.transform);
                    itemChip.GetComponent<RectTransform>().localPosition = new Vector3(x * 4, y * 4, 0);
                    itemChip.SetActive(true);
                    itemChips[x, y] = itemChip;
                }
            }
        }
    }
}
