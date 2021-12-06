using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public float healAmount;
    public Vector3 startforce;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(startforce, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 1f, 0));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<SoldierCharacter>() != null)
        {
            Destroy(gameObject);
            other.GetComponent<SoldierCharacter>().CurrentHealth += healAmount;
        }
    }
}
