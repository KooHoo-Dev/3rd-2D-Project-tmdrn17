using UnityEngine;

namespace HotteokSurvivor
{
    public abstract class Enemy : CombatEntity
    {
        [field: SerializeField] private BaseStat Stat { get; set; }

        public override void TakeDamage(int damage)
        {
            if (Stat.ApplyDamage(Mathf.Clamp(damage - Stat.DefensivePower, 0, damage)))
            {
                Debug.Log("Enemy is Dead");
            }
            else
            {
                Debug.Log("Enemy is Damaged");
            }
        }
    }
}