using System.Collections;
using System.Collections.Generic;
using Players;
using Unity.Networking.Transport.Error;
using Unity.VisualScripting;
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
    [SerializeField]
    WeaponType weaponType;
    static GameObject superHammerPrefab;

    static GameObject bulletBarragePrefab;

    static GameObject superBulletPrefab;
    static GameObject dotCloudPrefab;
    static GameObject superBlasterPrefab;

    static int bgTimer = 0;
    static int bhTimer = 0;
    static int fgTimer = 0;
    static int fhTimer = 0;

    static DarkPlayer darkPlayer;
    static LightPlayer lightPlayer;

    public enum WeaponType
    {
        BULLET,
        GREANADE,
        FLAMETHROWER,
        HAMMER
    }

    public enum ComboAttackType
    {
        BulletBarrage,
        SuperBullet,
        DOTCloud,
        SuperHammer,
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletBarragePrefab = Resources.Load<GameObject>("BulletBarrage");
        dotCloudPrefab = Resources.Load<GameObject>("DOTCloud");
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

    public static void SetDarkPlayer(DarkPlayer dp)
    {
        darkPlayer = dp;
    }

    public static void SetLightPlayer(LightPlayer lp)
    {
        lightPlayer = lp;
    }

    public WeaponType GetWeaponType()
    {
        return weaponType;
    }

    public static void SpawnBulletBarrage(GameObject grenade, GameObject bullet)
    {
        if (bgTimer > 0)
            return;
        bgTimer = 50;
        // It's VERY important to delete the grenade object first. Otherwise, it's theoretically possible that the spawned bullets collide with the same grenade.
        // This will not only result in more bullets being spawned, but can also potentially crash the game (trying to destroy a gameobject that already destroyed -> nullObjectReference)
        //Destroy(bullet.transform.parent.gameObject);
        Destroy(bullet);
        Vector3 grenadePos = grenade.transform.position;
        Destroy(grenade);
        GameObject tempBullet;
        Rigidbody tempRb;
        //Vector3 randomDir;
        int bulletCount = 12;
        for (int i = 0; i < bulletCount; i++)
        {
            tempBullet = Instantiate(bulletBarragePrefab, grenadePos, Quaternion.identity);
            tempRb = tempBullet.GetComponent<Rigidbody>();
            // randomDir = new(Random.Range(-1, 1), 0, Random.Range(-1, 1));
            // randomDir.Normalize();
            // tempRb.AddForce(randomDir * Blaster.GetBulletForce(), ForceMode.Impulse);

            float angle = i * (360f / bulletCount);
            Vector3 direction = new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                0f,
                Mathf.Sin(angle * Mathf.Deg2Rad)
            );
            tempRb.velocity = direction * 10f;
        }
    }

    public static void SpawnSuperBlaster()
    {
        if (bhTimer > 0)
            return;
        bhTimer = 5;
        lightPlayer.SetIsBlasterSuper(true);
    }

    public static void SpawnSuperHammer()
    {
        if (fhTimer > 0)
            return;
        fhTimer = 5;

        //Instantiate(superHammerPrefab, hammer.transform.position, Quaternion.identity);
        darkPlayer.SetIsHammerSuper(true);
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
        //Destroy(bullet.transform.parent.gameObject);
        Destroy(bullet);
    }

    public static void SpawnDOTCloud(GameObject grenade)
    {
        if (fgTimer > 0)
            return;
        fgTimer = 210; // cloud death timer is 200 FUs, so I'm making this slightly higher

        // Instantiate the cloud at the grenade's position
        Instantiate(dotCloudPrefab, grenade.transform.position, Quaternion.identity);

        Destroy(grenade); // Destroy the grenade
    }
}
