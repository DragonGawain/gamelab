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
    
    //Visuals for damage
    [SerializeField] private Renderer _renderer;
    private Material material;
    private Color originalColor;
    
    private EnemyAI enemyAI;

    public enum ComboAttackType
    {
        BulletBarrage,
        SuperBullet,
        DOTCloud,
        SuperHammer,
        // Add other combo types as needed
    }
    private void Awake()
    {
        //For Changing Color when hit 
        material = Instantiate(_renderer.material);
        _renderer.material = material;
        originalColor = material.color;
        
        enemyAI = GetComponent<EnemyAI>();
        
    }

    public virtual void OnHitByCombo(ComboAttackType attackType, int damage)
    {
        // Base implementation does nothing by default
        FlashRed();

    }
    public virtual void dealDamage(Weapon weapon)
    {

        // Play hit animation (e.g. enemy gets stunned or smth)
        // Enemy flashes red then turns back to normal
        FlashRed();
    }

        //Called by weapons/projectiles
        public virtual void OnHit(int dmg, string playerTag, Weapon weapon)
        {

            health -= dmg;
            if (playerTag.CompareTo("DarkPlayer") == 0)
            {
                enemyAI.setDarkPlayerTarget();
            }
            else if (playerTag.CompareTo("LightPlayer") == 0)
            {
                enemyAI.setLightPlayerTarget();
            }

            if (health <= 0)
            {
                OnDeath();
            }
            
            FlashRed();

    }

    protected void FlashRed()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(material.DOColor(Color.red, 0.2f));
        sequence.Append(material.DOColor(originalColor, 0.2f));
        sequence.Play();
    }
    

    protected virtual  void OnDeath()
    {
        // Put some animation player also
        Destroy(this.gameObject);
    }
    
}
