using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Players;
using Weapons;
using Sequence = DG.Tweening.Sequence;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 50;
    [SerializeField] private int damage;
    [SerializeField] private Renderer _renderer;
    private Material material;

    private void Awake()
    {
        material = Instantiate(_renderer.material);
        _renderer.material = material;
    }

    public void OnHit(int dmg)
    {
        Debug.Log("Before hit: " + health);
        health -= dmg;
        Debug.Log("After hit: " + health);
        if (health <= 0)
        {
            OnDeath();
            return;
        }
        
        // Play hit animation (e.g. enemy gets stunned or smth)
        // Enemy flashes red then turns back to normal

        Sequence sequence = DOTween.Sequence();
        sequence.Append(material.DOColor(Color.red, 0.2f));
        sequence.Append(material.DOColor(Color.white, 0.2f));
        sequence.Play();
    }

    private void HitByBlaster(Blaster blaster)
    {
        // deals blaster damage to health
        health -= blaster.GetDamage();
        
        // any other blaster effects can go here
    
    }

    private void OnDeath()
    {
        // Put some animation player also
        Destroy(this.gameObject);
    }
    
}
