using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;

    void Start()
    {
        slider.value = 1;
    }

    public void TakeDamage(int damage)
    {
        slider.value -= damage;
    }
}
