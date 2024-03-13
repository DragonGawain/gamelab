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

    static int bgTimer = 0;
    static int bhTimer = 0;
    static int fgTimer = 0;
    static int fhTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        bulletPrefab = Resources.Load<GameObject>("ComboWeapons/Bullet_DUPLICATE");
        superBulletPrefab = Resources.Load<GameObject>("ComboWeapons/SuperBullet");
        dotCloudPrefab = Resources.Load<GameObject>("ComboWeapons/DOTCloud");
        superHammerPrefab = Resources.Load<GameObject>("ComboWeapons/SuperHammer");
    }

    private void FixedUpdate()
    {
        if (bgTimer >= 0)
            bgTimer--;
        if (bhTimer >= 0)
            bhTimer--;
        if (fgTimer >= 0)
            fgTimer--;
        if (fhTimer >= 0)
            fhTimer--;
    }

    public static void SpawnBulletBarrage(GameObject grenade, GameObject bullet)
    {
        if (bgTimer > 0)
            return;
        bgTimer = 50;

        // It's VERY important to delete the grenade object first. Otherwise, it's theoretically possible that the spawned bullets collide with the same grenade.
        // This will not only result in more bullets being spawned, but can also potentially crash the game (trying to destroy a gameobject that already destroyed -> nullObjectReference)
        Destroy(bullet.transform.parent.gameObject);
        Vector3 grenadePos = grenade.transform.position;
        Destroy(grenade);
        GameObject tempBullet;
        Rigidbody tempRb;
        Vector3 radnomDir;
        for (int i = 0; i < 8; i++)
        {
            tempBullet = Instantiate(bulletPrefab, grenadePos, Quaternion.identity);
            tempRb = tempBullet.GetComponent<Rigidbody>();
            radnomDir = new(Random.Range(-1, 1), 0, Random.Range(-1, 1));
            radnomDir.Normalize();
            tempRb.AddForce(radnomDir * Blaster.GetBulletForce(), ForceMode.Impulse);
        }
        Debug.Log("SPAWN BULLET BARRAGE");
    }

    public static void SpawnSuperBullet(GameObject bullet)
    {
        if (bhTimer > 0)
            return;
        bhTimer = 50;

        GameObject superBullet = Instantiate(
            superBulletPrefab,
            bullet.transform.position,
            Quaternion.identity
        );
        Rigidbody superRb = superBullet.GetComponent<Rigidbody>();
        superRb.velocity = bullet.GetComponentInParent<Rigidbody>().velocity;
        Destroy(bullet.transform.parent.gameObject);
        Debug.Log("SPAWN SUPER BULLET");
    }

    public static void SpawnDOTCloud(GameObject grenade)
    {
        if (fgTimer > 0)
            return;
        fgTimer = 50;

        Instantiate(dotCloudPrefab, grenade.transform.position, Quaternion.identity);
        Destroy(grenade);
        Debug.Log("SPAWN DOT CLOUD");
    }

    public static void SpawnSuperHammer(GameObject hammer)
    {
        if (fhTimer > 0)
            return;
        fhTimer = 50;

        Instantiate(superHammerPrefab, hammer.transform.position, Quaternion.identity);
        Debug.Log("SPAWN SUPER HAMMER");
    }
}
