using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    private void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        if (other.GetComponent<SoldierCharacter>() != null)
        {
            Destroy(gameObject);
            other.GetComponent<SoldierCharacter>().TakeDamage(10);
        }
        if (other.GetComponent<TechncianCharacter>() != null)
        {
            Destroy(gameObject);
            other.GetComponent<TechncianCharacter>().CurrentHealth -= 10;
        }
    }
}
