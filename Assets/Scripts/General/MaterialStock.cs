using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialStock : MonoBehaviour
{
    public Material R;
    public Material B;
    public Material G;
    public Material Y;
    public Material BandG;
    public Material BandY;
    public Material RandB;
    public Material RandG;
    public Material RandY;
    public Material YandG;
    public Material RandYandG;
    public Material RandBandG;
    public Material RandBandY;
    public Material BandYandG;
    public Material Black;
    public Material White;

    public Material Player1Color;
    public Material Player2Color;
    public Material Player3Color;

    public static MaterialStock instance;
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
}
