using UnityEngine;
using System.Collections;

public class CoroutineHandler: MonoBehaviour
{
    protected static CoroutineHandler m_Instance;
    public static CoroutineHandler instance
    {
        get
        {
            if (m_Instance == null)
            {
                GameObject o = new GameObject("CoroutineHandler");
                DontDestroyOnLoad(o);
                m_Instance = o.AddComponent<CoroutineHandler>();
            }
            return m_Instance;
        }
    }

    public void OnDisable()
    {
        if (m_Instance)
        {
            Destroy(m_Instance.gameObject);
        }
    }

    public static Coroutine StartStaticCoroutine(IEnumerator coroutine)
    {
        return instance.StartCoroutine(coroutine);
    }

}
