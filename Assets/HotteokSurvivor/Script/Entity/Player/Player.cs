using Study.Utilities;
using UnityEngine;

namespace HotteokSurvivor
{
    [System.Serializable]
    public class PlayerStat : BaseStat
    {
        public int exp;
    }

    public class Player : CombatEntity
    {
        [field: SerializeField] private PlayerStat Stat { get; set; }
        public override BaseStat BaseStat => Stat;

        private void Update()
        {
            Move();
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
            
        }

        public void LevelUp()
        {

        }
    }
}
