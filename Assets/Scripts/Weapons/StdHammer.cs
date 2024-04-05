using System.Collections;
using System.Collections.Generic;
using Weapons;

namespace Weapons
{
    public class StdHammer : Hammer
    {
        private void Start()
        {
            dmg = 20;
            SetWeaponName("Hammer");
        }
    }
}
