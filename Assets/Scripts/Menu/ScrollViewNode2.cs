using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ScrollViewNode2 : MonoBehaviour
{
    [SerializeField] private Text Name;
    [SerializeField] private Text LvText;
    public SkillSelectManager parent;

    protected Color red = new Color(1, 0, 0);
    protected Color black = new Color(0.2f, 0.2f, 0.2f);

    public void OnPointerEnter()
    {
        Name.color = red;
        LvText.color = red;
    }

    public void OnPointerExit()
    {
        Name.color = black;
        LvText.color = black;
    }

    public void OnPointerDown()
    {
        if (parent.canChangePlayerInfo)
        {
            parent.selectingSkillEnum = SkillConstractParent.ReturnSkillEnumForName(Name.text);
            parent.SetSelectingSkillPanel(this);
            parent.canChangePlayerInfo = false;
            SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
        }
    }

    public int GetSkillLv()
    {
        return parent.GetSelectInfo().skillLvMemory.GetSkillLV(SkillConstractParent.ReturnSkillEnumForName(Name.text));
    }

    public void AddSkillPoint(int addPoint)
    {
        parent.GetSelectInfo().skillLvMemory.AddSkillLv(SkillConstractParent.ReturnSkillEnumForName(Name.text), addPoint);
    }

    public string WriteExplain()
    {
        int skillLv = GetSkillLv();
         return  SkillConstractParent.ReturnExplainFromName(Name.text) +"\n"+
            SkillConstractParent.ReturnSkillEffectExplainFromName(Name.text, skillLv) +
            "　消費MP:" + SkillConstractParent.ReturnNeedMpFromName(Name.text, skillLv);
    }
}
