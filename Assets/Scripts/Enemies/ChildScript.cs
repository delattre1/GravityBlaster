using UnityEngine;

public class ChildScript : MonoBehaviour 
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"){
            transform.parent.GetComponent<DarkNinjaController>().CollisionDetected(this);
        }
    }
 }