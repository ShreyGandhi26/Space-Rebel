using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSwitch : MonoBehaviour
{
    public Transform technician;
    public GameObject lever;
    public SpriteRenderer colour;
    public Elevator LiftToOperate;
    public float range;

    bool Switchoff = true;
    Quaternion rotation;


    private void Start()
    {
        rotation = lever.transform.rotation;
    }


    private void Update()
    {

        if (Switchoff)
        {

            colour.color = Color.red;
            if (Vector2.Distance(technician.position, transform.position) < range && Input.GetKeyDown(KeyCode.F))
            {
                Switchoff = false;
                rotation *= Quaternion.Euler(0, 0, -80);
                LiftToOperate.isElevatorActive = true;
            }
        }
        else
        {
            lever.transform.rotation = Quaternion.Lerp(lever.transform.rotation, rotation, Time.time * .05f);
            colour.color = Color.green;
        }

    }


}
