using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator anim;
    private Vector3 baseScale;
    private float side = -1;
    [SerializeField] Transform playerPos;
    [SerializeField] float vel = 0;
    [SerializeField] float JumpForce = 5;
    public GameObject thuder;
    public GameObject hadouken_right;
    public GameObject hadouken_left;

    void Start()
    {
        baseScale = transform.localScale;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCoroutine(Cooldown());
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
    }

    // Flips enemy
    public void changeFacingDirection() 
    {
        anim.SetBool("run", false);
        anim.SetBool("jump", false);
        Vector3 newScale = transform.localScale;
        if (newScale.x < 0) { newScale.x = -baseScale.x;} else 
        if (newScale.x > 0) { newScale.x =  baseScale.x;}
        transform.localScale = newScale;
        side *= -1;
        vel = 0;
        StartCoroutine(Cooldown());
    }
    
    // Detects egde of stage
    public void PositionDetected(PatrolPoint PatrolPoint)
    {
        Vector3 move = new Vector3(0.001f*(-side), 0f, 0f);
        transform.position += move;
        changeFacingDirection();
    } 

    // -------------------------ATTACKS-----------------------

    // Runs fowards
    void Dash()
    {
        anim.SetBool("run", true);
        vel = 10f * side;
    }

    // Jumps across
    void JumpDash()
    {
        anim.SetBool("jump", true);
        vel = 8.2f * side;
        rb2d.AddForce(Vector2.up * JumpForce);
    }

    // Animation for energy ball
    void SummonHadouken()
    {
        anim.SetTrigger("hadouken");
        ThrowEnergyBall();
    }
    
    // Instantiates energy ball
    void ThrowEnergyBall()
    {
        if(side < 0){
            Instantiate(hadouken_left, transform.position, Quaternion.identity);
        }else{
            Instantiate(hadouken_right, transform.position, Quaternion.identity);
        }
        StartCoroutine(Cooldown());
    }

    // Animation for thunder
    void SummonThunder()
    {
        anim.SetTrigger("thunder");
    }

    // Instantiates thunder
    void MakeItRain()
    {
        Instantiate(thuder, new Vector3(playerPos.position.x, -1.5f, 0f), Quaternion.identity);
        StartCoroutine(Cooldown());
    }

    // Handles attack state machine
    void TriggerAttack()
    {
        int attack = Random.Range(0,4);
        Debug.Log(attack);
        if(attack == 0){
            Dash();
        } else if (attack == 1){
            JumpDash();
        } 
        else if (attack == 2){
            SummonHadouken();
        } else {
            SummonThunder();
        }
    }

    // Cooldown and calls TriggerAttack
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(Random.Range(2,4));
        TriggerAttack();
    }
}
