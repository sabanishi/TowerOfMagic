using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System;
using Image = UnityEngine.UI.Image;

public class GrowManager : ManagerInterface
{
    [SerializeField] private Text Name1Text;
    [SerializeField] private Text Name2Text;
    [SerializeField] private Text Name3Text;
    [SerializeField] private Text Info1Text;
    [SerializeField] private Text Info2Text;
    [SerializeField] private Text Info3Text;

    [SerializeField] private Text FireMastery;
    [SerializeField] private Text WaterMastery;
    [SerializeField] private Text WindMastery;
    [SerializeField] private Text GroundMastery;

    [SerializeField] private ScrollView ScrollView;
    [SerializeField] private GameObject SkillNode;
    [SerializeField] private GameObject Content;
    [SerializeField] private Material Gray;
    [SerializeField] private Material White;

    [SerializeField] private Text skillExplainText;
    [SerializeField] private Text SelectInfoName;

    [SerializeField] private GameObject AssignPointPanel;
    [SerializeField] private Text AssignPointInfoText;
    [SerializeField] private Text AssignPointNumText;
    public Text GetAssignPointNumText()
    {
        return AssignPointNumText;
    }

    [SerializeField] private GameObject ErrorPanel;
    [SerializeField] private Text ErrorText;

    private MovingObjectInfomation info1, info2, info3;

    private bool isSelectSkill;
    public bool IsSelectSkill()
    {
        return isSelectSkill;
    }
    private bool isError;
    private MovingObjectInfomation selectInfo;
    public MovingObjectInfomation GetSelectInfo()
    {
        return selectInfo;
    }

    public enum SelectSkillOrMastery
    {
        Fire,Water,Wind,Ground,Skill
    }
    public SelectSkillOrMastery selectSkillOrMastery;
    public ScrollViewNode selectNode;

    public void Setting(MovingObjectInfomation _info1, MovingObjectInfomation _info2, MovingObjectInfomation _info3)
    {
        info1 = _info1;
        info2 = _info2;
        info3 = _info3;
        Name1Text.text = info1.Name;
        Name2Text.text = info2.Name;
        Name3Text.text = info3.Name;
        selectInfo = info1;
        isSelectSkill = false;
        isError = false;
        SetPlayerInfo();
        SetLeftPanel(selectInfo);
        CreateSkillScrollView();
        skillExplainText.text = "";
    }

    private void SetPlayerInfo()
    {
        Info1Text.text = "Lv:" + info1.Level + " 残りポイント:" + info1.SkillPoint;
        Info2Text.text = "Lv:" + info2.Level + " 残りポイント:" + info2.SkillPoint;
        Info3Text.text = "Lv:" + info3.Level + " 残りポイント:" + info3.SkillPoint;
    }

    private void SetMasteryInfo()
    {
        FireMastery.text = "・火属性 Lv:" + selectInfo.FireLevel;
        WaterMastery.text = "・水属性 Lv:" + selectInfo.WaterLevel;
        WindMastery.text = "・風属性 Lv:" + selectInfo.WindLevel;
        GroundMastery.text = "・地属性 Lv:" + selectInfo.GroundLevel;
    }

    private void CreateSkillScrollView()
    {
        int count = 0;
        foreach(Transform obj in Content.gameObject.transform)
        {
            GameObject.Destroy(obj.gameObject);
        }

        foreach (SkillConstract skillConstract in SkillConstractParent.GetAllSkill())
        {
            SkillEnum skillEnum = skillConstract.skillEnum;
            count++;
            if (count >=1)
            {
                string name = SkillConstractParent.ReturnNameFromSkillEnum(skillEnum);
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
                item.GetComponent<Image>().material = material;
                item.GetComponent<ScrollViewNode>().explainText = skillExplainText;
                item.GetComponent<ScrollViewNode>().parent = this;
                skillName.text = name;
            }
        }
        SetSkillInfo();
    }

    public override void Act()
    {
        if (isSelectSkill)
        {
            AssignPointPanel.SetActive(true);
        }
        else
        {
            AssignPointPanel.SetActive(false);
        }
        if (isError)
        {
            ErrorPanel.SetActive(true);
        }
        else
        {
            ErrorPanel.SetActive(false);
        }
    }

