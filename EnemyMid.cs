using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMid : MonoBehaviour
{
    [Header("Patroling")]
    public bool canPatrol;
    public Animator animator;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    public bool isFacingRight;
    public float walkSpeed;
    private bool needToFlip;

    [Header("Combat")]
    public float Speed;
    public float stoppingDist;
    public float retreatDist;
    public float backToPetrolDist;
    public Transform Player;
    public float range;

    [Header("UI_Marker")]
    public GameObject Marker;

    [Header("RayCast")]
    public Transform CastPos;
    public LayerMask rayhitLayer;
    private float castDist;
    private Rigidbody2D rb;


    [Header("Health")]
    public float Maxhealth;
    public float CurrentHealth;
    public GameObject BloodEffect;

    // private bool gettingShot;

    // Start is called before the first frame update
    void Start()
    {
        canPatrol = true;
        //canShoot = true;
        //gettingShot = false;
        CurrentHealth = Maxhealth;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canPatrol)
        {
            Patrol();
        }
        if (canPatrol == false)
        {
            EnteringCombat();
        }

        if (canSeePlayer(range))
        {
            canPatrol = false;
        }

        if (CurrentHealth <= 0)
        {
            GameManager.EnemiesKilled++;
            Destroy(gameObject);
        }
        if (canPatrol == false)
        {
            //animator.SetFloat("Speed", 0);
        }
        // if (gettingShot)
        // {
        //     flip();
        // }
    }

    private void FixedUpdate()
    {
        if (canPatrol)
        {
            needToFlip = !Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        }
    }


    bool canSeePlayer(float distance)
    {
        bool val = false;
        float distToPlayer = Vector2.Distance(transform.position, Player.transform.position);

        if (distToPlayer < distance)
        {
            val = true;
            GetComponentInChildren<EnemyMidAim>().enabled = true;
            EnteringCombat();
        }
        else
        {
            val = false;
            GetComponentInChildren<EnemyMidAim>().enabled = false;
        }
        return val;
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
        walkSpeed *= -1;
        canPatrol = true;
    }

    void EnteringCombat()
    {
        if (Vector2.Distance(transform.position, Player.position) > backToPetrolDist)
        {
            canPatrol = true;
        }
        else if (Vector2.Distance(transform.position, Player.position) > stoppingDist)
        {
            animator.SetFloat("Speed", 1);
            transform.position = Vector2.MoveTowards(transform.position, Player.position, Speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, Player.position) < stoppingDist && Vector2.Distance(transform.position, Player.position) > retreatDist)
        {
            animator.SetFloat("Speed", 0);
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, Player.position) < retreatDist)
        {
            animator.SetFloat("Speed", 1);
            transform.position = Vector2.MoveTowards(transform.position, Player.position, -Speed * Time.deltaTime);
        }
    }


    public void TakeDamage(float dmg)
    {
        // if (canPatrol == true)
        // {
        //     gettingShot = true;
        // }
        FindObjectOfType<AudioManager>().playSound("EnemyDamage");
        Instantiate(BloodEffect, transform.position, Quaternion.identity);
        CurrentHealth -= dmg;
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
