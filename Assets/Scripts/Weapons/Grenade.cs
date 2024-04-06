using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Weapons;

public class Grenade : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private Color changeColor;

    //how long it takes for grenade to explode
    [SerializeField]
    private float boomRate;

    //size of blast
    [SerializeField]
    private float blastRadius;

    private Renderer rend;
    public AudioSource soundEffect;
    private Color originalColor;
    static readonly int dmg = 30;
    bool hitGround = false;
    int timer = 0;
    public bool isExploding = false;
    public bool exploded = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
        soundEffect = GetComponent<AudioSource>();
        originalColor = rend.material.color;
    }

    private Weapon weaponRef; // passed from the weapon its firing from

    public void SetWeaponRef(Weapon weapon)
    {
        weaponRef = weapon;
    }

    public Weapon GetWeaponRef()
    {
        return weaponRef;
    }

    private void FixedUpdate()
    {
        // 0.3 seconds = 15 FU's
        if (isExploding)
        {
            if (!soundEffect.isPlaying)
            {
                // Play sound effect
                soundEffect.PlayScheduled(0.1);
            }
            
            // Debug.Log((float)timer / 15);
            transform.localScale = Vector3.Lerp(
                Vector3.one,
                Vector3.one * blastRadius,
                (float)timer / 15
            );
            timer++;
            if (timer >= 15)
            {
                //Create a ball the size of grenade to find all enemy colliders
                exploded = true;
                
                
                Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius / 2);
                foreach (Collider hit in colliders)
                {
                    if (!hit.CompareTag("BasicEnemy"))
                        continue;

                    hit.GetComponent<Enemy>().OnHit(dmg, "DarkPlayer");

                    Rigidbody rb = hit.GetComponent<Rigidbody>();
                    if (rb != null)
                        rb.AddExplosionForce(100, transform.position, blastRadius);
                }

                rend.enabled = false;
                exploded = false;
                isExploding = false;
                if (!soundEffect.isPlaying)
                {
                    Destroy(this.gameObject);
                }
                
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (exploded && other.CompareTag("BasicEnemy"))
            other.GetComponent<Enemy>().OnHit(dmg, "DarkPlayer");

        if (!hitGround && other.CompareTag("Floor"))
        {
            hitGround = true;
            rb.isKinematic = true;
            StartCoroutine(Explode());
        }
    }

    // private void OnCollisionEnter(Collision other) { }

    IEnumerator Explode()
    {
        float kaboomTime = Time.time + boomRate;
        float colorChangeRate = 1;
        while (Time.time < kaboomTime)
        {
            if (rend.material.color == originalColor)
            {
                rend.material.color = changeColor;
            }
            else
            {
                rend.material.color = originalColor;
            }

            colorChangeRate *= 0.7f;
            yield return new WaitForSeconds(colorChangeRate);
        }
        rend.material.color = changeColor;
        Debug.Log("Exploding");
        isExploding = true;
    }
}
