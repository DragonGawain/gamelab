using System;
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
        protected float swingSpeed;
        protected Sequence rotationSequence;

        protected bool hit;

        // protected static Hammer instance;

        private static bool isSwinging = false;
        private static bool shouldBeSuper = false;

        protected int dmg = 10;

        void Awake()
        {
            shouldBeSuper = false;
        }

        public override void OnFire()
        {
            if (rotationSequence == null)
            {
                isSwinging = true;
                slam();
            }
        }

        protected virtual void slam()
        {
            hit = true;
            rotationSequence = DOTween.Sequence();
            transform.localRotation = Quaternion.identity;
            rotationSequence.Append(
                transform.DOLocalRotate(new Vector3(75, 0, -5), 0.2f).SetEase(Ease.InBack)
            );
            rotationSequence.AppendInterval(0.1f);
            rotationSequence.Append(
                transform.DOLocalRotate(Vector3.zero, 0.1f).SetEase(Ease.OutBack)
            );
            rotationSequence.OnComplete(StopFire);
        }

        public override void StopFire()
        {
            hit = false;
            rotationSequence = null;
            isSwinging = false;
            if (shouldBeSuper)
                ComboAttackManager.SpawnSuperHammer();
        }

        public void Destroy()
        {
            if (rotationSequence != null)
                rotationSequence.Kill();

            Destroy(this.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (hit && other.CompareTag("BasicEnemy"))
                other.GetComponent<Enemy>().OnHit(dmg, "DarkPlayer");
        }

        public static bool GetIsSwinging()
        {
            return isSwinging;
        }

        public static void SetShouldBeSuper()
        {
            shouldBeSuper = true;
        }
    }
}
