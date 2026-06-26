using UnityEngine;

namespace HotteokSurvivor
{
    public abstract class Enemy : CombatEntity
    {
        [field: SerializeField] private BaseStat Stat { get; set; }
        private GridManager gridManager;
        private Vector2Int currentPos;

        public float hitRadius;

        protected virtual void Start()
        {
            gridManager = GridManager.Instance;
            currentPos = gridManager.GetCellIndex(transform.position);

            gridManager.AddToCell(currentPos, this);

            Stat.ResetToFull();
        }

        protected virtual void Update()
        {
            // 1. 이번 프레임의 새 방 번호를 딱 한 번만 구해서 저장합니다.
            Vector2Int newPos = gridManager.GetCellIndex(transform.position);

            // 2. Vector2Int는 서로 다름(!=)을 바로 비교할 수 있습니다.
            if (newPos != currentPos)
            {
                gridManager.DeleteToCell(currentPos, this);
                gridManager.AddToCell(newPos, this);
                currentPos = newPos; // 현재 방 번호 갱신
            }
        }

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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, hitRadius);
        }
    }
}