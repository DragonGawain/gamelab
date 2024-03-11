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

    [SerializeField] WeaponType weaponType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ComboHitBox>() == null)
            return;

        switch (weaponType)
        {
            case WeaponType.BULLET:
                if (other.gameObject.GetComponent<ComboHitBox>().GetWeaponType() == WeaponType.GREANADE)
                {
                    ComboAttackManager.SpawnBulletBarrage(other.gameObject, this.gameObject);
                }
                else if (other.gameObject.GetComponent<ComboHitBox>().GetWeaponType() == WeaponType.HAMMER)
                {
                    ComboAttackManager.SpawnSuperBullet(this.gameObject);
                }
                break;
            case WeaponType.FLAMETHROWER:
                if (other.gameObject.GetComponent<ComboHitBox>().GetWeaponType() == WeaponType.GREANADE)
                {
                    ComboAttackManager.SpawnDOTCloud(other.gameObject);
                }
                else if (other.gameObject.GetComponent<ComboHitBox>().GetWeaponType() == WeaponType.HAMMER)
                {
                    ComboAttackManager.SpawnSuperHammer(other.gameObject);
                }
                break;
            case WeaponType.GREANADE:
                if (other.gameObject.GetComponent<ComboHitBox>().GetWeaponType() == WeaponType.BULLET)
                {
                    ComboAttackManager.SpawnBulletBarrage(this.gameObject, other.gameObject);
                }
                else if (other.gameObject.GetComponent<ComboHitBox>().GetWeaponType() == WeaponType.FLAMETHROWER)
                {
                    ComboAttackManager.SpawnDOTCloud(this.gameObject);
                }
                break;
            case WeaponType.HAMMER:
                if (other.gameObject.GetComponent<ComboHitBox>().GetWeaponType() == WeaponType.BULLET)
                {
                    ComboAttackManager.SpawnSuperBullet(other.gameObject);
                }
                else if (other.gameObject.GetComponent<ComboHitBox>().GetWeaponType() == WeaponType.FLAMETHROWER)
                {
                    ComboAttackManager.SpawnSuperHammer(this.gameObject);
                }
                break;
        }
    }

    public WeaponType GetWeaponType()
    {
        return weaponType;
    }
}
