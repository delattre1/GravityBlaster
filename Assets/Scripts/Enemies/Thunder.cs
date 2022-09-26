using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    [SerializeField] LayerMask playerLayers;
    [SerializeField] Transform attackPosition;
    private float attackRange = 0.5f;

    // Attack check
    void Attack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, playerLayers);
        foreach(Collider2D hit in hitPlayer){
            hit.gameObject.GetComponent<PlayerController>().TakeDamage(10);
        }
        Kill();
    }


    // Destroys lightining
    void Kill()
    {
        Destroy(gameObject);
    }
}
