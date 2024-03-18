using System.Collections;
using System.Collections.Generic;
using Weapons;

namespace Weapons
{
    public class StdHammer : Hammer
    {
        void Start()
        {
            SetDamage(20);
            SetWeaponName("Hammer");
        }
    }
}
