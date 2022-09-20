using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Rigidbody2D rb2d;
    [SerializeField] float vel = 7;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Handles movement
    void FixedUpdate() 
    {
        rb2d.velocity = new Vector2(vel, rb2d.velocity.y);
    }
}
