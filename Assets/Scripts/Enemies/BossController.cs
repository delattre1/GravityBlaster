using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // private Rigidbody2D rb2d;
    // private Animator anim;
    // private Vector3 baseScale;
    // [SerializeField] float vel = 5;
    // [SerializeField] LayerMask playerLayers;
    // [SerializeField] Transform attackPosition;
    // [SerializeField] Transform playerPosition;
    // private float last_vel;
    // private float attackRange = 0.5f;

    // void Start()
    // {
    //     baseScale = transform.localScale;
    //     rb2d = GetComponent<Rigidbody2D>();
    //     anim = GetComponent<Animator>();
    // }
    
    // // Handles movement
    // void FixedUpdate() 
    // {
    //     rb2d.velocity = new Vector2(vel, rb2d.velocity.y);
    // }

    // // Handles movement
    // void FixedUpdate() 
    // {
    //     rb2d.velocity = new Vector2(vel, rb2d.velocity.y);
    // }
    
    // // Ignores collision with other enemies
    // void OnCollisionEnter2D(Collision2D col)
    // {
    //     if(col.gameObject.tag == "Enemy"){
    //         Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
    //     }
    // }

    // // Patrols until wall
    // void OnTriggerEnter2D(Collider2D c)
    // {
        
    //     if (c.gameObject.tag == "Scenario"){
    //         changeFacingDirection();
    //     }
    // }

    // // Flips enemy
    // public void changeFacingDirection() 
    // {
    //     Vector3 newScale = transform.localScale;
    //     if (newScale.x < 0) { newScale.x = baseScale.x;} else 
    //     if (newScale.x > 0) { newScale.x =  -baseScale.x;}
    //     transform.localScale = newScale;
    //     vel *= -1;
    // }
    
    // // Detects player
    // public void CollisionDetected(ChildScript childScript)
    // {
    //     StartCoroutine(Attack());
    // } 

    // // Attacks
    // IEnumerator Attack()
    // {

    // }

}
