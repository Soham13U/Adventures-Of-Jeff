using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState{
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class PlayerMovement : MonoBehaviour
{   public float speed = 4f;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;
    public PlayerState currentState;
    public FloatValue currentHealth;
    public VectorValue startingPosition;

    public SignalSender playerHealthSignal;
    // Start is called before the first frame update
    void Start()
    {   
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        currentState = PlayerState.walk;
        animator.SetFloat("moveX",0);
        animator.SetFloat("moveY",-1);
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal"); //we use GetAxisRaw to directly change the value, (no acceleration). If we used GetAxis then the character would accelerate while moving
        change.y = Input.GetAxisRaw("Vertical");
        if(Input.GetButtonDown("Attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk || currentState== PlayerState.idle){

        
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
    public void Knock(float knockTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if(currentHealth.RuntimeValue > 0)
        {
          
          StartCoroutine(KnockCo(knockTime));
        }
        else{
            this.gameObject.SetActive(false);
        }
        
    }
    private IEnumerator KnockCo(float knockTime)
    {
        if( myRigidBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;
           currentState = PlayerState.idle;
           myRigidBody.velocity = Vector2.zero;
            
        }
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
