using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ManualManager : MonoBehaviour
{
    [SerializeField] private GameObject page1;
    [SerializeField] private GameObject page2;
    [SerializeField] private GameObject page3;
    [SerializeField] private GameObject page4;
    [SerializeField] private GameObject page5;
    [SerializeField] private GameObject page6;
    [SerializeField] private GameObject page7;
    [SerializeField] private GameObject page8;
    [SerializeField] private GameObject page9;
    [SerializeField] private GameObject page10;
    [SerializeField] private GameObject page11;

    [SerializeField] private SpriteRenderer topSprite;

    [SerializeField] private GameObject UpButton;
    [SerializeField] private GameObject DownButton;

    private int pageNum;
    private bool canClick;
    // Use this for initialization
    void Start()
    {
        pageNum = 1;
        RemakePage();
        canClick = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Click(int index)
    {
        if (canClick)
        {
            SoundManager.instance.PlayManualSE(index);
        }
    }

    public void PageUp()
    {
        SoundManager.instance.PlayManualSE(15);
        pageNum++;
        StartCoroutine(ChangePage());
    }

    public void PageDowm()
    {
        SoundManager.instance.PlayManualSE(15);
        pageNum--;
        StartCoroutine(ChangePage());
    }

    private void RemakePage()
    {
        page1.SetActive(false);
        page2.SetActive(false);
        page3.SetActive(false);
        page4.SetActive(false);
        page5.SetActive(false);
        page6.SetActive(false);
        page7.SetActive(false);
        page8.SetActive(false);
        page9.SetActive(false);
        page10.SetActive(false);
        page11.SetActive(false);

        UpButton.SetActive(true);
        DownButton.SetActive(true);
        switch (pageNum)
        {
            case 1:
                page1.SetActive(true);
                DownButton.SetActive(false);
                break;
            case 2:
                page2.SetActive(true);
                break;
            case 3:
                page3.SetActive(true);
                break;
            case 4:
                page4.SetActive(true);
                break;
            case 5:
                page5.SetActive(true);
                break;
            case 6:
                page6.SetActive(true);
                break;
            case 7:
                page7.SetActive(true);
                break;
            case 8:
                page8.SetActive(true);
                break;
            case 9:
                page9.SetActive(true);
                break;
            case 10:
                page10.SetActive(true);
                break;
            case 11:
                page11.SetActive(true);
                UpButton.SetActive(false);
                break;
            default:
                break;
        }
    }

    IEnumerator ChangePage()
    {
        canClick = false;
        Color c = topSprite.color;
        c.a = 0;
        topSprite.color = c;

        bool isLoopOut = false;
        while (!isLoopOut)
        {
            yield return null;
            c.a += 0.05f;
            topSprite.color = c;
            if (c.a >= 1.0)
            {
                c.a = 1.0f;
                topSprite.color = c;
                isLoopOut = true;
            }
        }

        RemakePage();

        isLoopOut = false;
        while (!isLoopOut)
        {
            yield return null;
            c.a -= 0.05f;
            topSprite.color = c;
            if (c.a <= 0)
            {
                c.a = 0f;
                topSprite.color = c;
                isLoopOut = true;
            }
        }
        canClick = true;
    }

    public void GoBack()
    {
        SceneManager.LoadScene("title");
    }
}
