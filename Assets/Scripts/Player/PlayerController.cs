using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public LayerMask groundLayerMask;
    public float attackRange = 1f;
    Animator controlAnimation;   
    Rigidbody2D rb;
    Vector3 direction, currentPosition, baseScale;
    CapsuleCollider2D capsColl; 
    [SerializeField] Slider slider;
    [SerializeField] GameObject fireRightPrefab;
    [SerializeField] GameObject fireLeftPrefab; 
    [SerializeField] Transform spawnPosition;
    [SerializeField] LayerMask enemyLayers;

    float xAxis;
    bool isMoving;
    float vel = 0;
    bool isGrounded;
    bool right = true;
    bool isAttacking = false;

    void Start() {
        baseScale = transform.localScale;
        controlAnimation = GetComponent<Animator>();
        capsColl = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        slider.value = 100;
    }

    void changeFacingDirection() {
        Vector3 newScale = transform.localScale;
        if (xAxis < 0) { 
            newScale.x = -baseScale.x;
            right = true;
        } else if (xAxis > 0) { 
            newScale.x =  baseScale.x;
            right = false;
        }
        transform.localScale = newScale;
    }

    void changeStandingDirection() {
        Vector3 newScale = transform.localScale;
        newScale.y = -newScale.y;
        transform.localScale = newScale;
    }

    void handleGravity() {
        if (Input.GetKeyDown(KeyCode.I) && !isAttacking) {
            rb.gravityScale *= -1;
            changeStandingDirection();
        }
    }

    bool checkGrounded() {
        //Color rayColor;
        float extraHeight = 0.1f;
        float capsHeight = capsColl.bounds.extents.y * 2;
        Vector3 startRayPosition = capsColl.bounds.max;
        startRayPosition.y += extraHeight;
        RaycastHit2D rayCast = Physics2D.Raycast(
                       startRayPosition,
                        Vector2.down,
                        capsHeight + 2*extraHeight,
                        groundLayerMask
                        );

        return rayCast.collider != null;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(spawnPosition.position, attackRange);
    }

    void Update() {
        isGrounded = checkGrounded();

        if (isGrounded) {
            handleGravity();
        }

        if (Input.GetKeyDown(KeyCode.O) && !isAttacking) {
            StartCoroutine(Range());
        }

        if (Input.GetKeyDown(KeyCode.P) && !isAttacking) {
            StartCoroutine(Melee());
        }
    }

    void FixedUpdate() {
        xAxis = Input.GetAxis("Horizontal");
        direction = new Vector3(xAxis, 0, 0);
        rb.velocity = new Vector2(vel, rb.velocity.y);
        

        //Update animation
        isMoving = direction.magnitude != 0;

        if(!isAttacking){
            if (isMoving) {
                changeFacingDirection();
            }
            if (Input.GetKey(KeyCode.A)){
                vel = -10;
                controlAnimation.SetBool("isMoving", true);
            } else if (Input.GetKey(KeyCode.D)){
                vel = 10;
                controlAnimation.SetBool("isMoving", true);
            } 
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)){
                vel = 0;
                controlAnimation.SetBool("isMoving", false);
            }
        } else {
            controlAnimation.SetBool("isMoving", false);
            vel = 0;
        }
    }

    IEnumerator Range()
    {
        controlAnimation.SetBool("isMoving", false);
        controlAnimation.SetTrigger("range");
        isAttacking = true;
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }

    void Fire()
    {
        if(!right){
            Instantiate(fireRightPrefab, spawnPosition.position, Quaternion.identity);
        }else{
            Instantiate(fireLeftPrefab, spawnPosition.position, Quaternion.identity);
        }
    }

    IEnumerator Melee()
    {
        controlAnimation.SetBool("isMoving", false);
        controlAnimation.SetTrigger("melee");
        isAttacking = true;
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }

    void Attack()
    {
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(spawnPosition.position, attackRange, enemyLayers);
        foreach(Collider2D hit in hitEnemy){
            hit.gameObject.GetComponent<DamageEnemy>().LoseHealth(20);
        }
    }

    public void TakeDamage(int damage)
    {
        slider.value -= damage;
        StartCoroutine(InvecibilityFrames());
    }

    IEnumerator InvecibilityFrames()
    {
        // Activate Invencibility
        Physics2D.IgnoreLayerCollision(8, 3, true);
        controlAnimation.SetBool("isMoving", false);
        controlAnimation.SetTrigger("isHit");
        Color playerColor = GetComponent<SpriteRenderer>().color;
        playerColor.a = 0.25f;
        GetComponent<SpriteRenderer>().color = playerColor;
        yield return new WaitForSeconds(2f);

        
        // Deactivate Invencibility
        Physics2D.IgnoreLayerCollision(8, 3, false);
        playerColor.a = 1f;
        GetComponent<SpriteRenderer>().color = playerColor;
    }
}
