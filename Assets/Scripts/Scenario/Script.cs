using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script : MonoBehaviour
{
    public BossController bossScript;
    public GameObject bossHealthBar;

    private Animator healthBarAnim;
    private Slider healthBar;
    private bool playOnce = false;

    void Start()
    {
        healthBarAnim = bossHealthBar.GetComponent<Animator>();
        healthBar = bossHealthBar.GetComponent<Slider>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && !playOnce){
            bossScript.enabled = true;
            healthBarAnim.SetTrigger("appear");
            playOnce = true;
        }
    }
}
