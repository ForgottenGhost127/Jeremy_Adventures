using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    #region Fields
    [Header("Movimiento")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 6f;

    [Header("Audio")]
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private float footstepDelay = 0.4f;
    [SerializeField] private float footstepVolume = 0.7f;

    [Header("Components")]
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private AudioSource audioSource;

    [Header("Input")]
    public float moveX;
    private bool isGrounded = false;

    private float footstepTimer = 0f;
    private bool wasMovingLastFrame = false;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.volume = footstepVolume;
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleFootstepAudio();
        UpdateAnimations();
    }
    #endregion

    #region Private Methods
    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        //Debug.Log("Move Input: " + moveInput);
        rb.velocity = new Vector2(moveInput * _moveSpeed, rb.velocity.y);

        if (moveInput > 0)
            sr.flipX = true;
        else if (moveInput < 0)
            sr.flipX = false;
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, _jumpForce);
            isGrounded = false;
            anim.SetBool("IsJumping", true);
        }
    }

    private void HandleFootstepAudio()
    {
        float moveInput = Mathf.Abs(Input.GetAxisRaw("Horizontal"));
        bool isMoving = moveInput > 0 && isGrounded;

        if (isMoving)
        {
            footstepTimer += Time.deltaTime;
            if (footstepTimer >= footstepDelay)
            {
                PlayFootstepSound();
                footstepTimer = 0f;
            }
        }
        else
        {
            footstepTimer = 0f;
        }

        wasMovingLastFrame = isMoving;
    }

    private void PlayFootstepSound()
    {
        if (footstepSounds != null && footstepSounds.Length > 0 && audioSource != null)
        {
            AudioClip randomFootstep = footstepSounds[UnityEngine.Random.Range(0, footstepSounds.Length)];
            if (randomFootstep != null)
            {
                audioSource.PlayOneShot(randomFootstep, footstepVolume);
            }
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
