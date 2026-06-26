using UnityEngine;

namespace HotteokSurvivor
{
    // # CombatEntity - 전투에 참여하는 모든 개체의 공통 부모(추상 클래스)
    // - Player 개체와 Enemy 개체로 구성되며 원활한 개발을 위해 Dummy 개체도 포함된다
    // - 개발 순서
    // 1. Player => Dummy
    // 2. Enemy => Player

    public abstract class CombatEntity : MonoBehaviour
    {
        // 전투 개체의 머리 상단 부분 Pivot
        // Hp 바 또는 이름등을 출력하기 위해 사용
        [field: SerializeField] public Transform HeadUpPivot { get; private set; }
        [field: SerializeField] public virtual BaseStat BaseStat { get; }

        public abstract void TakeDamage(int damage);

        public virtual void TakeHeal(int heal)
        {

        }
    }
}
