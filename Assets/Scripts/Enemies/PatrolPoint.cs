using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    [SerializeField] Transform boss;
    [SerializeField] GameObject point;
    [SerializeField] bool Activate;
    [SerializeField] bool Right;

    // Update is called once per frame
    void Update()
    {
        if (Activate){
            if(!Right){
                if(boss.transform.position.x <= transform.position.x){
                    boss.GetComponent<BossController>().PositionDetected(this);
                    Activate = false;
                    point.GetComponent<PatrolPoint>().Activation();
                }
            }else {
                if(boss.transform.position.x >= transform.position.x){
                    boss.GetComponent<BossController>().PositionDetected(this);
                    Activate = false;
                    point.GetComponent<PatrolPoint>().Activation();
                }
            }
        }
    }

    // Method allows for activation
    public void Activation(){
        Activate = true;
    }
}
