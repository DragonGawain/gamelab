using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

[CreateAssetMenu(fileName = "New_SO_TargetManager", menuName = "ScriptableObjects/TargetManager")]
public class SO_TargetManager : ScriptableObject
{
    private List<WallSlot> wallSlotList = new List<WallSlot>();
    private List<DCore> dCoreList = new List<DCore>();
    private List<Transform> patrollingTransformList = new List<Transform>();
    public NavMeshSurface navmeshSurface;

    public void AddPatrollingPosition(Transform _patrollingTransform)
    {
        patrollingTransformList.Add(_patrollingTransform);
    }

    public Transform GetRandomPatrollingTransform()
    {
        int index = Random.Range(0, patrollingTransformList.Count);
        return patrollingTransformList[index];
    }
    public void AddWallSlot(WallSlot _wallSlot)
    {
        wallSlotList.Add(_wallSlot);
    }

    public void AddDCore(DCore _dCore)
    {
        dCoreList.Add(_dCore);
    }

    public void RemoveDCore(DCore _dCore)
    {
        dCoreList.Remove(_dCore);
    }

    public WallSlot GetClosestWallSlot(Vector3 pos)
    {
        int closestIndex = -1;
        float closestDistance = Mathf.Infinity;
        for(int i = 0; i < wallSlotList.Count; i++)
        {
            float currentDistane = Vector3.Distance(pos, wallSlotList[i].transform.position);
            if (currentDistane < closestDistance && wallSlotList[i].isFilled == false)
            {
                // normally in this if statement i was checking if the wallslot is filled or not, but i removed it
                // if the wall is already broken, they just continue
                closestDistance = currentDistane;
                closestIndex = i;
            }
        }

        if (closestIndex == -1) return null;
        else
        {
            wallSlotList[closestIndex].isFilled = true;
            return wallSlotList[closestIndex];
        }
    }

    public bool IsThereEmptyWallSlot()
    {
        foreach(WallSlot wallSlot in wallSlotList)
        {
            if(wallSlot.isFilled == false)
            {
                return true;
            }
        }
        return false;
    }
    


    public DCore GetClosestDCore(Vector3 pos)
    {
        int closestIndex = -1;
        float closestDistance = Mathf.Infinity;
        for (int i = 0; i < dCoreList.Count; i++)
        {
            float currentDistane = Vector3.Distance(pos, dCoreList[i].transform.position);
            if (currentDistane < closestDistance && dCoreList[i].GetHealth > 0)
            {
                closestDistance = currentDistane;
                closestIndex = i;
            }
        }

        if(closestIndex == -1) return null; 
        else return dCoreList[closestIndex];
    }

    public void ClearTargetManager()
    {
        wallSlotList = new List<WallSlot>();
        dCoreList = new List<DCore>();
        navmeshSurface = null;
        patrollingTransformList = new List<Transform>();
    }

    public void BakeNavmesh()
    {
        navmeshSurface.BuildNavMesh();
    }

}
