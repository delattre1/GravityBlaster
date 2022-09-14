using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// REF https://www.youtube.com/watch?v=dHkbDn-KQ9E
public class DarkNinjaController : MonoBehaviour
{
    Rigidbody2D rb2d;
    [SerializeField] Transform castPos;
    Vector3 baseScale;

    string facingDirection;
    [SerializeField] float baseCastDistance;
    const string _L = "left";
    const string _R = "right";
    float vel = 5;


    void Start()
    {
        facingDirection = _R;
        baseScale = transform.localScale;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        rb2d.velocity = new Vector2(vel, rb2d.velocity.y);

        if (isHittingWall()) {print("HIT WALL!");}
    }

    bool isHittingWall() {
        bool  isHitting = false;
        float castDist = baseCastDistance;

        // Define if should cast the distance to LEFT or RIGHT
        if (facingDirection == _L) {castDist = -baseCastDistance;}
        
        // Determine target destination based on cast distance
        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;

        isHitting = Physics2D.Linecast(castPos.transform.position, 
                                        targetPos, 
                                        1 << LayerMask.NameToLayer("Ground"));
        return isHitting;
    }

}
