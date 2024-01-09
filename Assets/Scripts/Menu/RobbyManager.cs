using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobbyManager : ManagerInterface
{
    [SerializeField] private Text SkillText;
    [SerializeField] private Text GrowText;
    [SerializeField] private Text ChangeText;
    [SerializeField] private Text ItemTex;
    [SerializeField] private Text BackText;

    [SerializeField] private Text Name1;
    [SerializeField] private Text Name2;
    [SerializeField] private Text Name3;

    [SerializeField] private Text Hp1;
    [SerializeField] private Text Hp2;
    [SerializeField] private Text Hp3;

    [SerializeField] private Text Mp1;
    [SerializeField] private Text Mp2;
    [SerializeField] private Text Mp3;

    [SerializeField] private GameObject MenuPanel;

    private Color red = new Color(1, 0, 0);
    private Color black = new Color(0.2f,0.2f,0.2f);

    public void Setting(MovingObjectInfomation info1, MovingObjectInfomation info2, MovingObjectInfomation info3,MovingObjectInfomation nowPlayerInfo)
    {
        Name1.text = info1.Name;
        Hp1.text = "HP: " + info1.Hp + "/" + info1.MaxHp;
        Mp1.text = "MP: " + info1.Mp + "/" + info1.MaxMp;

        Name2.text = info2.Name;
        Hp2.text = "HP: " + info2.Hp + "/" + info2.MaxHp;
        Mp2.text = "MP: " + info2.Mp + "/" + info2.MaxMp;

        Name3.text = info3.Name;
        Hp3.text = "HP: " + info3.Hp + "/" + info3.MaxHp;
        Mp3.text = "MP: " + info3.Mp + "/" + info3.MaxMp;

        SkillText.color = black;
        GrowText.color = black;
        ChangeText.color = black;
        ItemTex.color = black;
        BackText.color = black;

        Name1.color = black;
        Hp1.color = black;
        Mp1.color = black;
        Name2.color = black;
        Hp2.color = black;
        Mp2.color = black;
        Name3.color = black;
        Hp3.color = black;
        Mp3.color = black;

        if(object.ReferenceEquals(nowPlayerInfo.Name,info1.Name))
        {
            Name1.color = red;
            Hp1.color = red;
            Mp1.color = red;
        }
        //if (nowPlayerInfo.Name.Equals(info2.Name))
        if (object.ReferenceEquals(nowPlayerInfo.Name, info2.Name))
        {
            Name2.color = red;
            Hp2.color = red;
            Mp2.color = red;
        }
        if (object.ReferenceEquals(nowPlayerInfo.Name, info3.Name))
        {
            Name3.color = red;
            Hp3.color = red;
            Mp3.color = red;
        }
    }

    public override void Act()
    {
        
    }

    public void Skill()
    {
        SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
        parent.TurnEnumChange(PlayerTurnEnum.SkillSelect);
    }

    public void Grow()
    {
        parent.TurnEnumChange(PlayerTurnEnum.Grow);
    }

    public void Change()
    {
        SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
        parent.TurnEnumChange(PlayerTurnEnum.Change);
    }

    public void Item()
    {
        SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
        parent.TurnEnumChange(PlayerTurnEnum.Item);
    }

    public void Other()
    {

    }

    public void Back()
    {
        MenuPanel.SetActive(false);
        GameManager.instance.GetPlayerController().SetIsWaitForMenu(true);
        GameManager.instance.GetPlayerController().SetIsActive(true);
        SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
    }
}