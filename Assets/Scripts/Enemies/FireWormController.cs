using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWormController : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private Animator anim;
    private Vector3 baseScale;
    [SerializeField] float vel = 5;
    [SerializeField] Transform attackPosition;
    [SerializeField] GameObject firePrefab;
    private bool attack = false;
    private bool last_attack = false;
    private float last_vel;

    void Start()
    {
        baseScale = transform.localScale;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Handles attack
    void Update()
    {
        if(attack && !last_attack){
            last_vel = vel;
            Debug.Log(vel);
            vel = 0;
            anim.SetBool("attack", true);
            last_attack = true;
        } else if (!attack && last_attack){
            vel = last_vel;
            anim.SetBool("attack", false);
            last_attack = false;
        }
    }

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
        Vector3 spawnPoint = new Vector3(transform.position.x + 0.269f, transform.position.y + 0.079f, 0f);
        Instantiate(firePrefab, new Vector3(0.269f, 0.079f, 0f), Quaternion.identity);
    }

}
