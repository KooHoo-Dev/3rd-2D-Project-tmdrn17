using Study.Utilities;
using System.Collections.Generic;
using HotteokSurvivor.Script.Manager_And_System;
using UnityEngine;
using UnityEngine.UI;

namespace HotteokSurvivor
{
    [System.Serializable]
    public class PlayerStat : BaseStat
    {
        public int Level;
        public int MaxExp;
        public int Exp;
    }

    public class Player : CombatEntity
    {
        [field: SerializeField] public PlayerStat Stat { get; private set; }

        [SerializeField] private Slider hpSlider;

        private SpriteRenderer spriteRenderer;

        protected override BaseStat BaseStat => Stat;

        [SerializeField] protected Money money;
        private Vector2Int checkCell;

        public HotteokRotate hotteokRotate;

        private Animator animator;

        private void Start()
        {
            Stat.ResetToFull();
            GameManager.Instance.Player = this;
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            Move();
            SearchEnemy();

            hpSlider.value = (float)Stat.Hp / Stat.MaxHp;
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
                    (currentScaleX, localscale.y, localscale.z);
            }

            if (inputVector != Vector2.zero)
            {
                Vector3 moveVec = inputVector;

                transform.Translate(moveVec.normalized * Stat.MoveSpeed * Time.deltaTime);

                spriteRenderer.flipX = (FacingDirection == -1) ? true : false;

                animator.SetBool("IsMove", true);
            }
            else
            {
                animator.SetBool("IsMove", false);
            }
        }

        public override void TakeDamage(int damage)
        {
            if (Stat.ApplyDamage(Mathf.Clamp(damage - Stat.DefensivePower, 0, damage)))
            {
                GameManager.Instance.Canvas.transform.GetChild(1).gameObject.SetActive(true);
                GameManager.Instance.Canvas.transform.GetChild(4).gameObject.SetActive(false);
                Destroy(GameManager.Instance.PoolingSystem.gameObject);
                Time.timeScale = 0;
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

                    if (GridManager.Instance.Grid.TryGetValue(checkCell, out List<GameObject> objects))
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
            Stat.Exp += exp;

            if (Stat.Exp >= Stat.MaxExp)
            {
                UIManager uIManage = GameManager.Instance.Canvas.GetComponent<UIManager>();

                GameManager.Instance.LevelUpPopUp();

                uIManage.expSlider.value = 1f;

                Stat.Exp = 0;
                Stat.MaxExp += 1;
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
