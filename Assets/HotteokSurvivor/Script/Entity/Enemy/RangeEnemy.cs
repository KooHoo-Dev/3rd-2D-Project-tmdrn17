using Study.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HotteokSurvivor
{
    public class RangeEnemy : Enemy
    {
        [SerializeField] private EnemyBullet bulletPrefab;
        private ComponentPool<EnemyBullet> bulletPool;

        [SerializeField] List<EnemyBullet> bullets = new List<EnemyBullet>();

        protected override void Start()
        {
            base.Start();

            bulletPool = new ComponentPool<EnemyBullet>(bulletPrefab, transform);

            bullets.Add(bulletPool.Get());
        }

        protected override void Update()
        {
            base.Update();

            float sqrDistance = (transform.position - Target.transform.position).sqrMagnitude;
            float combinedRadius = hitRadius + Target.hitRadius;

            bool isIn = sqrDistance <= combinedRadius * combinedRadius;

            if (isIn && isAttack == false)
            {
                StartCoroutine(ShootingCoroutine());
            }
        }

        private IEnumerator ShootingCoroutine()
        {
            for (int i = 0; i < bullets.Count; ++i)
            {
                if (bullets[i].gameObject.activeSelf == false)
                {
                    bulletPool.Get();
                }
            }
            isAttack = true;
            yield return new WaitForSeconds(attackCoolTime);
            isAttack = false;
        }
    }
}