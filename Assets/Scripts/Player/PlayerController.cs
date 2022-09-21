using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask groundLayerMask;
    Animator controlAnimation;   
    Rigidbody2D rb;
    Vector3 direction, currentPosition, baseScale;
    CapsuleCollider2D capsColl; 

    float xAxis;
    bool isMoving;
    float vel = 10;
    bool isGrounded;

    void Start() {
        baseScale = transform.localScale;
        controlAnimation = GetComponent<Animator>();
        capsColl = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void changeFacingDirection() {
        Vector3 newScale = transform.localScale;
        if (xAxis < 0) { newScale.x = -baseScale.x;} else 
        if (xAxis > 0) { newScale.x =  baseScale.x;}
        transform.localScale = newScale;
    }

    void changeStandingDirection() {
        Vector3 newScale = transform.localScale;
        newScale.y = -newScale.y;
        transform.localScale = newScale;
    }

    //float vy = 0;
    void handleMovimentation() {
        xAxis = Input.GetAxis("Horizontal");
        direction = new Vector3(xAxis, 0, 0);

        // Move the character alon x axis
        currentPosition = rb.position;
        //vy = vy + Physics2D.gravity.y * Time.deltaTime;


        rb.MovePosition(
            currentPosition + (direction * vel * Time.deltaTime) //+ (Vector3.down * vy))
        );

        if (isMoving) {
            changeFacingDirection();
        }

        //Update animation
        isMoving = direction.magnitude != 0;
        controlAnimation.SetBool("isMoving", isMoving);
    }

    void handleGravity() {
        if (Input.GetKeyDown(KeyCode.O)) {
            rb.gravityScale *= -1;
            //Physics2D.gravity *= -1;
            changeStandingDirection();
        }
    }

    void handleJump() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Should Jump");
            float jmpVel = 100;
            rb.velocity = Vector2.up * jmpVel;
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
        //Debug
        //if (rayCast != null) {rayColor = Color.green;}
        //else {rayColor = Color.red;}

        //Debug.DrawRay(
        //            startRayPosition,
        //            Vector2.down * (capsHeight + 2*extraHeight),
        //            rayColor
        //            );

        //Debug.Log(rayCast.collider);
        return rayCast.collider != null;
    }

    void Update() {
        isGrounded = checkGrounded();

        if (isGrounded) {
            handleGravity();
            handleJump();
        }
    }

    void FixedUpdate() { //padrao execucao a cada 0.2 segundos
        handleMovimentation();
    }
}
