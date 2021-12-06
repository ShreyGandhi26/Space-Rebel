using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMoving : MonoBehaviour
{

    public float speed;

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 1f * speed));
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<SoldierCharacter>() != null)
        {
            other.GetComponent<SoldierCharacter>().CurrentHealth -= 10;
        }
        if (other.GetComponent<TechncianCharacter>() != null)
        {
            other.GetComponent<TechncianCharacter>().CurrentHealth -= 10;
        }
    }

}
