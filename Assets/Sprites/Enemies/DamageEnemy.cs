using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{   
    public bool Ninja = false;
    public bool Worm = false;
    public bool Boss = false;
    public int health = 50;

    public void LoseHealth(int damage)
    {
        health -= damage;
        if(health > 0){
            if(Ninja){
                GetComponent<DarkNinjaController>().TakeDamage();
            }
            else if(Worm){
                GetComponent<FireWormController>().TakeDamage();
            }
            else if(Boss){
                GetComponent<BossController>().TakeDamage();
            }
        }else{
            
            if(Ninja){
                GetComponent<DarkNinjaController>().Murder();
            }
            else if(Worm){
                GetComponent<FireWormController>().Murder();
            }
            else if(Boss){
                GetComponent<BossController>().Murder();
            }
        }
    }
}
