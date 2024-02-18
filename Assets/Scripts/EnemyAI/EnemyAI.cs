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
        GoingToBedroomDoorSlot,
        AtBedroomDoorSlot,
        ChasingPlayer
    }
    private EnemyState state = EnemyState.GoingToWallSlot;

    [SerializeField] SO_Enemy enemyType;

    [Header("Required References")]
    [SerializeField] private NavMeshAgent aiAgent;

    [Header("Testing Parameters")]
    [SerializeField] private WallSlot targetSlot;
    private BedroomDoorSlot bedroomDoorSlot;
    private BedroomDoorSlot.BDSlot finalBDSlot;

    public WallSlot TargetTransform
    {
        set { targetSlot = value; }
    }

    // private variables
    private Vector3 lastTargetPos;

    public static EnemyAI Create(Transform _prefab, Vector3 _worldPosition, WallSlot _targetSlot, BedroomDoorSlot _bedroomDoorSlot)
    {
        Transform spawningTransform = Instantiate(_prefab, _worldPosition, Quaternion.identity);

        EnemyAI enemyAiRef = spawningTransform.GetComponent<EnemyAI>();
        enemyAiRef.TargetTransform = _targetSlot;
        enemyAiRef.bedroomDoorSlot = _bedroomDoorSlot;

        return enemyAiRef;
    }

    private void Update()
    {
        if (targetSlot == null) return;

        if(lastTargetPos != targetSlot.transform.position)
        {
            aiAgent.SetDestination(targetSlot.transform.position);
        }
        lastTargetPos = targetSlot.transform.position;

        if (IsReached())
        {           
            if(state == EnemyState.GoingToWallSlot)
            {
                Debug.Log($"attack! {transform.name}");
                state = EnemyState.AtWallSlot;
                transform.LookAt(targetSlot.wallObject.transform);
                Invoke(nameof(PerformAttack), 0.5f);
            }

            if(state == EnemyState.GoingToBedroomDoorSlot)
            {
                Debug.Log($"attack to bedroom door! {transform.name}");
                state = EnemyState.AtBedroomDoorSlot;
                transform.LookAt(bedroomDoorSlot.DoorVisual.transform);
                Invoke(nameof(PerformBedroomDoorAttack), 0.5f);
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

    private void PerformAttack()
    {
        Debug.Log($"performing attack {transform.name}");
        if (targetSlot.GetDamage(enemyType.Damage))
        {
            // wall broken
            Destroy(targetSlot.wallObject);
            finalBDSlot = bedroomDoorSlot.GetOneBedroomDoorSlot();
            aiAgent.SetDestination(finalBDSlot.bdTransform.position);
            state = EnemyState.GoingToBedroomDoorSlot;
        }
        else
        {
            Invoke(nameof(PerformAttack), 1.0f);
        }
    }

    private void PerformBedroomDoorAttack()
    {
        Debug.Log($"performing bedroom attack {transform.name}");
        if (bedroomDoorSlot.GetDamage(enemyType.Damage))
        {
            // door broken
            bedroomDoorSlot.DoorVisual.SetActive(false);
            Debug.Log("GAME OVER");
        }
        else
        {
            Invoke(nameof(PerformBedroomDoorAttack), 1.0f);
        }
    }
}
