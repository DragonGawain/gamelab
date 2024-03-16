using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    private enum EnemyState
    {
        ChasingPlayer,
        GoingToDreamCore,
        AtDreamCore,
        NoDreamCoreState
    }
    
    private EnemyState state = EnemyState.GoingToDreamCore;
    
    [SerializeField] SO_Enemy enemyType;

    [Header("Required References")]
    // I am using NavMesh system for enemy movement
    [SerializeField] private NavMeshAgent aiAgent;
    // soTargetManager will be used to get a suitable target for enemy
    [SerializeField] private SO_TargetManager soTargetManager;

    [Header("Testing Parameters")]
    [SerializeField] private DCore dCoreTarget; // dream core target
    [SerializeField] private Transform target; // transform component of the target

    // creating health variable and getter-setter for health
    // (you may prefer public health instead of this, it is possible)
    private int health;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    // similar to the health
    private int damage;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }


    // for holding the position of the last target
    private Vector3 lastTargetPos;

    // When you call this Create method, an enemy will be spawn in the scene
    public static EnemyAI Create(Transform _prefab, Vector3 _worldPosition)
    {
        Transform spawningTransform = Instantiate(_prefab, _worldPosition, Quaternion.identity);
        
        EnemyAI enemyAiRef = spawningTransform.GetComponent<EnemyAI>();
        enemyAiRef.SetInitialTarget(); // setting the first target of the enemy

        // using the enemy scriptable object to set health and damage
        enemyAiRef.Health = enemyAiRef.enemyType.MaxHealth;
        enemyAiRef.Damage = enemyAiRef.enemyType.Damage;

        return enemyAiRef;
    }

    private void SetInitialTarget()
    {
        // we get the closest wall slot from the soTargetManager, we just give our position
        // to get the closest one.
        dCoreTarget = soTargetManager.GetClosestDCore(transform.position);
        target = dCoreTarget.transform; // don't forget to set transform component of the target
        
    }
    private void Update()
    {
        // when there is a target, it enters in this if statement
        if (target != null)
        {
            // if the traget position is changed, set new position
            if (lastTargetPos != target.position)
            {
                // here I do not give the just target position. instead, i give a close point
                // to the target position to avoid that agent walks inside of the target
                aiAgent.SetDestination(target.position - (target.position - transform.position).normalized * 1f);
            }
            lastTargetPos = target.position;
        }


        if (IsReached())
        {   // if enemy is reached its destination when going to wall slot,
            // it means that it is arrived to the wall slot
            if(state == EnemyState.GoingToDreamCore)
            {
                // if enemy were walking to the dream core,
                state = EnemyState.AtDreamCore;
                // first attack to the dream core, the first call will be 0.5f
                Invoke(nameof(PerformAttackToDreamCore), 0.5f);
            }
            else if(state == EnemyState.NoDreamCoreState)
            {
                state = EnemyState.NoDreamCoreState;
            }

        }

        
    }

    private bool IsReached()
    {
        // reaching method created by using NavMesh.
        if (!aiAgent.pathPending && aiAgent.remainingDistance <= aiAgent.stoppingDistance && (!aiAgent.hasPath || aiAgent.velocity.sqrMagnitude == 0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    

    // this method sets a dream core target
    private void GetDreamcoreTarget()
    {
        // closest dream core is dCoreTarget
        dCoreTarget = soTargetManager.GetClosestDCore(transform.position);
        if(dCoreTarget == null)
        {
            // when there is no dCoreTarget,
            state = EnemyState.NoDreamCoreState;
            target = null;
            return;
        }
        target = dCoreTarget.transform;
        state = EnemyState.GoingToDreamCore;
    }

    private void PerformAttackToDreamCore()
    {
        if(dCoreTarget == null)
        {
            // it is already destroyed
            // get a new dream core as target
            GetDreamcoreTarget();
            return;
        }
        if(dCoreTarget.GetDamage(enemyType.Damage))
        {
            // core destroyed (dCoreTarget.GetDamage returns true)
            Debug.Log($"core destroyed! getting another one!");
            GetDreamcoreTarget();
            // get closest enemy target
        }
        else
        {
            // if there is a dream core target, and if it is not dead yet,
            // give damage after 1 second
            Invoke(nameof(PerformAttackToDreamCore), 1.0f);
        }
    }
    

    
}
