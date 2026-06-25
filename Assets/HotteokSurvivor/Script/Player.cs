using UnityEngine;

namespace HotteokSurvivor
{
    public class PlayerStat : BaseStat
    {
        public int exp;
    }

    public class Player : CombatEntity
    {
        [field: SerializeField] private PlayerStat Stat { get; set; }
        public override BaseStat BaseStat => Stat;

        public override void TakeDamage(int damage)
        {
            
        }

        public void LevelUp()
        {

        }
    }
}
