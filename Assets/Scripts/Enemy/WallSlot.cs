// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class WallSlot: MonoBehaviour
// {
//     // to avoid assigning more than one enemy to the same wall, I added this
//     // if isFilled is true, wall slot not assigned to an enemy
//     // however, after the wall is destroyed, it can be assigned to multiple enemies
//     // because they will just walk over
//     public bool isFilled;
//
//     // wallObject is the wall visual. Do not destroy WallSlot object,
//     // just destroy the visual. And of course, if you add the repair feature,
//     // you can just set new health and activate the visual again!
//     public GameObject wallObject;
//
//     // health of the wall is 100 as default, I wrote a public getter for the health
//     private int wallHealth = 100;
//     public int GetHealth
//     {
//         get { return wallHealth; }
//     }
//
//     // soTargetManager is important. it has 2 jobs for WallSlots.
//     // 1) it holds the wall slot list. These are given to enemies as target.
//     // 2) When a wall is destroyed, NavMesh needs to be re-baked. soTargetManager
//     // have access to the navmesh data, BakeNavmesh() re-bakes the NavMesh.
//     [SerializeField] private SO_TargetManager soTargetManager;
//
//     private void Awake()
//     {
//         // adding itself to the wall slot list in soTargetManager
//         soTargetManager.AddWallSlot(this);
//     }
//     public bool GetDamage(int amount)
//     {
//         // returns true if the wall is destroyed
//         if(wallHealth - amount <= 0)
//         {
//             // just disable the wall visual (wallObject)
//             wallObject.SetActive(false);
//             // re-bake the navmesh
//             soTargetManager.BakeNavmesh();
//             // return true so that the enemy understand it destroyed the wall
//             return true;
//         }
//         else
//         {
//             wallHealth -= amount;
//             // return false to keep enemy continue to attack to the wall
//             return false;
//         }
//     }
//
// }
