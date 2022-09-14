using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator controlAnimation;   
    Rigidbody2D rb2D;
    Vector3 direction, currentPosition, baseScale;

    float yAxis, xAxis;
    bool isMoving;
    float vel = 10;

    void Start() {
        baseScale = transform.localScale;
        controlAnimation = GetComponent<Animator>();
    }

    void changeFacingDirection() {
        Vector3 newScale = baseScale;
        if (xAxis < 0) { newScale.x = -baseScale.x;}
        transform.localScale = newScale;
    }

    void Update() {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
        direction = new Vector3(xAxis, yAxis, 0);

        //Update animation
        isMoving = direction.magnitude != 0;
        controlAnimation.SetBool("isMoving", isMoving);
    }

    void FixedUpdate() { //padrao execucao a cada 0.2 segundos
        rb2D = GetComponent<Rigidbody2D>();
        currentPosition = rb2D.position;

        rb2D.MovePosition(
            currentPosition + direction * vel * Time.deltaTime
        );

        if (isMoving) {
            changeFacingDirection();
        }


        //aimRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(aimRay.origin, aimRay.direction*100, Color.red);

        //if (Physics.Raycast(aimRay, out impact, 100, GroundMask)) {
        //    positionPlayerAim = impact.point - transform.position;
        //    positionPlayerAim.y = transform.position.y;

        //    Quaternion newRotation = Quaternion.LookRotation(positionPlayerAim);
        //    rb2D.MoveRotation(newRotation);
        //}
    }
}
