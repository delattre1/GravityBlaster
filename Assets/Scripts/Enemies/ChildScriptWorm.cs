using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildScriptWorm : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"){
            transform.parent.GetComponent<FireWormController>().CollisionDetected(this);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"){
            transform.parent.GetComponent<FireWormController>().CollisionAvoided(this);
        }
    }
 }