using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TechncianCharacter : MonoBehaviour
{
    public CharacterController controller;
    public float runSpeed;
    public GameObject Marker;
    // public GameObject Cam;
    //public Animator animator;
    public static bool canInteract = false;
    public float Maxhealth;
    public float CurrentHealth;


    float HorizontalMove;
    bool isJumping = false;
    bool isCrouching = false;
    public bool canMove = true;

    private void Awake()
    {
        CurrentHealth = Maxhealth;
    }

    private void Start()
    {
        canInteract = false;

    }

    // Update is called once per frame
    void Update()
    {
        Marker.transform.eulerAngles = new Vector3(0, 0, 0);
        // Cam.transform.eulerAngles = new Vector3(0, 0, 0);
        if (canMove)
        {
            HorizontalMove = Input.GetAxisRaw("Horizontal");
            //animator.SetFloat("Speed", Mathf.Abs(HorizontalMove));

            if (Input.GetButtonDown("Jump"))
            {
                isJumping = true;
                //animator.SetBool("IsJumping", true);
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

        }
        if (CurrentHealth <= 0)
        {
            controller.animator.SetBool("IsDead", true);
            canMove = false;
            Invoke("Dead", 1f);
        }
    }
    public void onLanding()
    {
        // animator.SetBool("IsJumping", false);
    }

    public void onCrouching(bool isCrouching)
    {
        // animator.SetBool("IsCrawling", isCrouching);
    }


    private void FixedUpdate()
    {
        controller.Move(HorizontalMove * Time.fixedDeltaTime * runSpeed, isCrouching, isJumping);
        isJumping = false;
    }

    void Dead()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("GameOver");
    }
}