    public void Name1()
    {
        SetLeftPanel(info1);
    }

    public void Name2()
    {
        SetLeftPanel(info2);
    }

    public void Name3()
    {
        SetLeftPanel(info3);
    }

    private void SetLeftPanel(MovingObjectInfomation info)
    {
        if (!isSelectSkill)
        {
            selectInfo = info;
            SelectInfoName.text = info.Name + "のスキル";
            SetSkillInfo();
            SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
        }
    }

    private void SetSkillInfo()
    {
        SetMasteryInfo();
        foreach(Transform childTransform in Content.transform)
        {
            ScrollViewNode item = childTransform.gameObject.GetComponent<ScrollViewNode>();
            Text skillLv = item.transform.Find("Level").GetComponent<Text>();
            Text skillName = item.transform.Find("Name").GetComponent<Text>();
            skillLv.text="Lv:"+selectInfo.skillLvMemory.GetSkillLV(SkillConstractParent.ReturnSkillEnumForName(skillName.text));
        }
    }

    public void OnEnterFireText()
    {
        skillExplainText.text = "☆火属性マスタリ\n・火属性のスキル取得に必要。\n・風属性に強く、水属性に弱くなる。\n・レベル上昇で攻撃力UP。";
    }

    public void OnEnterWaterText()
    {
        skillExplainText.text = "☆水属性マスタリ\n・水属性のスキル取得に必要。\n・火属性に強く、地属性に弱くなる。\n・レベル上昇で防御力UP。";

    }

    public void OnEnterGroundText()
    {
        skillExplainText.text = "☆地属性マスタリ\n・地属性のスキル取得に必要。\n・水属性に強く、風属性に弱くなる。\n・レベル上昇で最大HPUP。";
    }

    public void OnEnterWindText()
    {
        skillExplainText.text = "☆風属性マスタリ\n・風属性のスキル取得に必要。\n・地属性に強く、火属性に弱くなる。\n・レベル上昇で最大MPUP。";
    }

    public void OnExitText()
    {
        if (!isSelectSkill)
        {
            skillExplainText.text = "";
        }
    }

    public void Back()
    {
        parent.TurnEnumChange(PlayerTurnEnum.Robby);
        SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
    }

    public void OnPointerDownFireMastery()
    {
        if (selectInfo.SkillPoint >= 1 && selectInfo.FireLevel <selectInfo.MasteryLevelMax)
        {
            SetAssignPointPanel("火属性マスタリ");
            selectSkillOrMastery = SelectSkillOrMastery.Fire;
            SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
        }
        else
        {
            ShowErrorMessage();
        }
    }

    public void OnPointerDownWaterMastery()
    {
        if (selectInfo.SkillPoint >= 1 && selectInfo.WaterLevel < selectInfo.MasteryLevelMax)
        {
            SetAssignPointPanel("水属性マスタリ");
            selectSkillOrMastery = SelectSkillOrMastery.Water;
            SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
        }
        else
        {
            ShowErrorMessage();
        }
    }

    public void OnPointerDownWindMastery()
    {
        if (selectInfo.SkillPoint >= 1 && selectInfo.WindLevel < selectInfo.MasteryLevelMax)
        {
            SetAssignPointPanel("風属性マスタリ");
            selectSkillOrMastery = SelectSkillOrMastery.Wind;
            SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
        }
        else
        {
            ShowErrorMessage();
        }
    }

    public void OnPointerDownGroundMastery()
    {
        if (selectInfo.SkillPoint >= 1 && selectInfo.GroundLevel < selectInfo.MasteryLevelMax)
        {
            SetAssignPointPanel("土属性マスタリ");
            selectSkillOrMastery = SelectSkillOrMastery.Ground;
            SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
        }
        else
        {
            ShowErrorMessage();
        }
    }

    public void SetAssignPointPanel(string skillName)
    {
        isSelectSkill = true;
        AssignPointNumText.text = 1+"";
        AssignPointInfoText.text = selectInfo.Name + "　の　" + skillName + "　に";
    }

