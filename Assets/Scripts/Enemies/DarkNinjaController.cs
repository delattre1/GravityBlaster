using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// REF https://www.youtube.com/watch?v=dHkbDn-KQ9E

public class DarkNinjaController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator anim;
    private Vector3 baseScale;
    private CapsuleCollider2D collider;
    [SerializeField] float vel = 5;
    [SerializeField] LayerMask playerLayers;
    [SerializeField] Transform attackPosition;
    private float max_vel;
    private float attackRange = 0.5f;
    private bool cantAttack = false;

    void Start()
    {
        baseScale = transform.localScale;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();
        max_vel = vel;
    }

    // Handles movement
    void FixedUpdate() 
    {
        rb2d.velocity = new Vector2(vel, rb2d.velocity.y);
    }
    
    // Ignores collision with other enemies
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy"){
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }
        
        if (col.gameObject.tag == "Player"){
            col.gameObject.GetComponent<PlayerController>().TakeDamage(5);
        }
    }

    // Patrols until wall
    void OnTriggerEnter2D(Collider2D c)
    {
        
        if (c.gameObject.tag == "Scenario"){
            changeFacingDirection();
        }
    }

    // Flips enemy
    public void changeFacingDirection() 
    {
        Vector3 newScale = transform.localScale;
        if (newScale.x < 0) { newScale.x = baseScale.x;} else 
        if (newScale.x > 0) { newScale.x =  -baseScale.x;}
        transform.localScale = newScale;
        vel *= -1;
        max_vel *= -1;
    }
    
    // Detects player
    public void CollisionDetected(ChildScript childScript)
    {
        if(!cantAttack){
            StartCoroutine(Attack());
        }
    } 

    // Attacks
    IEnumerator Attack()
    {
        // Stop for attack
        vel = 0;
        anim.SetTrigger("attack");

        // Cooldown
        yield return new WaitForSeconds(1.5f);
        anim.SetTrigger("run");
        vel = max_vel;
        changeFacingDirection();
    }

    // Attack check
    void Hit()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, playerLayers);
        foreach(Collider2D hit in hitPlayer){
            hit.gameObject.GetComponent<PlayerController>().TakeDamage(20);
        }
    }

    // Kill
    public void Murder()
    {
        cantAttack = true;
        vel = 0;
        rb2d.velocity = new Vector2(vel, rb2d.velocity.y);
        collider.enabled = false;
        rb2d.gravityScale = 0;
        anim.SetTrigger("die");
    }

    // Dies
    void Kill()
    {
        Destroy(gameObject);
    }

}
