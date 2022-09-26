using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{

    private Rigidbody2D rb2d;
    [SerializeField] float vel = 15;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Handles movement
    void FixedUpdate() 
    {
        rb2d.velocity = new Vector2(vel * transform.localScale.x, rb2d.velocity.y);
    }

    // Handles collision
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Scenario"){
            vel = 0;
            if(col.gameObject.tag == "Enemy"){
                col.gameObject.GetComponent<DamageEnemy>().LoseHealth(10);
            }
            Kill();
        }
    }

    // Handles distruction
    void Kill()
    {
        Destroy(gameObject);
    }
}
