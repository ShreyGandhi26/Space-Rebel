using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateMarker : MonoBehaviour
{
    RectTransform marker;
    Vector2 EndPos;
    Vector2 currentPos;


    // Start is called before the first frame update
    void Start()
    {
        marker = GetComponent<RectTransform>();
        EndPos = new Vector2(0f, 0.7f);
        currentPos = marker.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        marker.anchoredPosition = Vector2.Lerp(currentPos, EndPos, Mathf.PingPong(Time.time, .5f));
    }

}
