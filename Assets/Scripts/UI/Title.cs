using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField] private Text gameStart;
    [SerializeField] private Text manual;
    [SerializeField] private Text exit;
    [SerializeField] private bool isBgmResponsible;

    public void Start()
    {
        if (isBgmResponsible)
        {
            SoundManager.instance.PlayBGM(SoundManager.BGM_Type.Title);
        }
    }

    public void OnClickGameStart()
    {
        SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
        SceneManager.LoadScene("name");
    }

    public void OnClickManual()
    {
        SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
        SceneManager.LoadScene("manual");
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnEnterGameStart()
    {
        gameStart.color = new Color(255, 0, 0);
    }

    public void OnExitGameStart()
    {
        gameStart.color = new Color(255, 214, 0);
    }

    public void OnEnterManual()
    {
        manual.color = new Color(255, 0, 0);
    }

    public void OnExitManual()
    {
        manual.color = new Color(255, 214, 0);
    }

    public void OnEnterExit()
    {
        exit.color = new Color(255, 0, 0);
    }

    public void OnExitExit()
    {
        exit.color = new Color(255, 214, 0);
    }
}
