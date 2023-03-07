using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameOverController gameOverController;
    public ScoreController scoreController;
    public Animator animator;
    private Rigidbody2D rb2d;
    private BoxCollider2D boxCollider2D;

    public float jumpForce;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    
    

    //private bool isGrounded= false;
    [SerializeField]
    private float speed;
    [SerializeField] private float fallMultiplier;

    private Vector3 respawnPoint;
    public GameObject FallDetector;
    Vector2 verGravity;
    private bool hasDoubleJumped = false;
    [SerializeField] int jumpCount;
    public int NumberOfJumps = 1;
    int jumpCountAnim;
    private bool isJumpPressed = false;
    //public static GameObject gameObject1;

    private void Awake()
    {

        verGravity = new Vector2(0, -Physics2D.gravity.y);
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        jumpCount = NumberOfJumps;
        jumpCountAnim = NumberOfJumps;
        respawnPoint = transform.position;
    }
    public void PickUpKey()
    {
        Debug.Log("Player has picked up the key ");
        SoundManager.Instance.Play(SoundManager.Sounds.Pickup);
        ScoreController scoreController = new ScoreController();
        scoreController.IncreaseScore(10);


    }
    public void KillPlayer()
    {
        //Debug.Log("player has died");
        //Destroy(gameObject);
        gameOverController.Playerdied();
        SoundManager.Instance.Play(SoundManager.Sounds.PlayerMove);
        //gameOverController.ReloadLevel();
        this.enabled = false;
    }

   

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        MoveCharacter(horizontal, vertical);
        MovementAnimation(horizontal, vertical);
        Jump();
        Crouch();

        FallDetector.transform.position = new Vector2(transform.position.x, FallDetector.transform.position.y);

    }
    private void FixedUpdate()
    {
        
        isJumpPressed = Input.GetKeyDown(KeyCode.Space); // set space bar press
       
    }
    bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;
        }
    }

    void MoveCharacter(float horizontal, float vertical)
    {
        // move character horizontaly 
        Vector3 position = transform.position;
        //delata time 1/30 per sec
        position.x = position.x + horizontal * speed * Time.deltaTime;
        transform.position = position;
        SoundManager.Instance.Play(SoundManager.Sounds.PlayerMove);
    }
    public void JumpSound()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.Jump);
    }
    public void MovementSound()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.PlayerMove);
    }

    private void MovementAnimation(float horizontal, float vertical)
    {
      
        animator.SetFloat("horizontal", Mathf.Abs(horizontal));

        // flip
        Vector3 scale = transform.localScale;
        if (horizontal < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (horizontal > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
        //jump animaTION
        if (isGrounded())
        {
            animator.ResetTrigger("jump");

            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("jump");
            }
        }
        else
        {
            animator.ResetTrigger("isGrounded");
        }
    }

    //JUMP FUNCTION
    void Jump()
    {
       
        if (isGrounded() && isJumpPressed)
        {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            hasDoubleJumped = false;
           
        }
        else if (!isGrounded() && isJumpPressed && !hasDoubleJumped)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            hasDoubleJumped = true;
           
        }
        if (rb2d.velocity.y < 0)
        {
            rb2d.velocity -= verGravity * fallMultiplier * Time.deltaTime;
        }
        isJumpPressed = false;
    }
    //crouch function 
    void Crouch()
    {
        Vector2 offset = boxCollider2D.offset;
        Vector2 size = boxCollider2D.size;
        if (Input.GetKey(KeyCode.S))
        {
            offset.x = -0.1136989f;
            offset.y = 0.5907992f;
            size.x = 0.8533424f;
            size.y = 1.240493f;
            animator.SetBool("Crouch", true);
        }
        else
        {
            offset.x = 0.02338372f;
            offset.y = 0.9791999f;
            size.x = 0.5791771f;
            size.y = 2.017294f;
            animator.SetBool("Crouch", false);
        }
        boxCollider2D.offset = offset;
        boxCollider2D.size = size;
    }
}








