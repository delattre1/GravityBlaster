using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{   
    private Animator anim;
    private BoxCollider2D collider;
    private bool hasOpened = false;
    private bool isSealed = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Opens and closes door
    void OnTriggerEnter2D(Collider2D c)
    {
        if(!isSealed){
            // Opens door
            if (c.gameObject.tag == "Player" && !hasOpened){
                anim.SetTrigger("open");
                collider.enabled = false;
            }

            // Closes door
            if (c.gameObject.tag == "Player" && hasOpened){
                anim.SetTrigger("close");
                collider.enabled = true;
                isSealed = true;
            }
        }
    }
    
    public void Close()
    {
        Debug.Log("FoiParent");
        anim.SetTrigger("close");
        collider.enabled = true;
        isSealed = true;
    }
}
