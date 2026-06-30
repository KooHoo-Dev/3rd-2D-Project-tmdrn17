using Study.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace HotteokSurvivor
{
    [System.Serializable]
    public class PlayerStat : BaseStat
    {
        public int Level;
        public int maxExp;
        public int exp;
    }

    public class Player : CombatEntity
    {
        [field: SerializeField] private PlayerStat Stat { get; set; }
        public override BaseStat BaseStat => Stat;

        [SerializeField] protected Money money;
        private Vector2Int checkCell;


        private void Start()
        {
            Stat.ResetToFull();
        }

        private void Update()
        {
            Move();
            SearchEnemy();
        }

        protected override void Move()
        {
            Vector2 inputVector = SimpleInput.GetMoveAxisRaw();
            float absMovement = Mathf.Abs(inputVector.x);
            Vector3 localscale = transform.localScale;

            if (absMovement > 0)
            {
                float currentScaleX = Mathf.Abs(localscale.x);
                FacingDirection = (inputVector.x < 0) ? -1 : 1;
                transform.localScale = new Vector3
                    (currentScaleX * FacingDirection, localscale.y, localscale.z);
            }

            if (inputVector != Vector2.zero)
            {
                Vector3 moveVec = inputVector;

                transform.Translate(moveVec.normalized * Stat.MoveSpeed * Time.deltaTime);
            }
        }

        public override void TakeDamage(int damage)
        {
            if (Stat.ApplyDamage(Mathf.Clamp(damage - Stat.DefensivePower, 0, damage)))
            {
                Debug.Log("is Dead");
            }
            else
            {
                Debug.Log("is Damaged");
            }
        }

        private void SearchEnemy()
        {
            Vector2Int myCell = GridManager.Instance.GetCellIndex(transform.position);

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    checkCell = myCell + new Vector2Int(x, y);

                    if (GridManager.Instance.grid.TryGetValue(checkCell, out List<GameObject> objects))
                    {
                        for (int i = 0; i < objects.Count; i++)
                        {
                            if (objects[i].TryGetComponent<Money>(out _))
                            {
                                money = objects[i].GetComponent<Money>();

                                float sqrDistance = (transform.position - money.transform.position).sqrMagnitude;
                                float combinedRadius = hitRadius + money.hitRadius;

                                bool isIn = sqrDistance <= combinedRadius * combinedRadius;

                                if (isIn)
                                {
                                    LevelUp(money.exp);
                                    money.gameObject.SetActive(false);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void LevelUp(int exp)
        {
            Stat.exp += exp;

            if (Stat.exp >= Stat.maxExp)
            {
                Stat.exp = 0;
                Stat.maxExp += 1;
                Stat.Level++;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, hitRadius);
        }
    }
}
