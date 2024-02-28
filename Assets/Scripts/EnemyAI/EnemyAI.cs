using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    private enum EnemyState
    {
        GoingToWallSlot,
        AtWallSlot,
        ChasingPlayer,
        GoingToDreamCore,
        AtDreamCore,
        NoDreamCoreState
    }
    private EnemyState state = EnemyState.GoingToWallSlot;

    [SerializeField] SO_Enemy enemyType;

    [Header("Required References")]
    [SerializeField] private NavMeshAgent aiAgent;
    [SerializeField] private SO_TargetManager soTargetManager;

    [Header("Testing Parameters")]
    [SerializeField] private WallSlot wallTargetSlot;
    [SerializeField] private DCore dCoreTarget;
    [SerializeField] private Transform target;


    // private variables
    private Vector3 lastTargetPos;

    public static EnemyAI Create(Transform _prefab, Vector3 _worldPosition)
    {
        Transform spawningTransform = Instantiate(_prefab, _worldPosition, Quaternion.identity);
        
        EnemyAI enemyAiRef = spawningTransform.GetComponent<EnemyAI>();
        enemyAiRef.SetInitialTarget();

        return enemyAiRef;
    }

    private void SetInitialTarget()
    {
        //Debug.Log("get a wall slot target");
        wallTargetSlot = soTargetManager.GetClosestWallSlot(transform.position);
        target = wallTargetSlot.transform;
    }
    private void Update()
    {

        //if (target == null) return;

        if(target != null)
        {
            if (lastTargetPos != target.position)
            {
                aiAgent.SetDestination(target.position - (target.position - transform.position).normalized * 1f);
            }
            lastTargetPos = target.position;
        }


        if (IsReached())
        {           
            if(state == EnemyState.GoingToWallSlot)
            {
                //Debug.Log($"attack! {transform.name}");
                state = EnemyState.AtWallSlot;
                //transform.LookAt(wallTargetSlot.wallObject.transform);
                if(wallTargetSlot.GetHealth < 0)
                {
                    GetDreamcoreTarget();
                }
                Invoke(nameof(PerformAttackToWall), 0.5f);
            }
            else if(state == EnemyState.GoingToDreamCore)
            {
                //Debug.Log($"attack to dream core! ({dCoreTarget.name})");
                state = EnemyState.AtDreamCore;
                Invoke(nameof(PerformAttackToDreamCore), 0.5f);
            }
            else if(state == EnemyState.NoDreamCoreState)
            {
                state = EnemyState.NoDreamCoreState;
                Invoke(nameof(GetRandomTarget), 0.5f);
            }

        }

        
    }

    private bool IsReached()
    {
        if (!aiAgent.pathPending && aiAgent.remainingDistance <= aiAgent.stoppingDistance && (!aiAgent.hasPath || aiAgent.velocity.sqrMagnitude == 0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void PerformAttackToWall()
    {
        //Debug.Log($"performing attack {transform.name}");
        if (wallTargetSlot.GetDamage(enemyType.Damage))
        {
            // wall broken
            wallTargetSlot.isFilled = false;
            GetDreamcoreTarget();
        }
        else
        {
            Invoke(nameof(PerformAttackToWall), 1.0f);
        }
    }

    private void GetDreamcoreTarget()
    {
        dCoreTarget = soTargetManager.GetClosestDCore(transform.position);
        if(dCoreTarget == null)
        {
            Debug.Log("there is no more dream cores!");
            state = EnemyState.NoDreamCoreState;
            target = null;
            wallTargetSlot = null;
            GetRandomTarget();
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
            GetDreamcoreTarget();
            return;
        }
        if(dCoreTarget.GetDamage(enemyType.Damage))
        {
            // core destroyed
            Debug.Log($"core destroyed! getting another one!");
            GetDreamcoreTarget();
            // get another core
        }
        else
        {
            Invoke(nameof(PerformAttackToDreamCore), 1.0f);
        }
    }

    private void GetRandomTarget()
    {
        target = soTargetManager.GetRandomPatrollingTransform();
        
        state = EnemyState.NoDreamCoreState;
    }
}
