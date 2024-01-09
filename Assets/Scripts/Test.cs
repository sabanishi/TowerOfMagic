using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Test : MonoBehaviour
{
    SpriteRenderer sprite;
    public Material material;
    public MaterialStock stock;
    // Use this for initialization
    void Start()
    { 
        
    }

    // Update is called once per fra
    [System.Obsolete]
    void Update()
    {
   /*     if (Input.GetButtonDown("Vertical"))
        {
            var a = UnityWebRequest.EscapeURL("わたしは35階で分からされました");
            var url = "https://twitter.com/intent/tweet?text="+a;

#if UNITY_WEBGL
                Application.ExternalEval(string.Format("window.open('{0}','blank')",url));
#else
            // Application.OpenURL(url);
        

#endif


        }*/
        sprite = GetComponent<SpriteRenderer>();
        //sprite.material = stock.Blue;
        material.SetColor("_KeyColor", new Color(0, 0, 0));
    }
}
