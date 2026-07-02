using Study.Utilities;
using UnityEngine;

namespace HotteokSurvivor
{
    [System.Serializable]
    public class EnemyStat : BaseStat
    {
        public int Damage;
    }

    public abstract class Enemy : CombatEntity
    {
        [SerializeField] protected float stoppingDistance = 0.1f;

        [field: SerializeField] protected EnemyStat Stat { get; set; }
        public override BaseStat BaseStat => Stat;

        private GridManager gridManager;
        private PoolingSystem poolingSystem;

        private Vector2Int currentPos;

        public Player Target;
        protected GameObject Money;

        protected bool isAttack = false;
        [SerializeField] protected float attackCoolTime = 0.1f;

        protected virtual void OnEnable()
        {
            gridManager = GridManager.Instance;
            poolingSystem = PoolingSystem.Instance;
            currentPos = gridManager.GetCellIndex(transform.position);
            Target = GameManager.Instance.Player;

            gridManager.AddToCell(currentPos, gameObject);

            Stat.ResetToFull();
        }

        protected virtual void Update()
        {
            // 1. 이번 프레임의 새 방 번호를 딱 한 번만 구해서 저장합니다.
            Vector2Int newPos = gridManager.GetCellIndex(transform.position);

            // 2. Vector2Int는 서로 다름(!=)을 바로 비교할 수 있습니다.
            if (newPos != currentPos)
            {
                gridManager.DeleteToCell(currentPos, gameObject);
                gridManager.AddToCell(newPos, gameObject);
                currentPos = newPos; // 현재 방 번호 갱신
            }

            Move();
        }

        protected override void Move()
        {
            Vector2 inputVector = Target.transform.position - transform.position;
            Vector3 localscale = transform.localScale;

            float absMovement = Mathf.Abs(inputVector.x);
            float currentScaleX = Mathf.Abs(localscale.x);

            if (inputVector != Vector2.zero)
            {
                if (transform.IsInRange(Target.transform.position, stoppingDistance))
                {
                    return;
                }
                else
                {
                    Vector3 moveVec = inputVector;

                    transform.Translate(moveVec.normalized * Stat.MoveSpeed * Time.deltaTime);
                }
            }

            if (absMovement > 0)
            {
                if (transform.IsInRange(Target.transform.position, stoppingDistance))
                {
                    transform.localScale = new Vector3
                        (currentScaleX * FacingDirection, localscale.y, localscale.z);
                }
                else
                {
                    FacingDirection = (inputVector.x < 0) ? -1 : 1;
                    transform.localScale = new Vector3
                        (currentScaleX * FacingDirection, localscale.y, localscale.z);
                }
            }
        }

        public override void TakeDamage(int damage)
        {
            // enemy 죽을때 실행
            if (Stat.ApplyDamage(Mathf.Clamp(damage - Stat.DefensivePower, 0, damage)))
            {
                gridManager.DeleteToCell(currentPos, gameObject);
                Money dropmoney = poolingSystem.moneyPool.Get(true);
                dropmoney.transform.position = transform.position;
                dropmoney.InIt();
                gameObject.SetActive(false);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, hitRadius);
        }
    }
}