    public void UpSelectPoint()
    {
        int num = int.Parse(AssignPointNumText.text);
        if (selectSkillOrMastery == SelectSkillOrMastery.Skill)
        {
            if ((GetNowSkillPointFromSelectEnum(selectSkillOrMastery) + num < selectNode.GetMaxLv()) &&
            (num < selectInfo.SkillPoint))
            {
                num++;
                SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
            }
            else
            {
                SoundManager.instance.PlaySE(SoundManager.SE_Type.cannotClick);
            }
        }
        else
        {
            if ((GetNowSkillPointFromSelectEnum(selectSkillOrMastery) + num < selectInfo.MasteryLevelMax) &&
            (num < selectInfo.SkillPoint))
            {
                num++;
                SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
            }
            else
            {
                SoundManager.instance.PlaySE(SoundManager.SE_Type.cannotClick);
            }
        }
        AssignPointNumText.text = num + "";

        if (selectSkillOrMastery == SelectSkillOrMastery.Skill)
        {
            selectNode.ReWriteExplain();
        }
    }

    public void DownSeletPoint()
    {
        int num = int.Parse(AssignPointNumText.text);
        if (num>1)
        {
            num--;
            SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
        }
        else
        {
            SoundManager.instance.PlaySE(SoundManager.SE_Type.cannotClick);
        }
        AssignPointNumText.text = num + "";

        if (selectSkillOrMastery == SelectSkillOrMastery.Skill)
        {
            selectNode.ReWriteExplain();
        }
    }

    private int GetNowSkillPointFromSelectEnum(SelectSkillOrMastery select)
    {
        int skillPoint = 0;
        switch (select)
        {
            case SelectSkillOrMastery.Fire:
                skillPoint = selectInfo.FireLevel;
                break;
            case SelectSkillOrMastery.Water:
                skillPoint = selectInfo.WaterLevel;
                break;
            case SelectSkillOrMastery.Ground:
                skillPoint = selectInfo.GroundLevel;
                break;
            case SelectSkillOrMastery.Wind:
                skillPoint = selectInfo.WindLevel;
                break;
            case SelectSkillOrMastery.Skill:
                skillPoint = selectNode.GetSkillLv();
                break;
            default:
                break;
        }
        return skillPoint;

    }

    public void YesForSelectSkill()
    {
        int assignSkillPoint = int.Parse(AssignPointNumText.text);
        
        switch (selectSkillOrMastery)
        {
            case SelectSkillOrMastery.Fire:
                selectInfo.AddFirePoint(assignSkillPoint);
                break;
            case SelectSkillOrMastery.Water:
                selectInfo.AddWaterePoint(assignSkillPoint);
                break;
            case SelectSkillOrMastery.Ground:
                selectInfo.AddGroundPoint(assignSkillPoint);
                break;
            case SelectSkillOrMastery.Wind:
                selectInfo.AddWindPoint(assignSkillPoint);
                break;
            case SelectSkillOrMastery.Skill:
                selectNode.AddSkillPoint(assignSkillPoint);
                break;
            default:
                break;
        }
        isSelectSkill = false;
        SetSkillInfo();
        SetPlayerInfo();
        skillExplainText.text = "";
        selectInfo.ChoiceMaterialForPlayer();
        SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
        AssignPointNumText.text = 0 + "";
    }

    public void NoForSelectSkill()
    {
        isSelectSkill = false;
        skillExplainText.text = "";
        SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
        AssignPointNumText.text = 0 + "";
    }

    public void ShowErrorMessage()
    {
        isError = true;
        if (selectInfo.SkillPoint <= 0)
        {
            ErrorText.text = "スキルポイントがありません！";
            SoundManager.instance.PlaySE(SoundManager.SE_Type.runOutOfPoint);
        }
        else
        {
            ErrorText.text = "スキルレベルが最大です！";
            SoundManager.instance.PlaySE(SoundManager.SE_Type.alreadyMaxPoint);
        }
    }

    public void ShowErrorBecauseNeedSkill()
    {
        isError = true;
        ErrorText.text = "必要マスタリを取得してません！";
        SoundManager.instance.PlaySE(SoundManager.SE_Type.cannotAssignPoint);
    }

    public void OnPointerDownOK()
    {
        isError = false;
        SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
    }

}
