using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    #region Fields
    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 12f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private bool isGrounded = false;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        UpdateAnimations();
    }
    #endregion

    #region Private Methods
    private void HandleMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput > 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(1, 1, 1);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            anim.SetBool("IsJumping", true);
        }
    }

    private void UpdateAnimations()
    {
        float moveInput = Mathf.Abs(Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("Speed", moveInput);

        if (isGrounded)
            anim.SetBool("IsJumping", false);
    }
    #endregion

    #region Unity Collision Callbacks
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    #endregion

}
