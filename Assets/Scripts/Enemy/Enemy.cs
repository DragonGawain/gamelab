using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Players;
using Weapons;
using static ComboAttackManager;
using Sequence = DG.Tweening.Sequence;


public class Enemy : MonoBehaviour
{
    [SerializeField] public int health = 100;
    [SerializeField] private int damage;
    [SerializeField] private Renderer _renderer;
    private Material material;
    private EnemyAI enemyAI;

    private void Awake()
    {
        //For Changing Color when hit 
        material = Instantiate(_renderer.material);
        _renderer.material = material;
        
        enemyAI = GetComponent<EnemyAI>();
        
    }

    public virtual void OnHitByCombo(ComboAttackType attackType, int damage)
    {
        // Base implementation does nothing by default
    }


    public virtual void dealDamage(Weapon weapon)
    {

        // Play hit animation (e.g. enemy gets stunned or smth)
        // Enemy flashes red then turns back to normal
        FlashRed();
    }

        //Called by weapons/projectiles
        public void OnHit(int dmg, string playerTag)
    {
        if (playerTag.CompareTo("DarkPlayer") == 0)
        {
            enemyAI.setDarkPlayerTarget();
        }
        else if (playerTag.CompareTo("LightPlayer") == 0)
        {
            enemyAI.setLightPlayerTarget();
        }
        
    

        
    }

    protected void FlashRed()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(material.DOColor(Color.red, 0.2f));
        sequence.Append(material.DOColor(Color.white, 0.2f));
        sequence.Play();
    }


    //private void HitByBlaster(Blaster blaster)
    //{
    //    // deals blaster damage to health
    //    health -= blaster.GetDamage();
        
    //    // any other blaster effects can go here
    
    //}

    private void OnDeath()
    {
        // Put some animation player also
        Destroy(this.gameObject);
    }
    
}
