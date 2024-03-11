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
        Destroy(grenade);
        Destroy(bullet);
    }

    public static void SpawnSuperBullet(GameObject bullet)
    {
        GameObject superBullet = Instantiate(superBulletPrefab, bullet.transform.position, Quaternion.identity);
        Rigidbody superRb = superBullet.GetComponent<Rigidbody>();
        superRb.velocity = bullet.GetComponent<Rigidbody>().velocity;
        Destroy(bullet);
    }

    public static void SpawnDOTCloud(GameObject grenade)
    {
        Instantiate(dotCloudPrefab, grenade.transform.position, Quaternion.identity);
        Destroy(grenade);
    }

    public static void SpawnSuperHammer(GameObject hammer)
    {
        Instantiate(superHammerPrefab, hammer.transform.position, Quaternion.identity);
    }

}
