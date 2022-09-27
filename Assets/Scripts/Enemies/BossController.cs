using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator anim;
    private Vector3 baseScale;
    private CapsuleCollider2D collider;
    private float side = -1;
    private bool cantAttack = false;
    [SerializeField] Transform playerPos;
    [SerializeField] float vel = 0;
    [SerializeField] float JumpForce = 5;
    [SerializeField] Slider slider;
    public Transform spawnPoint;
    public GameObject thuder;
    public GameObject hadouken_right;
    public GameObject hadouken_left;
    public GameObject right;
    public GameObject left;

    void Start()
    {
        baseScale = transform.localScale;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();
        StartCoroutine(Cooldown());
        slider.value = 500;
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
    }
    
    // Instantiates energy ball
    void ThrowEnergyBall()
    {
        if(side < 0){
            Instantiate(hadouken_left, spawnPoint.position, Quaternion.identity);
        }else{
            Instantiate(hadouken_right, spawnPoint.position, Quaternion.identity);
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
        if(!cantAttack){
            int attack = Random.Range(0,4);
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
    }

    // Cooldown and calls TriggerAttack
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(Random.Range(2,4));
        TriggerAttack();
    }

    // Take damage
    public void TakeDamage(int damage)
    {
        slider.value -= damage;
    }

    // Kill
    public void Murder()
    {
        cantAttack = true;
        collider.enabled = false;
        rb2d.gravityScale = 0;
        anim.SetBool("run", false);
        anim.SetBool("jump", false);
        slider.value = 0;
        vel = 0;
        rb2d.velocity = new Vector2(vel, rb2d.velocity.y);
        anim.SetTrigger("die");
    }

    // Dies
    void Kill()
    {
        left.GetComponent<PatrolPoint>().enabled = false;
        right.GetComponent<PatrolPoint>().enabled = false;
        Destroy(gameObject);
    }
}
