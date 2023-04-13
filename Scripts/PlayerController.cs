using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Jobs;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    Vector2 moveInput;
    public float jumpForce;
    private Rigidbody2D rig;
    private Animator animator;
    public float jumpSpeed = 8f;

    [SerializeField]
    private bool _IsMoving = false;
    public bool IsMoving
    {
        get
        {
            return _IsMoving;
        }
        private set
        {
            _IsMoving = value;
            animator.SetBool("IsMoving", value);
        }
    }

    [SerializeField]
    private bool _IsJumping = false;
    [SerializeField]
    private bool _IsGrounded = false;

    public bool IsJumping
    {
        get
        {
            return _IsJumping;
        }
        set
        {
            _IsJumping = value;
            animator.SetBool("IsJumping", value);
        }
    }

    public bool IsGrounded
    {
        get
        {
            return _IsGrounded;
        }
        set
        {
            _IsGrounded = value;
        }
    }

    public bool _IsFacingRight = true;

    public bool IsFacingRight
    {
        get
        {
            return _IsFacingRight;
        }
        private set
        {
            if (_IsFacingRight != value)
            {
                //Flip the local scale to make the player face the opposite direction
                /*Vector3 rot = transform.rotation.eulerAngles;
                rot = new Vector3(rot.x, rot.y + 180, rot.z);
                transform.rotation = Quaternion.Euler(rot);*/
                transform.localScale *= new Vector2(-1, 1);
            }
            _IsFacingRight = value;
        }
    }

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");


    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * walkSpeed, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            //Face the right
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            //Face the left
            IsFacingRight = false;
        }
    }

    /*public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            IsJumping = true;
        }
        else if (context.canceled)
        {

        }
    }*/

    void Jump()
    {
        /*
            if (Input.GetButtonDown("Jump") && !IsGrounded)
            {
                rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                animator.SetBool("IsJumping", true);
                IsGrounded= false;
                
            }
            else if (!IsGrounded)
            {
                animator.SetBool("IsJumping", false);

            }
        */
        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            IsJumping = !IsJumping;
            IsGrounded = !IsGrounded;
        }
        else if (IsGrounded && IsJumping)
        {
            IsJumping = !IsJumping;
        }
    }
}