using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkNinjaController : MonoBehaviour
{
    Rigidbody2D rb2d;
    float vel = 5;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        rb2d.velocity = new Vector2(vel, rb2d.velocity.y);
    }
}
