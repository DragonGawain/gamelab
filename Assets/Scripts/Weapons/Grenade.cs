using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private Color changeColor;

    //how long it takes for grenade to explode
    [SerializeField] private float boomRate;
    //size of blast
    [SerializeField] private float blastRadius;
    
    
    private Renderer rend;
    private Color originalColor;
    public static int dmg;
    bool hitGround = false;
    int timer = 0;
    bool isExploding = false;
    private bool exploded = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    private void FixedUpdate()
    {
        // 0.3 seconds = 15 FU's
        if (isExploding)
        {
            // Debug.Log((float)timer / 15);
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * blastRadius, (float)timer / 15);
            timer++;
            if (timer >= 15)
            {
                exploded = true;
                Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius/2);
                foreach (Collider hit in colliders)
                {
                    if (!hit.CompareTag("Enemy"))
                        continue;
                    // Check if the collider belongs to an enemy
                    Enemy enemy = hit.GetComponent<Enemy>();
                    enemy.OnHit(dmg);
                    Rigidbody rb = hit.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.AddExplosionForce(100, transform.position, blastRadius);
                    }
                }
                Destroy(this.gameObject);
                exploded = false;
            }
                
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (exploded && other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.OnHit(dmg);
        }
        
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
        isExploding = true;
    }
}
