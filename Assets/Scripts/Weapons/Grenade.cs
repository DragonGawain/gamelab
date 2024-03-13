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
    private Renderer rend;
    private Color originalColor;
    public static int damage;
    bool hitGround = false;
    int timer = 0;
    bool isExploding = false;

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
            Debug.Log((float)timer / 15);
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 10, (float)timer / 15);
            timer++;
            if (timer >= 15)
                Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // print(other.tag);
        if (other.CompareTag("Floor") && !hitGround)
        {
            hitGround = true;
            rb.isKinematic = true;
            StartCoroutine(Explode());
        }
    }

    // private void OnCollisionEnter(Collision other) { }

    IEnumerator Explode()
    {
        float kaboomTime = Time.time + 3;
        float boomRate = 1;
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

            boomRate *= 0.7f;
            yield return new WaitForSeconds(boomRate);
        }
        rend.material.color = changeColor;
        isExploding = true;

        // transform.DOScale(Vector3.one * 10, 0.3f).OnComplete(() => Destroy(this.gameObject));
    }
}
