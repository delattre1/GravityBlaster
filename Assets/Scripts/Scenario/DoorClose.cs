using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClose : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"){
            Debug.Log("FoiChild");
            transform.parent.GetComponent<Door>().Close();
        }
    }
}
