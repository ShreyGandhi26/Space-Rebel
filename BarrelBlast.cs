using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class BarrelBlast : MonoBehaviour
{
    public GameObject BlastEffect;
    // public BoxCollider2D box1;
    // public BoxCollider2D box2;
    public float fieldOfImpact;
    public float force;
    public float damage;
    // public bool respawn;
    // public Vector2 orignalPos;
    public LayerMask layerofDamge;
    private CinemachineImpulseSource source;

    private void Awake()
    {
        source = GetComponent<CinemachineImpulseSource>();
        // orignalPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Bullet>() != null)
        {
            explode();
        }
        if (other.GetComponent<EnemyBullet>() != null)
        {
            explode();
            FindObjectOfType<PushableObjects>().isLiftingBox = false;
        }
    }

    private void explode()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact, layerofDamge);

        foreach (Collider2D obj in objects)
        {
            Vector2 direction = obj.transform.position - transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(direction * force);
            if (obj.GetComponent<SoldierCharacter>() != null)
            {
                obj.GetComponent<SoldierCharacter>().TakeDamage(damage);
            }
            // else if (obj.GetComponent<TechncianCharacter>() != null)
            // {
            //     obj.GetComponent<TechncianCharacter>().CurrentHealth -= damage;
            // }
            else if (obj.GetComponent<EnemyBasic>() != null)
            {
                obj.GetComponent<EnemyBasic>().CurrentHealth -= damage;
            }
            else if (obj.GetComponent<NormalBarrel>() != null)
            {
                Destroy(obj.GetComponent<NormalBarrel>().gameObject);
            }
        }
        Instantiate(BlastEffect, transform.position, Quaternion.identity);
        source.GenerateImpulse();

        Destroy(gameObject);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldOfImpact);
    }
}
