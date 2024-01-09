using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private float discardCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        discardCount += Time.deltaTime;
        if (discardCount >= 0.75f)
        {
            Destroy(gameObject);
        }
    }
}
