using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(fileName = "New_SO_TargetManager", menuName = "ScriptableObjects/TargetManager")]
public class SO_TargetManager : ScriptableObject
{
    
    // It will store all the dream core references. They can be given to enemies as target when they destroyed a wall or when their assigned wall is already destroyed before.
    private List<DCore> dCoreList = new List<DCore>();

    // DCore (Dream core) objects will use this method to add themselves into the dCoreList.
    public void AddDCore(DCore _dCore)
    {
        
        dCoreList.Add(_dCore);
    }

    // When a dream core is destroyed, destroyed DCore object will call this to remove itself from the dCoreList.
    public void RemoveDCore(DCore _dCore)
    {
        dCoreList.Remove(_dCore);
    }

    public bool IsThereDCore()
    {
        return dCoreList.Count > 0;
    }
    // this method used by enemies to get a closest dream core reference. same dream core can be targeted more than one enemy.
    // ReSharper disable Unity.PerformanceAnalysis
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
        dCoreList = new List<DCore>();
    }
}
