using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float force;
    [SerializeField] private Color changeColor;
    private Renderer rend;
    private Color originalColor;
    public static int damage;

    void Start()
    {
        
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
        rb.AddForce(-transform.right * force, ForceMode.Impulse);
    }


    private void OnCollisionEnter(Collision other)
    {
        rb.isKinematic = true;
        StartCoroutine(Explode());
    }

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
        transform.DOScale(Vector3.one * 10, 0.3f).OnComplete(() => Destroy(this.gameObject));

    }
}
