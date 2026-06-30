using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace HotteokSurvivor
{
    public enum HotteokName
    {
        Normal,
        Pat,
        Ddalgi,
    }

    public abstract class Hotteok : MonoBehaviour
    {
        [SerializeField] protected HotteokName name;
        [SerializeField] protected Enemy enemy;

        [SerializeField] protected Transform startPos;
        [SerializeField] protected float endDistance;

        [SerializeField] protected float shotSpeed;
        [SerializeField] protected float hitRadius = 1f;
        [SerializeField] protected int damage;

        public HashSet<Enemy> HitEnemies = new HashSet<Enemy>();

        private Vector2Int checkCell;

        protected virtual void OnEnable()
        {
            startPos = transform.parent;
            transform.position = startPos.position;
            enemy = null;
            HitEnemies.Clear();
            SearchEnemy();
        }

        protected virtual void Update()
        {
            SearchEnemy();

            Move();

            float distance = Vector2.Distance(startPos.position, transform.position);

            if (distance >= endDistance)
            {
                transform.position = startPos.position;
                gameObject.SetActive(false);
            }
        }

        protected abstract void Move();

        protected abstract void SendDamage();

        protected void SearchEnemy()
        {
            // 1. 총알의 현재 방 번호 구하기
            Vector2Int myCell = GridManager.Instance.GetCellIndex(transform.position);

            // 2. 내 방 + 주변 8방(총 9칸)의 좌표를 반복문으로 돌며 명단을 가져옴
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    checkCell = myCell + new Vector2Int(x, y);

                    // 해당 방에 적들이 있다면?
                    if (GridManager.Instance.grid.TryGetValue(checkCell, out List<GameObject> enemies))
                    {
                        // 3. 그 방에 있는 소수의 적들과만 거리 계산!
                        for (int i = 0; i < enemies.Count; i++)
                        {
                            /* (out _)키워드: 무시(Discard, 버리기)'**를 뜻하는 특별한 문법입니다.
                                        Dictionary<key, value>.TryGetValue, gameObject.TryGetComponent 등 
                                        함수의 규칙상 값을 받아올 자리를 비워둘 순 없지만, 
                                        실제로는 그 값을 전혀 사용하지 않을 때 사용합니다.
                                        작동 방식: 컴퓨터에게 "이 자리에 들어올 값은 필요 없으니 
                                        변수를 메모리에 만들지 말고 그냥 버려줘"라고 지시합니다.
                                        장점: 불필요한 변수 선언이 사라져 코드가 깔끔해지고, 메모리도 절약됩니다.*/
                            if (enemies[i].TryGetComponent<Enemy>(out _))
                            {
                                enemy = enemies[i].GetComponent<Enemy>();

                                float sqrDistance = (transform.position - enemy.transform.position).sqrMagnitude;
                                float combinedRadius = hitRadius + enemy.hitRadius;

                                bool enemyHit = HitEnemies.Contains(enemy);
                                bool isIn = sqrDistance <= combinedRadius * combinedRadius;

                                if (isIn && (enemyHit == false))
                                {
                                    HitEnemies.Add(enemy);
                                    SendDamage();
                                    return; // 하나 맞췄으면 총알 검사 즉시 종료!
                                }
                            }
                        }
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, hitRadius);
        }
    }
}