using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport.Error;
using UnityEngine;
using Weapons;


/* 
Combo attacks:
Blaster + Grenade: bullet barrage:
    Spawn many bullets in random directions from grenade point. Also destroy original bullet and original grenade.
Blaster + Hammer: super blaster:
    Make the bullet BIG and do MOAR damage
Flamethrower + Grenade: cloud bomb:
    Delete original grenade. Spawn in DOT cloud
Flamethrower + Hammer: super hammer: (doo-doo da-dee da-deee!)
    Hammer gets BEEG. 
*/
public class ComboAttackManager : MonoBehaviour
{
    static GameObject bulletPrefab;
    static GameObject superBulletPrefab;
    static GameObject dotCloudPrefab;
    static GameObject superHammerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        bulletPrefab = Resources.Load<GameObject>("ComboWeapons/Bullet_DUPLICATE");
        superBulletPrefab = Resources.Load<GameObject>("ComboWeapons/SuperBullet");
        dotCloudPrefab = Resources.Load<GameObject>("ComboWeapons/DOTCloud");
        superHammerPrefab = Resources.Load<GameObject>("ComboWeapons/SuperHammer");
    }

    public static void SpawnBulletBarrage(GameObject grenade, GameObject bullet)
    {
        Debug.Log("SPAWN BULLET BARRAGE");
        // It's VERY important to delete the grenade object first. Otherwise, it's theoretically possible that the spawned bullets collide with the same grenade. 
        // This will not only result in more bullets being spawned, but can also potentially crash the game (trying to destroy a gameobject that already destroyed -> nullObjectReference)
        Destroy(grenade);
        GameObject tempBullet;
        Rigidbody tempRb;
        Vector3 radnomDir;
        for (int i = 0; i < 8; i++)
        {
            tempBullet = Instantiate(bulletPrefab, grenade.transform.position, Quaternion.identity);
            tempRb = tempBullet.GetComponent<Rigidbody>();
            radnomDir = new(Random.Range(-1,1), 0, Random.Range(-1,1));
            radnomDir.Normalize();
            tempRb.AddForce(GameManager.GetMousePosition3() * Blaster.GetBulletForce(), ForceMode.Impulse);
        }
        Destroy(bullet);
    }

    public static void SpawnSuperBullet(GameObject bullet)
    {
        Debug.Log("SPAWN SUPER BULLET");
        GameObject superBullet = Instantiate(superBulletPrefab, bullet.transform.position, Quaternion.identity);
        Rigidbody superRb = superBullet.GetComponent<Rigidbody>();
        superRb.velocity = bullet.GetComponent<Rigidbody>().velocity;
        Destroy(bullet);
    }

    public static void SpawnDOTCloud(GameObject grenade)
    {
        Debug.Log("SPAWN DOT CLOUD");
        Instantiate(dotCloudPrefab, grenade.transform.position, Quaternion.identity);
        Destroy(grenade);
    }

    public static void SpawnSuperHammer(GameObject hammer)
    {
        Debug.Log("SPAWN SUPER HAMMER");
        Instantiate(superHammerPrefab, hammer.transform.position, Quaternion.identity);
    }

}
