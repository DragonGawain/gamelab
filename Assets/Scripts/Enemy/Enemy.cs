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
    [SerializeField]
    public int health = 100;

    [SerializeField]
    private int damage;

    //Visuals for damage
    [SerializeField]
    private Renderer _renderer;
    private Material material;
    private Color originalColor;

    private EnemyAI enemyAI;

    protected int dotTimer = 0;
    protected bool isTakingDOTDamage;

    [SerializeField, Range(1, 250)]
    protected int dotTick = 25; // number of FUs to take a tick of DOT damage

    private void Awake()
    {
        //For Changing Color when hit
        material = Instantiate(_renderer.material);
        _renderer.material = material;
        originalColor = material.color;

        enemyAI = GetComponent<EnemyAI>();
    }

    private void FixedUpdate()
    {
        if (isTakingDOTDamage)
        {
            if (dotTimer <= 0)
            {
                dotTimer = dotTick;
                OnHit(DOTCloud.GetDotDamage(), "DarkPlayer");
            }
        }
    }

    public virtual void OnHitByCombo(ComboAttackType attackType, int damage)
    {
        // Base implementation does nothing by default
        FlashRed();
    }

    //Called by weapons/projectiles
    public virtual void OnHit(int dmg, string playerTag)
    {
        health -= dmg;
        if (playerTag.CompareTo("DarkPlayer") == 0)
            enemyAI.setDarkPlayerTarget();
        else if (playerTag.CompareTo("LightPlayer") == 0)
            enemyAI.setLightPlayerTarget();

        if (health <= 0)
            OnDeath();

        FlashRed();
    }

    public virtual void dealDamage(Weapon weapon)
    {
        // Play hit animation (e.g. enemy gets stunned or smth)
        // Enemy flashes red then turns back to normal
        FlashRed();
    }

    protected void FlashRed()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(material.DOColor(Color.red, 0.2f));
        sequence.Append(material.DOColor(originalColor, 0.2f));
        sequence.Play();
    }

    protected virtual void OnDeath()
    {
        // Put some animation player also
        Destroy(this.gameObject);
    }

    public void SetIsTakingDOTDamage(bool status)
    {
        isTakingDOTDamage = status;
    }
}
