using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HomingMissile : MonoBehaviour
{
    public Transform target;
    private Rigidbody2D rb;
    public float speed;
    public float rotateSpeed;
    public float fieldOfImpact;
    public float force;
    public LayerMask layerofDamge;
    public GameObject effect;
    private CinemachineImpulseSource source;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Head").transform;
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<CinemachineImpulseSource>();
    }

    private void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.position - rb.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;

        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<SoldierCharacter>() != null)
        {
            Destroy();
        }
        Destroy();
    }

    public void Destroy()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact, layerofDamge);

        foreach (Collider2D obj in objects)
        {
            Vector2 direction = obj.transform.position - transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(direction * force);
            if (obj.GetComponent<SoldierCharacter>() != null)
            {
                obj.GetComponent<SoldierCharacter>().TakeDamage(25);
            }
        }
        Instantiate(effect, transform.position, transform.rotation);
        source.GenerateImpulse();
        Destroy(gameObject);
    }
}
