using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private float _jumpForce = 400f;                          // Amount of force added when the player jumps.

    [Range(0, 1)]
    [SerializeField]
    private float _crouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%

    [Range(0, .3f)]
    [SerializeField]
    private float _movementSmoothing = .05f;  // How much to smooth out the movement

    [SerializeField]
    private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;

    [SerializeField]
    private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character

    [SerializeField]
    private Transform _groundCheck;                           // A position marking where to check if the player is grounded.

    [SerializeField]
    private Transform _ceilingCheck;                          // A position marking where to check for ceilings


    public CapsuleCollider2D _crouchCollider;                // A collider that will be disabled when crouching

    //public Vector2 


    const float _isGroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool isGrounded;            // Whether or not the player is grounded.
    const float _isAnyCeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    public Rigidbody2D rb;
    public Animator animator;
    public bool FacingRight = true;  // For determining which way the player is currently facing.
    public Vector2 NormalHeight;
    public Vector2 crouchHeight;

    private Vector3 Velocity = Vector3.zero;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;

    private PushableObjects pushable;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pushable = GetComponent<PushableObjects>();
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, _isGroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }


    public void Move(float move, bool crouch, bool jump)
    {
        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(_ceilingCheck.position, _isAnyCeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (isGrounded || m_AirControl)
        {

            // If crouching
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move *= _crouchSpeed;
                animator.SetBool("IsCrouch", true);
                animator.SetBool("IsCrouchMove", true);
                // Disable one of the colliders when crouching
                if (_crouchCollider != null)
                    _crouchCollider.direction = CapsuleDirection2D.Horizontal;
                _crouchCollider.size = crouchHeight;
            }
            else
            {
                // Enable the collider when not crouching
                if (_crouchCollider != null)
                    _crouchCollider.direction = CapsuleDirection2D.Vertical;
                _crouchCollider.size = NormalHeight;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
                animator.SetBool("IsCrouch", false);
                animator.SetBool("IsCrouchMove", false);
            }

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(move));
            // And then smoothing it out and applying it to the character
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref Velocity, _movementSmoothing);

            // If the input is moving the player right and the player is facing left...
            // if (move > 0 && !FacingRight)
            // {
            //     // ... flip the player.
            //     Flip();
            // }
            // Otherwise if the input is moving the player left and the player is facing right...
            // else if (move < 0 && FacingRight)
            // {
            //     // ... flip the player.
            //     Flip();
            // }
        }
        // If the player should jump...
        if (isGrounded && jump && !crouch)
        {
            // Add a vertical force to the player.
            isGrounded = false;
            animator.SetTrigger("Jump");
            //rb.AddForce(new Vector2(0f, _jumpForce));
            rb.velocity = new Vector2(rb.velocity.x, _jumpForce);
        }
    }


    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        FacingRight = !FacingRight;
        var thisScale = transform.localScale;
        thisScale.x *= -1;
        transform.localScale = thisScale;
        //transform.Rotate(0, 180f, 0);
        pushable.distance *= -1;
    }

}
