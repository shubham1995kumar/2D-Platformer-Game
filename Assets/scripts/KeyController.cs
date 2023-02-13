using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public Animator animator;
    public void OnAnimatorIK()
    {
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            PlayerController.PickUpKey();
            Destroy(gameObject);
        }
    }
}
