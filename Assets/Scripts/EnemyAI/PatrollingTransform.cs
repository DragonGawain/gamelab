using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingTransform : MonoBehaviour
{
    [SerializeField] private SO_TargetManager soTargetManager;

    private void Awake()
    {
        soTargetManager.AddPatrollingPosition(this.transform);
    }
}
