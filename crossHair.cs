using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crossHair : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //print(Cursor.visible);
        Vector2 mouseCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mouseCursorPos;
    }
}
