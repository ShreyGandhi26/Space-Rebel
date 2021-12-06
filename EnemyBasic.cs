using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour
{
    [HideInInspector]
    public bool canPatrol;
    public bool isFacingRight = true;
    public float walkSpeed;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    public float range;
    //public GameObject Bullet;
    //public GameObject MuzzleFalsh;
    //public Transform firePos;
    //public Transform CastPos;
    public GameObject Marker;
    public float Maxhealth;
    public float CurrentHealth;
    public GameObject BloodEffect;
    //public LayerMask rayhitLayer;
    public Animator animator;
    public GameObject target;
    //public GameObject Hand;
    //public Vector2 offset;

    private Rigidbody2D rb;
    private bool needToFlip;
    // private bool canShoot;
    private float castDist;
    //Vector2 Direction;


    // Start is called before the first frame update
    void Start()
    {
        canPatrol = true;
        //canShoot = true;
        CurrentHealth = Maxhealth;
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canPatrol)
        {
            Patrol();
        }
        if (canSeePlayer(range))
        {
            canPatrol = false;
        }
        else
        {
            canPatrol = true;
        }
        if (CurrentHealth <= 0)
        {
            GameManager.EnemiesKilled++;
            Destroy(gameObject);
        }
        if (canPatrol == false)
        {
            animator.SetFloat("Speed", 0);
        }
    }

    bool canSeePlayer(float distance)
    {
        bool val = false;
        float distToPlayer = Vector2.Distance(transform.position, target.transform.position);

        if (distToPlayer < distance)
        {
            val = true;
            GetComponentInChildren<EnemyAim>().enabled = true;
        }
        else
        {
            val = false;
            GetComponentInChildren<EnemyAim>().enabled = false;
        }
        return val;

    }


    private void FixedUpdate()
    {
        if (canPatrol)
        {
            needToFlip = !Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        }
    }

    private void Patrol()
    {
        if (needToFlip || bodyCollider.IsTouchingLayers(groundLayer))
        {
            flip();
        }
        rb.velocity = new Vector2(walkSpeed * 10f * Time.fixedDeltaTime, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }
    public void flip()
    {
        isFacingRight = !isFacingRight;
        canPatrol = false;
        Marker.transform.Rotate(0, 180, 0);
        var thisScale = transform.localScale;
        thisScale.x *= -1;
        transform.localScale = thisScale;
        //firePos.transform.Rotate(0, 180, 0);
        //transform.Rotate(0, 180, 0);
        walkSpeed *= -1;
        canPatrol = true;
    }

    // IEnumerator Fire()
    // {
    //     canShoot = false;
    //     yield return new WaitForSeconds(timeBtwShots);
    //     GameObject newBullet = Instantiate(Bullet, firePos.position, firePos.rotation);
    //     Instantiate(MuzzleFalsh, firePos.position, firePos.rotation);
    //     canShoot = true;
    // }

    public void TakeDamage(float dmg)
    {
        Instantiate(BloodEffect, transform.position, Quaternion.identity);
        CurrentHealth -= dmg;
        FindObjectOfType<AudioManager>().playSound("EnemyDamage");
    }

    private void footsteps()
    {
        FindObjectOfType<AudioManager>().playSound("EnemyWalk");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
