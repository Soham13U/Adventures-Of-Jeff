using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;

     void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            if(enemy != null)
            {
                enemy.isKinematic = false;
                Debug.Log("1");
                Vector2 difference = enemy.transform.position - transform.position;
                  Debug.Log("2");
                difference = difference.normalized * thrust;
                Debug.Log("3");
                enemy.AddForce(difference, ForceMode2D.Impulse);
                  Debug.Log("4");
                StartCoroutine(KnockCo(enemy));
                 Debug.Log("5");
            }
        }
    } 
    private IEnumerator KnockCo(Rigidbody2D enemy)
    {
        if( enemy != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;
            enemy.isKinematic = true;
        }
    }
}
