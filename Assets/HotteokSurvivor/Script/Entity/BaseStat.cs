using UnityEngine;
using System;

namespace HotteokSurvivor
{
    [System.Serializable]
    public class BaseStat
    {
        public int MaxHp;
        public int DefensivePower;
        public float MoveSpeed = 1.0f;

        [field: SerializeField] public int Hp { get; private set; }

        private bool isDead => (Hp <= 0);

        /// <summary>
        /// 스탯 객체에게 데미지를 적용하고, 죽었는지를 반환하는 함수
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>

        public bool ApplyDamage(int damage)
        {
            Hp = Mathf.Clamp(Hp - damage, 0, MaxHp);
            return isDead;
        }

        public void Apply(int heal)
        {
            Hp = Mathf.Clamp(Hp + heal, 0, MaxHp);
        }

        public void ResetToFull()
        {
            Hp = MaxHp;
        }
    }
}
