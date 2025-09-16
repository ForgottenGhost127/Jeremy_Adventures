using UnityEngine;
using System;

public class EnemyAI : MonoBehaviour
{
    #region Fields
    [Header("Movement")]
    public float speed = 2f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator anim;
    private bool movingRight = true;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        rb.velocity = new Vector2(speed * (movingRight ? 1 : -1), rb.velocity.y);
        anim.SetBool("isWalking", Mathf.Abs(rb.velocity.x) > 0.1f);

        if (!Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer))
        {
            Flip();
        }
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void Flip()
    {
        movingRight = !movingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    #endregion

}
