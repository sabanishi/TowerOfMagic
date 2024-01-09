using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager:MonoBehaviour
{
    [SerializeField] private GameObject MenuPanel;

    [SerializeField] private GameObject RobbyPanel;
    [SerializeField] private RobbyManager robbyManager;

    [SerializeField] private GameObject SkillSelctPanel;
    [SerializeField] private SkillSelectManager skillSelectManager;

    [SerializeField] private GameObject ChangePanel;
    [SerializeField] private ChangeManager changeManager;

    [SerializeField] private GameObject GrowPanel;
    [SerializeField] private GrowManager growManager;

    [SerializeField] private GameObject ItemPanel;
    [SerializeField] private ItemManager itemManager;

    private PlayerTurnEnum turnEnum;

    public void Setting()
    {
        TurnEnumChange(PlayerTurnEnum.Robby);
        MenuPanel.SetActive(true);
    }

    public void Act()
    {
        DialogManager.instance.DisappeareDialog();
        switch (turnEnum)
        {
            case PlayerTurnEnum.Robby:
                robbyManager.Act();
                break;
            case PlayerTurnEnum.SkillSelect:
                skillSelectManager.Act();
                break;
            case PlayerTurnEnum.Change:
                changeManager.Act();
                break;
            case PlayerTurnEnum.Grow:
                growManager.Act();
                break;
            case PlayerTurnEnum.Item:
                itemManager.Act();
                break;
            default:
                Debug.Log("MenuManagerでバグ"+turnEnum+"がない");
                break;
        }
    }

    public void TurnEnumChange(PlayerTurnEnum turn)
    {
        GameManager gameManager = GameManager.instance;
        turnEnum = turn;
        RobbyPanel.SetActive(false);
        SkillSelctPanel.SetActive(false);
        ChangePanel.SetActive(false);
        GrowPanel.SetActive(false);
        ItemPanel.SetActive(false);
        switch (turnEnum)
        {
            case PlayerTurnEnum.Robby:
                RobbyPanel.SetActive(true);
                robbyManager.Setting(gameManager.GetPlayerInfo1(),gameManager.GetPlayerInfo2(),gameManager.GetPlayerInfo3(),
                    gameManager.GetPlayerController().GetPlayer().GetNowMovingObjectInfo());
                break;
            case PlayerTurnEnum.SkillSelect:
                SkillSelctPanel.SetActive(true);
                skillSelectManager.Setting(gameManager.GetPlayerInfo1(), gameManager.GetPlayerInfo2(), gameManager.GetPlayerInfo3(),
                    gameManager.GetPlayerController().GetPlayer().GetNowMovingObjectInfo());
                break;
            case PlayerTurnEnum.Change:
                ChangePanel.SetActive(true);
                changeManager.Setting(gameManager.GetPlayerInfo1(), gameManager.GetPlayerInfo2(), gameManager.GetPlayerInfo3(),
                    gameManager.GetPlayerController().GetPlayer().GetNowMovingObjectInfo());
                break;
            case PlayerTurnEnum.Grow:
                GrowPanel.SetActive(true);
                growManager.Setting(gameManager.GetPlayerInfo1(), gameManager.GetPlayerInfo2(), gameManager.GetPlayerInfo3());
                break;
            case PlayerTurnEnum.Item:
                ItemPanel.SetActive(true);
                itemManager.Setting();
                break;
        }
    }

    public bool PlayerDieDeal(MovingObjectInfomation info1,MovingObjectInfomation info2,MovingObjectInfomation info3)
    {
        TurnEnumChange(PlayerTurnEnum.Change);
        bool isTrue = changeManager.PlayerDieDeal(info1,info2,info3);
        if (!isTrue)
        {
            MenuPanel.SetActive(false);
        }
        return isTrue;
    }

    public void Back()
    {
        MenuPanel.SetActive(false);
        GameManager.instance.GetPlayerController().SetIsActive(true);
    }
}
