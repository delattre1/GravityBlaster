using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// REF https://www.youtube.com/watch?v=dHkbDn-KQ9E
public class DarkNinjaController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator anim;
    private Vector3 baseScale;
    [SerializeField] float vel = 5;
    [SerializeField] LayerMask playerLayers;
    [SerializeField] Transform attackPosition;
    private float last_vel;
    private float attackRange = 0.5f;

    void Start()
    {
        baseScale = transform.localScale;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
    }
    
    // Detects player
    public void CollisionDetected(ChildScript childScript)
    {
        StartCoroutine(Attack());
    } 

    // Attacks
    IEnumerator Attack()
    {
        // Stop for attack
        last_vel = vel;
        vel = 0;
        anim.SetTrigger("attack");

        // Attack check
        yield return new WaitForSeconds(0.26f);
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, playerLayers);
        foreach(Collider2D hit in hitPlayer){
            hit.gameObject.GetComponent<PlayerController>().TakeDamage(20);
        }

        // Cooldown
        yield return new WaitForSeconds(1.5f);
        anim.SetTrigger("run");
        vel = last_vel;
        changeFacingDirection();
    }

    // Take damage
    public void TakeDamage()
    {
        StartCoroutine(Damage());
    }

    // Kill
    public void Murder()
    {
        vel = 0;
        anim.SetTrigger("die");
    }

    // Dies
    void Kill()
    {
        Destroy(gameObject);
    }

    // Damage Reaction
    IEnumerator Damage()
    {
        anim.SetTrigger("hurt");
        last_vel = vel;
        vel = 0;
        yield return new WaitForSeconds(0.16f);
        vel = last_vel;
    }

}
