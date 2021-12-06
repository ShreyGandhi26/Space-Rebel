using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTV : MonoBehaviour
{
    public bool Disabled = false;
    public float Range;
    public GameObject target;
    public GameObject alarm;
    public GameObject gun;
    public GameObject bullet;
    public float fireRate;
    public float force;
    public bool detected = false;
    public GameObject shootPos;
    //public Vector2 offset;

    float nextTimeToFire;
    Vector2 Direction;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Disabled == false)
        {
            if (detected == false)
            {
                alarm.GetComponent<SpriteRenderer>().color = Color.green;
            }

            Vector2 targetPos = target.transform.position;
            Direction = targetPos - (Vector2)shootPos.transform.position;

            RaycastHit2D rayInfo = Physics2D.Raycast(shootPos.transform.position, Direction, Range);
            if (rayInfo)
            {
                if (rayInfo.collider.gameObject.tag == "Player" && rayInfo.distance <= Range - 1)
                {
                    detected = true;
                    alarm.GetComponent<SpriteRenderer>().color = Color.red;
                }
                else
                {
                    detected = false;
                    alarm.GetComponent<SpriteRenderer>().color = Color.green;
                }
            }
            if (detected == true)
            {
                gun.transform.up = Direction;
                if (Time.time > nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1 / fireRate;
                    shoot();
                }
            }
        }
        if (Disabled == true)
        {
            alarm.GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }

    void shoot()
    {
        GameObject bulletins = Instantiate(bullet, shootPos.transform.position, shootPos.transform.rotation);
        bulletins.GetComponent<Rigidbody2D>().AddForce(Direction * force);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Bullet>() != null)
        {
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }


}
