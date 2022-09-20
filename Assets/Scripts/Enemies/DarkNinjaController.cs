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
    //private float xAxis;
    private float last_vel;
    private float attackRange = 0.5f;

    void Start()
    {
        baseScale = transform.localScale;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // void Update()
    // {
    //     xAxis = Input.GetAxis("Horizontal");

    //     // Cast a ray straight
    //     Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
    //     RaycastHit2D hitFront = Physics2D.Raycast(transform.position, forward);
    //     Debug.DrawRay(transform.position, forward, Color.green);
    // }

    // Handles movement
    void FixedUpdate() 
    {
        rb2d.velocity = new Vector2(vel, rb2d.velocity.y);
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
            Debug.Log("Levou");  // TIRAR VIDA AQUI <<<<<<--------------------------
        }

        // Cooldown
        yield return new WaitForSeconds(1.5f);
        anim.SetTrigger("run");
        vel = last_vel;
        changeFacingDirection();
    }

}
