using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening;
using UnityEngine;
using Weapons;

namespace Weapons
{
    public class Hammer : Weapon
    {
        [SerializeField]
        private float swingSpeed;

        private Sequence rotationSequence;
        private Transform handle;
        void Start()
        {
            SetDamage(20);
            SetWeaponName("Hammer");
            handle = transform;
        }

        public override void OnFire()
        {
            
            if (rotationSequence == null)
            {
                slam();    
            }
            
        }

        private void slam()
        {
            rotationSequence = DOTween.Sequence();
            handle.localRotation = Quaternion.identity;
            rotationSequence.Append(handle.DOLocalRotate(new Vector3(-65, 0, -40), 0.2f).SetEase(Ease.InBack));
            rotationSequence.AppendInterval(0.1f);
            rotationSequence.Append(handle.DOLocalRotate(Vector3.zero, 0.1f).SetEase(Ease.OutBack));
            rotationSequence.OnComplete(() => rotationSequence = null);
        }

        public override void StopFire()
        {
            
        }

        public void Destroy()
        {
            if (rotationSequence != null)
            {
                rotationSequence.Kill();
            }

            // Destroy(transform.parent.gameObject);
            
            Destroy(this.gameObject);
        }
    }
}