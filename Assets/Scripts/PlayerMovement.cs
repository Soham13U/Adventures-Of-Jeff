using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState{
    walk,
    attack,
    interact
}

public class PlayerMovement : MonoBehaviour
{   public float speed = 4f;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;
    private PlayerState currentState;
    // Start is called before the first frame update
    void Start()
    {   
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        currentState = PlayerState.walk;
        animator.SetFloat("moveX",0);
        animator.SetFloat("moveY",-1);
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal"); //we use GetAxisRaw to directly change the value, (no acceleration). If we used GetAxis then the character would accelerate while moving
        change.y = Input.GetAxisRaw("Vertical");
        if(Input.GetButtonDown("Attack") && currentState != PlayerState.attack)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk){

        
        UpdateAnimation();
        }

    }

     private IEnumerator AttackCo()
     {
        animator.SetBool("attacking",true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking",false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk;
        
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
            change.Normalize();
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
