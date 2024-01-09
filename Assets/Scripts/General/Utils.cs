using System.Collections.Generic;
using UnityEngine;
public class Utils
{
    public static bool RandomJadge(float rate)//rateの確立でtrueを返す
    {
        return Random.value < rate;
    }

    public static int GetRandomInt(int min, int max)
    {
        return min + Mathf.FloorToInt(Random.Range(0f,0.999f) * (max - min + 1));
    }

    public static T GetRandom<T>(List<T> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count)];
    }
}
