using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSpan : MonoBehaviour
{
    public float timer;


    // Update is called once per frame
    void Update()
    {
        Invoke("Destroy", timer);
    }
    void Destroy()
    {
        Destroy(gameObject);
    }
}
