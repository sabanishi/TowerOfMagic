using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System;
using Image = UnityEngine.UI.Image;

public class SkillSelectManager : ManagerInterface
{
    [SerializeField] private Text name1;
    [SerializeField] private Text name2;
    [SerializeField] private Text name3;

    [SerializeField] private Text selectName;
    [SerializeField] private SpriteRenderer selectImage;

    [SerializeField] private Text level;
    [SerializeField] private Text needExp;
    [SerializeField] private Text hp;
    [SerializeField] private Text mp;
    [SerializeField] private Text atk;
    [SerializeField] private Text def;
    [SerializeField] private Text fire;
    [SerializeField] private Text water;
    [SerializeField] private Text ground;
    [SerializeField] private Text wind;

    [SerializeField] private Text selectSkill;

    [SerializeField] private ScrollView ScrollView;
    [SerializeField] private GameObject SkillNode;
    [SerializeField] private GameObject Content;
    [SerializeField] private Material Gray;
    [SerializeField] private Material White;

    [SerializeField] private GameObject SelectingPanel;
    [SerializeField] private Text selectingText;
    [SerializeField] private Text selectText;
    [SerializeField] private Text selectingSkillUseText;
    [SerializeField] private Text SkillName;
    [SerializeField] private Text SkillLv;
    [SerializeField] private Text SkillInfo;

    private MovingObjectInfomation info1;
    private MovingObjectInfomation info2;
    private MovingObjectInfomation info3;
    private MovingObjectInfomation nowPlayerInfo;
    private MovingObjectInfomation selectInfo;
    public MovingObjectInfomation GetNowPlayerInfo()
    {
        return nowPlayerInfo;
    }
    public MovingObjectInfomation GetSelectInfo()
    {
        return selectInfo;
    }

    public bool canChangePlayerInfo;
    public SkillEnum selectingSkillEnum;

    public void Setting(MovingObjectInfomation _info1, MovingObjectInfomation _info2, MovingObjectInfomation _info3, MovingObjectInfomation _nowPlayerInfo)
    {
        info1 = _info1;
        info2 = _info2;
        info3 = _info3;
     
        nowPlayerInfo = _nowPlayerInfo;
        selectInfo = info1;
        SetPlayerInfo();
        SetSkillInfo();
        SetName();
        canChangePlayerInfo = true;
    }

    private void SetName()
    {
        name1.text = info1.Name;
        name2.text = info2.Name;
        name3.text = info3.Name;
        if (info1.Equals(nowPlayerInfo))
        {
            name1.text += ":E";
        }
        if (info2.Equals(nowPlayerInfo))
        {
            name2.text += ":E";
        }
        if (info3.Equals(nowPlayerInfo))
        {
            name3.text += ":E";
        }
    }

    public override void Act()
    {
        if (canChangePlayerInfo)
        {
            SelectingPanel.SetActive(false);
        }
        else
        {
            SelectingPanel.SetActive(true);
        }
    }

    private void SetPlayerInfo()
    {
        hp.text = selectInfo.Hp + "/" + selectInfo.MaxHp;
        mp.text = selectInfo.Mp + "/" + selectInfo.MaxMp;
        atk.text = selectInfo.Atk+"";
        def.text = selectInfo.Def+"";
        fire.text = selectInfo.FireLevel+"";
        water.text = selectInfo.WaterLevel + "";
        ground.text = selectInfo.GroundLevel + "";
        wind.text = selectInfo.WindLevel + "";
        level.text = selectInfo.Level+"";
        needExp.text = (int)(selectInfo.NeedExpData[selectInfo.Level]-selectInfo.NowExp) + "";

        selectName.text = selectInfo.Name;
        selectImage.material = selectInfo.material;
    }

