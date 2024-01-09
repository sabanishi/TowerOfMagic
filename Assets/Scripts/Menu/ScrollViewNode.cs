using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollViewNode : MonoBehaviour
{
    [SerializeField] private Text Name;
    [SerializeField] private Text LvText;
    public Text explainText;
    private string baseExplain;
    public GrowManager parent;

    protected Color red = new Color(1, 0, 0);
    protected Color black = new Color(0.2f, 0.2f, 0.2f);

    public void OnPointerEnter()
    {
        Name.color = red;
        LvText.color = red;
        SetExplain();
    }

    public void OnPointerExit()
    {
        Name.color = black;
        LvText.color = black;
        parent.OnExitText();
    }

    public void OnPointerDown()
    {
        MovingObjectInfomation info = parent.GetSelectInfo();
        if (info.SkillPoint >= 1 && int.Parse(LvText.text.Substring(3)) < GetMaxLv())
        {
            int fire = info.FireLevel;
            int water = info.WaterLevel;
            int ground = info.GroundLevel;
            int wind = info.WindLevel;
            if(SkillConstractParent.ReturnCanLvUpFromName(Name.text,new int[] {fire,water,ground,wind }))
            {
                parent.SetAssignPointPanel(Name.text);
                parent.selectSkillOrMastery = GrowManager.SelectSkillOrMastery.Skill;
                parent.selectNode = this;
                SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
            }
            else
            {
                parent.ShowErrorBecauseNeedSkill();
            }
        }
        else
        {
            parent.ShowErrorMessage();
        }
    }

    private void SetExplain()
    {
        baseExplain= SkillConstractParent.ReturnExplainForAssignSkillFromName(Name.text);
        ReWriteExplain();

    }

    public void ReWriteExplain()
    {
        int skillLv = GetSkillLv();
        if (parent.IsSelectSkill())
        {
            skillLv += int.Parse(parent.GetAssignPointNumText().text);
        }
        if (skillLv <= 0)
        {
            skillLv = 1;
        }
        explainText.text = baseExplain + "\nLv:" + skillLv +
            SkillConstractParent.ReturnSkillEffectExplainFromName(Name.text,skillLv) + 
            " 消費MP:" + SkillConstractParent.ReturnNeedMpFromName(Name.text, skillLv);
    }

    public int GetSkillLv()
    {
        return parent.GetSelectInfo().skillLvMemory.GetSkillLV(SkillConstractParent.ReturnSkillEnumForName(Name.text));
    }

    public int GetMaxLv()
    {
        return SkillConstractParent.ReturnMaxLvFromName(Name.text);
    }

    public void AddSkillPoint(int addPoint)
    {
        parent.GetSelectInfo().skillLvMemory.AddSkillLv(SkillConstractParent.ReturnSkillEnumForName(Name.text), addPoint);
    }
}
