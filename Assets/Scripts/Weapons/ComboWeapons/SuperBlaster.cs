using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class SuperBlaster : Blaster
{
    static int deathTimer = 450;

    void Start()
    {
        SetWeaponName("SuperBlaster");
        // This is not a 'safe' way, but it will work. Just make sure that the fire point is alwayse the 0th child.
        firePoint = transform.GetChild(0);
    }

    void FixedUpdate()
    {
        deathTimer--;
        if (deathTimer <= 0)
            player.SetIsBlasterSuper(false);
    }

    public static void ResetSuperBlasterTimer()
    {
        deathTimer = 450;
    }
}
