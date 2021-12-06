using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Bullet : MonoBehaviour
{

    public float speed;
    public Rigidbody2D rb;
    public float timer;

    private CinemachineImpulseSource source;

    private void Start()
    {
        rb.velocity = transform.right * speed;
        source = GetComponent<CinemachineImpulseSource>();
        source.GenerateImpulse();
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("Destroy", timer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        if (other.GetComponent<EnemyBasic>() != null)
        {
            Destroy(gameObject);
            other.GetComponent<EnemyBasic>().TakeDamage(15);
        }
        if (other.GetComponent<HomingMissile>())
        {
            other.GetComponent<HomingMissile>().Destroy();
        }
        if (other.GetComponent<EnemyMid>() != null)
        {
            Destroy(gameObject);
            other.GetComponent<EnemyMid>().TakeDamage(10);
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
