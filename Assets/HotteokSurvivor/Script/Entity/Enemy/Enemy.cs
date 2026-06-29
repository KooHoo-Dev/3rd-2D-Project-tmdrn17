using Study.Utilities;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace HotteokSurvivor
{
    public abstract class Enemy : CombatEntity
    {
        [SerializeField] protected float stoppingDistance = 0.1f;

        [field: SerializeField] protected BaseStat Stat { get; set; }
        private GridManager gridManager;
        private Vector2Int currentPos;

        [SerializeField] protected Player Target;

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