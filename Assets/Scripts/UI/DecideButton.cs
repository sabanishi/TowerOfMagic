using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DecideButton : MonoBehaviour
{
    [SerializeField] private InputField inputField1;
    [SerializeField] private InputField inputField2;
    [SerializeField] private InputField inputField3;
    [SerializeField] private GameObject Hukidasi;
    [SerializeField] private GameObject DecideNamePanel;

    public void OnClick()
    {
        if (inputField1.text.Trim().Length == 0 ||
            inputField2.text.Trim().Length == 0 ||
            inputField3.text.Trim().Length == 0)
        {
            SoundManager.instance.PlaySE(SoundManager.SE_Type.nameError);
            Hukidasi.SetActive(true);
        }
        else
        {
            SceneManager.sceneLoaded += GameSceneLoaded;
            SoundManager.instance.PlaySE(SoundManager.SE_Type.gameStart);
            SoundManager.instance.StopBGM();
            MovingObjectInfomation info1 = new MovingObjectInfomation(1);
            MovingObjectInfomation info2 = new MovingObjectInfomation(1);
            MovingObjectInfomation info3 = new MovingObjectInfomation(1);
            info1.Name = inputField1.text;
            info2.Name = inputField2.text;
            info3.Name = inputField3.text;
            info1.SetMasteryPoint(0,0,0, 0);
            info2.SetMasteryPoint(0,0, 0,0);
            info3.SetMasteryPoint(0,0,0,0);
            info1.ChoiceMaterialForPlayer(MaterialStock.instance.Player1Color);
            info2.ChoiceMaterialForPlayer(MaterialStock.instance.Player2Color);
            info3.ChoiceMaterialForPlayer(MaterialStock.instance.Player3Color);
            SoundManager.PlayerInfomations[0] = info1;
            SoundManager.PlayerInfomations[1] = info2;
            SoundManager.PlayerInfomations[2] = info3;
            SceneManager.LoadScene("main");
        }
    }

    private void GameSceneLoaded(Scene next,LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= GameSceneLoaded;
    } 
}
