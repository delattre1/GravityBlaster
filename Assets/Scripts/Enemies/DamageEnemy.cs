using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// REF https://github.com/BarthaSzabolcs/Tutorial-SpriteFlash/blob/main/Assets/Scripts/FlashEffects/SimpleFlash.cs

public class DamageEnemy : MonoBehaviour
{   
    public bool Ninja = false;
    public bool Worm = false;
    public bool Boss = false;
    public int health;

    [SerializeField] Material flashMaterial;
    [SerializeField] private float duration;

    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    public void LoseHealth(int damage)
    {
        health -= damage;
        if(health > 0){
            StartCoroutine(Damage());
            if(Boss){
                GetComponent<BossController>().TakeDamage(damage);
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

    // Damage Reaction
    IEnumerator Damage()
    {
        // Flash sprite
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(duration);
        spriteRenderer.material = originalMaterial;
    }
}
