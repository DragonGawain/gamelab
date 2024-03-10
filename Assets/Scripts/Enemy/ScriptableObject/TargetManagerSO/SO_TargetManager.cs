using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

[CreateAssetMenu(fileName = "New_SO_TargetManager", menuName = "ScriptableObjects/TargetManager")]
public class SO_TargetManager : ScriptableObject
{
    // I used Scriptable Objects again for management purposes. The reason is that
    // instead of referencing between all the objects (it becomes very hard to manage after some time),
    // all objects reference to this one and there is no need to put this manager into the scene.
    // however, the variables inside of the scriptable objects are not removing themselves, if you do not do it on your own,
    // they will be kept as stored. So that don't forget the clear variables (I wrote ClearTargetManager() method for that and EnemySpawner uses that when the scene is closed).

    // It will store all the destructible wall references in wallSlotlist. They will be given to enemies as target.
    private List<WallSlot> wallSlotList = new List<WallSlot>();

    // It will store all the dream core references. They can be given to enemies as target when they destroyed a wall or when their assigned wall is already destroyed before.
    private List<DCore> dCoreList = new List<DCore>();

    // I randomly created some patrolling points inside of the example house. When enemies destroyed all the dream cores, they just selects one of them and go over there.
    // When they reached, they request another one and it continues like that...
    private List<Transform> patrollingTransformList = new List<Transform>();

    // this reference is used for re-baking the navmesh. When enemies destroy a wall, the way should be opened. so we need to re-bake the navmesh.
    public NavMeshSurface navmeshSurface;

    // Patrolling points will use this method to add themselves into the patrollingTransformList.
    public void AddPatrollingPosition(Transform _patrollingTransform)
    {
        patrollingTransformList.Add(_patrollingTransform);
    }

    // Enemies use this to get a random target. It returns the transform of the random point.
    public Transform GetRandomPatrollingTransform()
    {
        int index = Random.Range(0, patrollingTransformList.Count);
        return patrollingTransformList[index];
    }

    // WallSlot objects will use this method to add themselves to the wallSlotList.
    public void AddWallSlot(WallSlot _wallSlot)
    {
        wallSlotList.Add(_wallSlot);
    }

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

    // Enemies call this method to get a closest and suitable WallSlot. If there is no suitable WallSlot, it returns null.
    public WallSlot GetClosestWallSlot(Vector3 pos)
    {
        int closestIndex = -1;
        float closestDistance = Mathf.Infinity;
        for(int i = 0; i < wallSlotList.Count; i++)
        {
            float currentDistane = Vector3.Distance(pos, wallSlotList[i].transform.position);
            if (currentDistane < closestDistance && wallSlotList[i].isFilled == false)
            {
                // this index for wall slot list is closer to the enemy, we set it new closestDistance and store the index.
                closestDistance = currentDistane;
                closestIndex = i;
            }
        }

        // we checked all the wall slots and found the closest suitable one (suitable means it is not filled already)

        if (closestIndex == -1) return null;
        else
        {
            // set it as filled to avoid giving it to another enemy (when the enemy destroy the wall, it will make it suitable again)
            wallSlotList[closestIndex].isFilled = true;
            return wallSlotList[closestIndex];
        }
    }

    // this method used by EnemySpawner to check if there is an empty wall slot. if not, it does not spawn enemy. if yes,
    // it spawns enemy.
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
    
    // this method used by enemies to get a closest dream core reference. same dream core can be targeted more than one enemy.
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

    // this method is very important. if you add new features to the SO_TargetManager, you should add any variables/list/arrays/references
    // here to clear them. Otherwise, the game will give some erros that you don't understand why they occured.
    // if you face such kind of bugs/errors, come and check here if you forget to clear anything inside of the SO_TargetManager.
    public void ClearTargetManager()
    {
        wallSlotList = new List<WallSlot>();
        dCoreList = new List<DCore>();
        navmeshSurface = null;
        patrollingTransformList = new List<Transform>();
    }

    // this method re-bakes the navmesh. it is used by wallslots, when they are destroyed, they call this to re-bake the navmesh.
    public void BakeNavmesh()
    {
        navmeshSurface.BuildNavMesh();
    }

}
