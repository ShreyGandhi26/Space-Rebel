using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoldierCharacter : MonoBehaviour
{

    public CharacterController controller;
    //public Animator animator;
    public float runSpeed;
    public GameObject Marker;
    //public GameObject Cam;
    public float Maxhealth;
    public float CurrentHealth;
    public float range;
    //public Transform technician;
    public DialogueManager dialogue;

    float HorizontalMove;
    bool isJumping = false;
    bool isCrouching = false;
    public bool canMove = true;
    public GameObject BloodEffect;


    private void Start()
    {
        CurrentHealth = Maxhealth;
    }

    private void Update()
    {
        //Marker.transform.eulerAngles = new Vector3(0, 0, 0);
        // Cam.transform.eulerAngles = new Vector3(0, 0, 0);
        if (canMove)
        {
            //controller.rb.freezeRotation = true;
            controller.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            HorizontalMove = Input.GetAxisRaw("Horizontal");

            // animator.SetFloat("Speed", Mathf.Abs(HorizontalMove));

            if (Input.GetButtonDown("Jump"))
            {
                isJumping = true;
                FindObjectOfType<AudioManager>().playSound("Jump");
                // animator.SetTrigger("Jump");
            }
            if (Input.GetButtonDown("Crouch"))
            {
                isCrouching = true;
                controller._crouchCollider.offset = new Vector2(0, .7f);
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                isCrouching = false;
                controller._crouchCollider.offset = new Vector2(0, .9f);
            }
        }
        else
        {
            controller.rb.velocity = Vector3.zero;
            controller.rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
            controller.animator.SetFloat("Speed", 0);
        }
        if (CurrentHealth <= 0)
        {
            canMove = false;
            GetComponentInChildren<Weapon>().canShoot = false;
            controller.animator.SetBool("IsDead", true);
            Invoke("Dead", 1f);
        }
        if (CurrentHealth >= 100)
        {
            CurrentHealth = Maxhealth;
        }

        // if (Vector2.Distance(technician.position, transform.position) < range && Input.GetKeyDown(KeyCode.T))
        // {
        //     dialogue.canTalk = true;
        // }
    }

    void Dead()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("GameOver");
    }

    public void onLanding()
    {
        //animator.SetBool("IsJumping", false);
    }

    private void FixedUpdate()
    {
        controller.Move(HorizontalMove * Time.fixedDeltaTime * runSpeed, isCrouching, isJumping);
        isJumping = false;
    }

    public void TakeDamage(float dmg)
    {
        if (gameObject.GetComponentInChildren<Shield>().isUsingShield == false)
        {
            FindObjectOfType<AudioManager>().playSound("Damage");
            Instantiate(BloodEffect, transform.position, Quaternion.identity);
            CurrentHealth -= dmg;
        }
    }

    private void footsteps()
    {
        FindObjectOfType<AudioManager>().playSound("Walk");
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Elevator>())
        {
            transform.parent = other.gameObject.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Elevator>())
        {
            transform.parent = null;
        }
    }
}
