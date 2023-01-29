using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
    public Animator animator;
    public float speed;
    public float jump;
    private Rigidbody2D rb2d;
    public float input;
    private void Awake()
    {
        Debug.Log("Player controller is awake");
        rb2d = gameObject.GetComponent<Rigidbody2D>();

    }

    //private void OnCollision2D(Collision2D collision)
    //{
    //   Debug.Log("Collision: " + collision.gameObject.name);
    //}
    private void Update()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        MoveCharacter(horizontal,vertical);
        PlayMovementAnimation(horizontal, vertical);
        



    }
    private void MoveCharacter(float horizontal, float vertical)
    {
        // move character horizontaly 
        Vector3 position = transform.position;
        //delata time 1/30 per sec
        position.x = position.x + horizontal * speed * Time.deltaTime;
        transform.position = position;
        // move character horizontaly 

        if (vertical > 0)
        {
            rb2d.AddForce(new Vector2(0f, jump), ForceMode2D.Force);

        }
        if (input > 0)
        {
            position.x = position.x + horizontal * speed * Time.deltaTime;
            transform.position = position;
        }

    }

    private void PlayMovementAnimation(float horizontal, float vertical)
    {
        animator.SetFloat("horizontal", Mathf.Abs(horizontal));

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



        //jump
        if (vertical > 0)
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }


        if (Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("Crouch", true);
        }
        else
        {
            animator.SetBool("Crouch", false);
        }
        //walk
       

    }
   

}
