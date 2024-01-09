using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Runtime.InteropServices;

public class GameOverManager : MonoBehaviour
{
    [DllImport("__Internal")] private static extern void OpenNewTab(string URL); 
    [DllImport("__Internal")] private static extern void OpenNewWindow(string URL);
    [SerializeField] SpriteRenderer image;
    public bool isGameOver;
    string url;
    public void Start()
    {
        Material material = GameManager.instance.GetPlayerController().GetPlayer().GetNowMovingObjectInfo().material;
        image.material = material;

        int level = GameManager.instance.GetLevel();
        string Name1 = GameManager.instance.GetPlayerInfo1().Name;
        string Name2 = GameManager.instance.GetPlayerInfo2().Name;
        string Name3 = GameManager.instance.GetPlayerInfo3().Name;
        string text;
        if (isGameOver)
        {
            text = "【TowerOfMagic】" + Name1 + "と" + Name2 + "と" + Name3 + "の三人は魔法の塔" + GameManager.instance.GetLevel()+"階で分からされました...";
        }
        else
        {
            text = "【TowerOfMagic】" + Name1 + "と" + Name2 + "と" + Name3 + "の三人は魔法の塔を踏破しました！";
        }
        
        var a = UnityWebRequest.EscapeURL(text);

        string link = "https://sabanisi.github.io/TowerOfMagic/";
        //string link = "https://sabanisi.github.io/TowerOfMagicAlphaVer/";
        var b = UnityWebRequest.EscapeURL(link);

        url = "https://twitter.com/intent/tweet?text=" + a + "&url=" + b;
    } 

    public void GoTilte()
    {
        Destroy(GameManager.instance.gameObject);
        SceneManager.LoadScene("title");
    }

    public void TweetEnter()
    {
        OpenNewWindow(url);
    }

    public void Tweet()
    {

#if UNITY_WEBGL
        OpenNewTab(url);
#else
         Application.OpenURL(url);
        

#endif
    }
}
