using HotteokSurvivor;
using Study.Utilities;
using UnityEngine;

namespace HotteokSurvivor
{
    public class PatHptteok : Hotteok
    {
        public bool isAttack = false;

        protected override void OnEnable()
        {
            base.OnEnable();
            RotateZ();
        }

        protected override void Move()
        {
            transform.Translate(Vector3.up * shotSpeed * Time.deltaTime);
        }

        protected override void SendDamage()
        {
            enemy.TakeDamage(damage);
            isAttack = true;
            Debug.Log("is Attack!");
        }

        private void RotateZ()
        {
            Vector2 mousePosition = SimpleInput.GetMousePosition();

            // 화면 좌표계에서 World(Scene)의 좌표계로 전환
            // 전환 할 때는 Camera의 기능을 이용
            Vector3 worldMousePoint = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector3 myPosition = transform.position;

            // 2D 카메라는 기본적으로 z값이 -10 정도로 돼있으므로 z값을 보정해준다
            worldMousePoint.z = transform.position.z;

            Vector3 dir = (worldMousePoint - myPosition).normalized;
            transform.up = dir;
        }
    }
}
