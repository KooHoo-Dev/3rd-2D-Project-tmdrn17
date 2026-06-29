using HotteokSurvivor;
using System;
using System.Collections;
using UnityEngine;

namespace HotteokSurvivor
{
    public class DdalgiHotteok : Hotteok
    {
        [SerializeField] private float coolDown = 0.5f;

        protected override void OnEnable()
        {
            HotteokRotate parent = GetComponentInParent<HotteokRotate>();

            parent.AddHotteok(name.ToString(), gameObject);

            HitEnemies.Clear();
            SearchEnemy();
        }

        protected override void Update()
        {
            SearchEnemy();
        }


        protected override void SendDamage()
        {
            enemy.TakeDamage(damage);
            StartCoroutine(CoolDownCoroutine());
        }

        private IEnumerator CoolDownCoroutine()
        {
            yield return new WaitForSeconds(coolDown);
            HitEnemies.Clear();
        }
        protected override void Move()
        {

        }
    }
}