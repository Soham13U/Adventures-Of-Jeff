using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
   public GameObject dialogBox;
   public Text dialogText;
   public string dialog;
   public bool inRange;
   public SignalSender contextOn;
   public SignalSender contextOff;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && inRange)
        {
            if(dialogBox.activeInHierarchy)
            {
                 dialogBox.SetActive(false);
            }
            else{
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
               
                inRange = true;
                 contextOn.Raise();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
                contextOff.Raise();
                inRange = false;
                dialogBox.SetActive(false);
        }
    }
}