    private void CreateSkillScrollView()
    {
        int count = 0;
        foreach (Transform obj in Content.gameObject.transform)
        {
            GameObject.Destroy(obj.gameObject);
        }

        foreach (SkillConstract skillconstact in SkillConstractParent.GetAllSkill())
        {
            SkillEnum skillEnum = skillconstact.skillEnum;
            count++;
            if (count >=1)
            {
                count--;
                string name = SkillConstractParent.ReturnNameFromSkillEnum(skillEnum);
                if (selectInfo.skillLvMemory.GetSkillLV(skillEnum) >= 1)
                {
                    Material material;
                    if (count % 2 == 0)
                    {
                        material = White;
                    }
                    else
                    {
                        material = Gray;
                    }
                    GameObject item = Instantiate(SkillNode, Content.transform);
                    Text skillName = item.transform.Find("Name").GetComponent<Text>();
                    item.transform.Find("Level").GetComponent<Text>().text = "Lv:"+selectInfo.skillLvMemory.GetSkillLV(skillEnum);
                    item.GetComponent<Image>().material = material;
                    item.GetComponent<ScrollViewNode2>().parent = this;
                    skillName.text = name;
                    count++;
                }
            }
        }
    }

    private void SetSkillInfo()
    {
        CreateSkillScrollView();

        if (selectInfo.selectSkill == SkillEnum.None || selectInfo.selectSkill == SkillEnum.NormalAttack)
        {
            selectSkill.text = "なし";
        }
        else
        {
            selectSkill.text = SkillConstractParent.ReturnNameFromSkillEnum(selectInfo.selectSkill);
        }
    }

    public void Name1()
    {
        if (canChangePlayerInfo)
        {
            selectInfo = info1;
            SetPlayerInfo();
            SetSkillInfo();
            SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
        }
    }

    public void Name2()
    {
        if (canChangePlayerInfo)
        {
            selectInfo = info2;
            SetPlayerInfo();
            SetSkillInfo();
            SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
        }
    }

    public void Name3()
    {
        if (canChangePlayerInfo)
        {
            selectInfo = info3;
            SetPlayerInfo();
            SetSkillInfo();
            SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
        }
    }

    public void SetSelectingSkillPanel(ScrollViewNode2 node)
    {
        selectingText.text = selectInfo.Name + "　は　" + SkillConstractParent.ReturnNameFromSkillEnum(selectingSkillEnum) + "　を";
        if (selectInfo == nowPlayerInfo)
        {
            selectingSkillUseText.GetComponent<TextController5>().isActive = true;

        }
        else
        {
            selectingSkillUseText.GetComponent<TextController5>().isActive = false;
        }
        selectingSkillUseText.GetComponent<TextController5>().OnPointerExit();
        if (selectInfo.selectSkill == selectingSkillEnum)
        {
            selectText.text = "登録解除";
        }
        else
        {
            selectText.text = "登録する";
        }

        SkillName.text = SkillConstractParent.ReturnNameFromSkillEnum(selectingSkillEnum);
        SkillLv.text = "Lv."+selectInfo.skillLvMemory.GetSkillLV(selectingSkillEnum);
        SkillInfo.text = node.WriteExplain();
    }

    public void UseSkill()
    {
        if (selectInfo.Equals(nowPlayerInfo))
        {
            parent.TurnEnumChange(PlayerTurnEnum.Robby);
            parent.Back();
            GameManager.instance.GetPlayerController().SkillByMenu(selectingSkillEnum);
        }
        else
        {
            SoundManager.instance.PlaySE(SoundManager.SE_Type.cannotClick);
        }
    }

    public void SelectSkill()
    {
        if (selectInfo.selectSkill == selectingSkillEnum)
        {
            selectInfo.selectSkill = SkillEnum.None;
        }
        else
        {
            selectInfo.selectSkill = selectingSkillEnum;
        }
        canChangePlayerInfo = true;
        SetSkillInfo();
        SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
    }

    public void CancelSkill()
    {
        canChangePlayerInfo = true;
        SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
    }

    public void Change()
    {
        if (!selectInfo.Equals(nowPlayerInfo))
        {
            if (selectInfo.Hp>0)
            {
                nowPlayerInfo = selectInfo;
                GameManager.instance.GetPlayerController().GetPlayer().SetNowMovingObjectInfo(selectInfo);
                SetName();
                SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
            }
            else
            {
                SoundManager.instance.PlaySE(SoundManager.SE_Type.cannotClick);
            }
        }
        else
        {
            SoundManager.instance.PlaySE(SoundManager.SE_Type.cannotClick);
        }
    }

    public void Back()
    {
        SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
        parent.TurnEnumChange(PlayerTurnEnum.Robby);
    }
}
