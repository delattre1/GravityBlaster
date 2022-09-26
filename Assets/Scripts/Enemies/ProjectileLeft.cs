using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLeft : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private Animator anim;
    [SerializeField] float vel = -7;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Handles movement
    void FixedUpdate() 
    {
        rb2d.velocity = new Vector2(vel, rb2d.velocity.y);
    }

    // Handles collision
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Scenario"){
            vel = 0;
            anim.SetTrigger("explode");
            if(col.gameObject.tag == "Player"){
                col.gameObject.GetComponent<PlayerController>().TakeDamage(10);
            }
        }
    }

    // Handles distruction
    void Kill()
    {
        Destroy(gameObject);
    }
}
