using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Players;
using Unity.Netcode;
using Weapons;
using static ComboAttackManager;
using Sequence = DG.Tweening.Sequence;

public class Enemy : NetworkBehaviour
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

    protected bool isVoid = false;

    [SerializeField]
    GameObject voidHolePrefab;

    [SerializeField] AudioSource voidAudio;


    private void Awake()
    {
        //For Changing Color when hit
        material = Instantiate(_renderer.material);
        _renderer.material = material;
        originalColor = material.color;
        
        enemyAI = GetComponent<EnemyAI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        voidAudio = GetComponent<AudioSource>();

        
    }
    private void FixedUpdate()
    {
        if (isTakingDOTDamage)
        {
            dotTimer--;
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
        FlashRedClientRPC();
    }

    //Called by weapons/projectiles
    public virtual void OnHit(int dmg, string playerTag)
    {
        
        
        if (IsServer)
            onHitServerRpc(dmg, playerTag);
    }

    
    
    [ServerRpc]
    void onHitServerRpc(int dmg, string playerTag)
    {
        TestingManager.enemy = this;
        health -= dmg;
        
        if (playerTag.CompareTo("DarkPlayer") == 0)
            enemyAI.setDarkPlayerTarget();
        else if (playerTag.CompareTo("LightPlayer") == 0)
            enemyAI.setLightPlayerTarget();

        if (health <= 0)
            OnDeath();
        

        FlashRedClientRPC();
    }

    [ClientRpc]
    void FlashRedClientRPC()
    {
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
        WaveManager.EnemyDied();
        // Put some animation player also
        if (isVoid)
        {
            GameObject vo = Instantiate(voidHolePrefab, transform.position, Quaternion.identity);
            vo.transform.localScale = vo.transform.localScale;
            
            NetworkObject net_voidHole = vo.GetComponent<NetworkObject>();
            net_voidHole.Spawn();

            WaveManager.VoidEnemyDied(vo);
            voidAudio.Play();

        }
        Destroy(this.gameObject);
    }

    public void SetIsTakingDOTDamage(bool status)
    {
        isTakingDOTDamage = status;
        dotTimer = 25;
    }

    public void ThisEnemyIsVoid()
    {
        isVoid = true;
        health = 200;
        // set color stuff
        transform.localScale = new(2, 2, 2); // temp debug thing to differentiate what a void enemy is
    }
}
