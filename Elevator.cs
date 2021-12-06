using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform downPos;
    public Transform upperPos;
    public float speed;
    public bool isElevatorActive;

    bool isElevatorDown;

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y <= downPos.position.y)
        {
            isElevatorDown = true;
        }
        else if (transform.position.y >= upperPos.position.y)
        {
            isElevatorDown = false;
        }
        StartElevator();
    }

    private void StartElevator()
    {
        if (isElevatorDown && isElevatorActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, upperPos.position, speed * Time.deltaTime);
        }
        else if (isElevatorDown == false && isElevatorActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, downPos.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<SoldierCharacter>() != null)
        {
            other.GetComponent<SoldierCharacter>().CurrentHealth = 0;
        }
    }

}
