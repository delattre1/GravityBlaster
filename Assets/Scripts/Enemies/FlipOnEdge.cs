using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipOnEdge : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Scenario"){
            transform.parent.GetComponent<DarkNinjaController>().changeFacingDirection();
        }
    }
}
