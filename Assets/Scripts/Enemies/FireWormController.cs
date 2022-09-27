using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWormController : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private Animator anim;
    private Vector3 baseScale;
    private CapsuleCollider2D collider;
    [SerializeField] float vel = 5;
    [SerializeField] Transform attackPosition;
    [SerializeField] Transform spawnPosition;
    [SerializeField] GameObject fireRightPrefab;
    [SerializeField] GameObject fireLeftPrefab; 
    private bool attack = false;
    private bool last_attack = false;
    private bool right = false;
    private float last_vel;
    private bool cantAttack = false;

    void Start()
    {
        baseScale = transform.localScale;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    // Handles attack
    void Update()
    {               
        if(!cantAttack){
            if(attack && !last_attack){
                last_vel = vel;
                vel = 0;
                anim.SetBool("attack", true);
                last_attack = true;
            } else if (!attack && last_attack){
                vel = last_vel;
                anim.SetBool("attack", false);
                last_attack = false;
            }
        }
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
            col.gameObject.GetComponent<PlayerController>().TakeDamage(10);
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
        right = !right;
    }

    // Detects player
    public void CollisionDetected(ChildScriptWorm childScript)
    {
        attack = true;
    }     
    
    // Detects exit
    public void CollisionAvoided(ChildScriptWorm childScript)
    {
        attack = false;
    }

    // Shoots Fireball
    public void Fire()
    {
        if(!right){
            Instantiate(fireRightPrefab, spawnPosition.position, Quaternion.identity);
        }else{
            Instantiate(fireLeftPrefab, spawnPosition.position, Quaternion.identity);
        }
    }

    // Kill
    public void Murder()
    {
        cantAttack = true;
        vel = 0;
        anim.SetBool("attack", false);
        collider.enabled = false;
        rb2d.gravityScale = 0;
        rb2d.velocity = new Vector2(vel, rb2d.velocity.y);
        anim.SetTrigger("die");
    }

    // Dies
    void Kill()
    {
        Destroy(gameObject);
    }
}