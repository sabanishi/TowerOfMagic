using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoBack : MonoBehaviour
{
   public void OnClick()
    {
        SoundManager.instance.PlaySE(SoundManager.SE_Type.click);
        SceneManager.LoadScene("title");
    }
}
