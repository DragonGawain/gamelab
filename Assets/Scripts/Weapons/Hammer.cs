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
        // protected static Hammer instance;

        void Awake()
        {
            Debug.Log("THE HAMMER AWAKENS");
            // singleton pattern - there can only be a single instance of the super hammer
            // if (instance == null)
            //     instance = this;
            // if (instance != this)
            //     Destroy(this.gameObject);
        }

        public override void OnFire()
        {
            if (rotationSequence == null)
            {
                slam();
            }
        }

        protected void slam()
        {
            rotationSequence = DOTween.Sequence();
            transform.localRotation = Quaternion.identity;
            rotationSequence.Append(
                transform.DOLocalRotate(new Vector3(-65, 0, -40), 0.2f).SetEase(Ease.InBack)
            );
            rotationSequence.AppendInterval(0.1f);
            rotationSequence.Append(
                transform.DOLocalRotate(Vector3.zero, 0.1f).SetEase(Ease.OutBack)
            );
            rotationSequence.OnComplete(() => rotationSequence = null);
        }

        public override void StopFire() { }

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
