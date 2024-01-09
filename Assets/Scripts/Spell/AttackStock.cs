using UnityEngine;
using System.Collections;

public class AttackStock : MonoBehaviour
{
    public GameObject ItemEffect;

    public GameObject damageEffect;
    public GameObject normalSkill;
    public GameObject fire;
    public GameObject attackUp;
    public GameObject burn;
    public GameObject phoenix;
    public GameObject water;
    public GameObject defenceUp;
    public GameObject sprash;
    public GameObject freeze;
    public GameObject wind;
    public GameObject agilityUp;
    public GameObject blast;
    public GameObject speedUp;
    public GameObject stone;
    public GameObject heal;
    public GameObject quake;
    public GameObject comet;
    public GameObject areaHeal;
    public GameObject explosion;
    public GameObject bigBurn;
    public GameObject superNova;
    public GameObject volcano;
    public GameObject meteo;
    public GameObject stome;
    public GameObject thudar;
    public GameObject ressrection;
    public GameObject healVeil;
    public GameObject craft;
    public GameObject warp;

    public static AttackStock instance;
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
        DontDestroyOnLoad(gameObject);

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
