using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   public float speed = 4f;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {   
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal"); //we use GetAxisRaw to directly change the value, (no acceleration). If we used GetAxis then the character would accelerate while moving
        change.y = Input.GetAxisRaw("Vertical");
        UpdateAnimation();

    }
    void UpdateAnimation()
    { MoveCharacter();
         if(change != Vector3.zero)
        {
           
            animator.SetFloat("moveX",change.x);
            animator.SetFloat("moveY",change.y);
            animator.SetBool("moving",true);

        }
        else
        {
            animator.SetBool("moving",false);
        }
    }

    void MoveCharacter()
    {   
     
       // myRigidBody.MovePosition(transform.position + change*Speed() *Time.deltaTime);
       myRigidBody.velocity = new Vector2(change.x*Speed(),change.y*Speed());
       
    }

    float Speed()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10f;
        }
        else{
            speed = 4f;
        }
        return speed;
    }
}
