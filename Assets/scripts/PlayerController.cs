using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
    public Animator animator;
    private void Awake()
    {
        Debug.Log("Player controller is awake");
    }

    //private void OnCollision2D(Collision2D collision)
    //{
    //   Debug.Log("Collision: " + collision.gameObject.name);
    //}
    private void Update()
    {
        float jump = Input.GetAxisRaw("Vertical");
        float speed = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(speed));

        Vector3 scale = transform.localScale;
        if (speed < 0)
        {

            scale.x = -1f * Mathf.Abs(scale.x);

        }
        else if (speed > 0)
        {

            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
        if (jump > 0)
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



    }



   




}
