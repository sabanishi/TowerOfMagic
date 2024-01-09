using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameQuitButton : MonoBehaviour
{
    public void ButtonClick()
    {
        //GameObject.FindWithTag("GameManager").GetComponent<GameManager>().isFinish = true;
        SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
        SceneManager.LoadScene("title");
    }
}
