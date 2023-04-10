using UnityEngine;

public class movement : MonoBehaviour
{
    private float horizontal; 
    private float speed = 6f;
    private float jumpingPower = 12f;
    private bool isFacingRight = true;

    private Rigidbody2D rb;
    private Animator _animator;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator= GetComponent<Animator>();
    }

    void  Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _animator.Play("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
    
        

        Flip();
    }
    
    private void FixedUpdate()
        {
            if (IsGrounded()&&horizontal!=0)
            {
                _animator.SetBool("walk",true);
            }
            else
            {
                _animator.SetBool("walk",false);
            }
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            
        }
    }
}

