using System;
using System.Collections;
using UnityEngine;

namespace HotteokSurvivor
{
    public class MeleeEnemy : Enemy
    {
        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();

            float sqrDistance = (transform.position - Target.transform.position).sqrMagnitude;
            float combinedRadius = hitRadius + Target.hitRadius;

            bool isIn = sqrDistance <= combinedRadius * combinedRadius;

            if (isIn && isAttack == false)
            {
                StartCoroutine(SendDamage());
            }
        }

        private IEnumerator SendDamage()
        {
            Target.TakeDamage(Stat.Damage);
            isAttack = true;
            yield return new WaitForSeconds(attackCoolTime);
            isAttack = false;
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
        }
    }
}