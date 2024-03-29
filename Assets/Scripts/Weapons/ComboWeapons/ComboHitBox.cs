using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboHitBox : MonoBehaviour
{
    public enum WeaponType
    {
        BULLET,
        GREANADE,
        FLAMETHROWER,
        HAMMER
    }

<<<<<<< Updated upstream
    [SerializeField]
    WeaponType weaponType;

=======
    [SerializeField] WeaponType weaponType;
   
>>>>>>> Stashed changes
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ComboHitBox>() == null)
            return;

        switch (weaponType)
        {
            case WeaponType.BULLET:
<<<<<<< Updated upstream
                if (
                    other.gameObject.GetComponent<ComboHitBox>().GetWeaponType()
                    == WeaponType.GREANADE
                )
=======
                if (other.gameObject.GetComponent<ComboHitBox>().GetWeaponType() == WeaponType.GRENADE)
>>>>>>> Stashed changes
                {
                    Debug.Log("here: " + other.gameObject + " and " + this.gameObject);
                    ComboAttackManager.SpawnBulletBarrage(other.gameObject, this.gameObject);
                }
             
                break;
            case WeaponType.FLAMETHROWER:
                if (
                    other.gameObject.GetComponent<ComboHitBox>().GetWeaponType()
                    == WeaponType.GREANADE
                )
                {
                    ComboAttackManager.SpawnDOTCloud(other.gameObject);
                }
                else if (
                    other.gameObject.GetComponent<ComboHitBox>().GetWeaponType()
                    == WeaponType.HAMMER
                )
                {
                    ComboAttackManager.SpawnSuperHammer();
                }
                break;
<<<<<<< Updated upstream
            case WeaponType.GREANADE:
                if (
                    other.gameObject.GetComponent<ComboHitBox>().GetWeaponType()
                    == WeaponType.BULLET
                )
                {
                    ComboAttackManager.SpawnBulletBarrage(this.gameObject, other.gameObject);
                }
                else if (
                    other.gameObject.GetComponent<ComboHitBox>().GetWeaponType()
                    == WeaponType.FLAMETHROWER
                )
                {
                    ComboAttackManager.SpawnDOTCloud(this.gameObject);
                }
                break;
=======
            //case WeaponType.GRENADE:
            //    if (
            //        other.gameObject.GetComponent<ComboHitBox>().GetWeaponType()
            //        == WeaponType.BULLET
            //    )
            //    {
            //        ComboAttackManager.SpawnBulletBarrage(this.gameObject, other.gameObject);
            //    }
            //    else if (
            //        other.gameObject.GetComponent<ComboHitBox>().GetWeaponType()
            //        == WeaponType.FLAMETHROWER
            //    )
            //    {
            //        ComboAttackManager.SpawnDOTCloud(this.gameObject);
            //    }
            //    break;
>>>>>>> Stashed changes
            case WeaponType.HAMMER:
                if (
                    other.gameObject.GetComponent<ComboHitBox>().GetWeaponType()
                    == WeaponType.BULLET
                )
                {
                    ComboAttackManager.SpawnSuperBullet(other.gameObject);
                }
                else if (
                    other.gameObject.GetComponent<ComboHitBox>().GetWeaponType()
                    == WeaponType.FLAMETHROWER
                )
                {
                    ComboAttackManager.SpawnSuperHammer();
                }
                break;
        }
    }

    // if the FT is staying in contact with the hammer, continuously reset the super hammer timer
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<ComboHitBox>() == null)
            return;

        switch (weaponType)
        {
            case WeaponType.FLAMETHROWER:
                if (
                    other.gameObject.GetComponent<ComboHitBox>().GetWeaponType()
                    == WeaponType.HAMMER
                )
                {
                    ComboAttackManager.SpawnSuperHammer();
                }
                break;
            case WeaponType.HAMMER:
                if (
                    other.gameObject.GetComponent<ComboHitBox>().GetWeaponType()
                    == WeaponType.FLAMETHROWER
                )
                {
                    ComboAttackManager.SpawnSuperHammer();
                }
                break;
            default:
                break;
        }
    }

    public WeaponType GetWeaponType()
    {
        return weaponType;
    }
}
