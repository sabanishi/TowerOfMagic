using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogManager: MonoBehaviour
{
    public static DialogManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }

    //一行あたりの最大文字数は32字
    [SerializeField]private Text text1;
    [SerializeField]private Text text2;
    [SerializeField]private Text text3;
    [SerializeField]private Text text4;
    [SerializeField] private GameObject DialogPanel;

    /* private string waitingText1;
     private string waitingText2;
     private string waitingText3;*/
    private List<string> waitingTexts = new List<string>();

    private float DisappeareCount;
    private const float DissapeareTime = 4.0f;
    public void DisappeareDialog()
    {
        DisappeareCount = 0;
        DialogPanel.SetActive(false);
    }

    private float ChangeCount;
    private const float ChangeTime = 0.4f;

    public void Update()
    {
        if (DisappeareCount > 0)
        {
            DisappeareCount -= Time.deltaTime;
            if (DisappeareCount <= 0)
            {
                DialogPanel.SetActive(false);
            }
        }

        if (ChangeCount > 0)
        {
            ChangeCount -= Time.deltaTime;
            if (ChangeCount <= 0)
            {
                ChangeText();
            }
        }
    }

    private void ChangeText()
    {
        if (waitingTexts.Count>=1)
        {
            text4.text = text3.text;
            text3.text = text2.text;
            text2.text = text1.text;
            text1.text = waitingTexts[0];
            for(int i = 0; i < waitingTexts.Count; i++)
            {
                if (i < waitingTexts.Count-1)
                {
                    waitingTexts[i] = waitingTexts[i + 1];
                }
                else
                {
                    waitingTexts.Remove(waitingTexts[i]);
                }
            }
           /* waitingText1 = null;
            waitingText1 = waitingText2;
            waitingText2 = waitingText3;
            waitingText3 = null;*/
            ChangeCount = ChangeTime;
        }
        DisappeareCount = DissapeareTime;
        DialogPanel.SetActive(true);
    }

    public void SetText(string text)
    {
        waitingTexts.Add(text);
        if (waitingTexts.Count == 1)
        {
            
            if (ChangeCount <= 0)
            {
                ChangeText();
            }
        }
    }

    public void AttackFormat(string clientName,string skillName)
    {
        SetText("<color=#ffff00>" + clientName + "</color>　の　<color=#ff0000>" + skillName+"</color>！");
    }

    public void DamageFormat(string clientName,int damage)
    {
        SetText("<color=#ffff00>" + clientName + "</color>　に　<color=#00ffff>"+damage+"</color>　ダメージ");
    }

    public void DieFormat(string clientName)
    {
        SetText(" <color=#ffff00>" + clientName + "</color>　は　倒れた！");
    }

    public void AddExpFormat(string clientName,long exp)
    {
        SetText(" <color=#ffff00>" + clientName + "</color>　は　<color=#00ffff>" + exp+ "</color>　の経験値を手に入れた");
    }

    public void LevelUpFormat(string clientName,int level)
    {
        SetText(" <color=#ffff00>" + clientName + "</color>　は　LV:<color=#00ffff>" + level + "</color>　になった！");
    }

    public void ItemgetFormat(string itemName)
    {
        SetText("アイテム:<color=#00ff00>"+itemName+ "</color>　を拾った！");
    }

    public void HealFormat(string clientName,int damage)
    {
        SetText("<color=#ffff00>" + clientName + "</color>　のHPが　<color=#00ffff>" + damage + "</color>　回復した");
    }

    public void HealMpFormat(string clientName, int damage)
    {
        SetText("<color=#ffff00>" + clientName + "</color>　のMPが　<color=#00ffff>" + damage + "</color>　回復した");
    }

    public void UseItemFormat(string itemName)
    {
        SetText("アイテム:<color=#00ff00>" + itemName + "</color>　を使った！");
    }

}
