using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeManager : ManagerInterface
{
    [SerializeField] private Text Name1Text;
    [SerializeField] private Text Name2Text;
    [SerializeField] private Text Name3Text;
    private TextController2 NameController1;
    private TextController2 NameController2;
    private TextController2 NameController3;

    [SerializeField] private Text PlayerName;
    [SerializeField] private GameObject DecidePanel;
    [SerializeField] private Text YesText;
    [SerializeField] private Text NoText;

    private bool isDecide;

    private Color red = new Color(1, 0, 0);
    private Color black = new Color(0.2f,0.2f,0.2f);

    private bool dead1;
    private bool dead2;
    private bool dead3;
    private int NameCount;

    private MovingObjectInfomation info1;
    private MovingObjectInfomation info2;
    private MovingObjectInfomation info3;

    public override void Act()
    {
        if (isDecide)
        {
            DecidePanel.SetActive(true);
        }
        else
        {
            DecidePanel.SetActive(false);
        }
    }

    public void Setting(MovingObjectInfomation info1, MovingObjectInfomation info2, MovingObjectInfomation info3, MovingObjectInfomation nowPlayerInfo)
    {
        Name1Text.text = info1.Name;
        Name2Text.text = info2.Name;
        Name3Text.text = info3.Name;
        NameController1 = Name1Text.GetComponent<TextController2>();
        NameController2 = Name2Text.GetComponent<TextController2>();
        NameController3 = Name3Text.GetComponent<TextController2>();

        this.info1 = info1;
        this.info2 = info2;
        this.info3 = info3;

        if (info1.Hp <= 0)
        {
            dead1 = true;
            NameController1.isActive = false;
        }
        else
        {
            dead1 = false;
            NameController1.isActive = true;
        }
        if (info2.Hp <= 0)
        {
            dead2 = true;
            NameController2.isActive = false;
        }
        else
        {
            dead2 = false;
            NameController2.isActive = true;
        }
        if (info3.Hp <= 0)
        {
            dead3 = true;
            NameController3.isActive = false;
        }
        else
        {
            dead3 = false;
            NameController3.isActive = true;
        }
        SetNameColor();

        isDecide = false;

        if (nowPlayerInfo.Name.Equals(info1.Name))
        {
            PlayerName.text=info1.Name;
        }
        if (nowPlayerInfo.Name.Equals(info2.Name))
        {
            PlayerName.text = info2.Name;
        }
        if (nowPlayerInfo.Name.Equals(info3.Name))
        {
            PlayerName.text = info3.Name;
        }
        SetDecideFalse();
    }

    private void SetNameColor()
    {
        Name1Text.color = black;
        Name2Text.color = black;
        Name3Text.color = black;
        if (dead1)
        {
            Name1Text.color = new Color(Name1Text.color.r,Name1Text.color.g,Name1Text.color.b,0.5f);
        }
        if (dead2)
        {
            Name2Text.color = new Color(Name2Text.color.r, Name2Text.color.g, Name2Text.color.b, 0.5f);
        }
        if (dead3)
        {
            Name3Text.color = new Color(Name3Text.color.r, Name3Text.color.g, Name3Text.color.b, 0.5f);
        }
    }

    public void Name1()
    {
        if (!dead1)
        {
            NameCount = 1;
            isDecide = true;
            SetDecideTrue();
            SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
        }
    }

    public void Name2()
    {
        if (!dead2)
        {
            NameCount = 2;
            isDecide = true;
            SetDecideTrue();
            SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
        }
    }

    public void Name3()
    {
        if (!dead3)
        {
            NameCount = 3;
            isDecide = true;
            SetDecideTrue();
            SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
        }
    }

    public void Yes()
    {
        MovingObjectInfomation info = null;
        switch (NameCount)
        { 
            case 1:
                info = info1;
                break;
            case 2:
                info = info2;
                break;
            case 3:
                info = info3;
                break;
         }
        if (info != null)
        {
            GameManager.instance.GetPlayerController().GetPlayer().SetNowMovingObjectInfo(info);
        }
        PlayerName.text = info.Name;
        isDecide = false;
        SetDecideFalse();
        SetNameColor();
        SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
    }

    public void No()
    {
        isDecide = false;
        SetDecideFalse();
        SetNameColor();
        SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
    }

    public void Back()
    {
        parent.TurnEnumChange(PlayerTurnEnum.Robby);
        SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
    }

    private void SetDecideTrue()
    {
        NameController1.isDecide = true;
        NameController2.isDecide = true;
        NameController3.isDecide = true;
    }

    private void SetDecideFalse()
    {
        NameController1.isDecide = false;
        NameController2.isDecide = false;
        NameController3.isDecide = false;
    }

    public bool PlayerDieDeal(MovingObjectInfomation _info1, MovingObjectInfomation _info2, MovingObjectInfomation _info3)
    {
        info1 = _info1;
        info2 = _info2;
        info3 = _info3;
        MovingObjectInfomation info = null;
        if (info1.Hp <= 0)
        {
            dead1 = true;
        }
        else
        {
            dead1 = false;
        }
        if (info2.Hp <= 0)
        {
            dead2 = true;
        }
        else
        {
            dead2 = false;
        }
        if (info3.Hp <= 0)
        {
            dead3 = true;
        }
        else
        {
            dead3 = false;
        }

        if (dead1)
        {
            if (dead2)
            {
                if (dead3)
                {
                    return false;
                }
                else
                {
                    info = info3;
                }
            }
            else
            {
                info = info2;
            }
        }
        else
        {
            info = info1;
        }
        if (info != null)
        {
            GameManager.instance.GetPlayerController().GetPlayer().SetNowMovingObjectInfo(info);
        }
        PlayerName.text = info.Name;
        isDecide = false;
        SetDecideFalse();
        SetNameColor();
        return true;
    }
}
