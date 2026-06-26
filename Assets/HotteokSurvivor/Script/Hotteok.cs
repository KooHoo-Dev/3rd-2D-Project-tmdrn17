using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace HotteokSurvivor
{
    public abstract class Hotteok : MonoBehaviour
    {
        [SerializeField] protected Enemy enemy;
        [SerializeField] protected Transform startPos;
        [SerializeField] protected float endDistance;
        [SerializeField] protected float shotSpeed;
        [SerializeField] protected float hitRadius = 1f;
        [SerializeField] protected int damage;

        protected virtual void Start()
        {
            transform.position = startPos.position;
            gameObject.SetActive(true);
        }

        protected void Update()
        {
            transform.position += Vector3.up * shotSpeed * Time.deltaTime;

            float distance = Vector2.Distance(startPos.position, transform.position);

            // 1. 총알의 현재 방 번호 구하기
            Vector2Int myCell = GridManager.Instance.GetCellIndex(transform.position);

            // 2. 내 방 + 주변 8방(총 9칸)의 좌표를 반복문으로 돌며 명단을 가져옴
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Vector2Int checkCell = myCell + new Vector2Int(x, y);

                    // 해당 방에 적들이 있다면?
                    if (GridManager.Instance.grid.TryGetValue(checkCell, out List<Enemy> enemies))
                    {
                        // 3. 그 방에 있는 소수의 적들과만 거리 계산!
                        for (int i = 0; i < enemies.Count; i++)
                        {
                            Enemy enemy = enemies[i];

                            // sqrMagnitude로 고속 거리 연산
                            float sqrDistance = (transform.position - enemy.transform.position).sqrMagnitude;
                            float combinedRadius = hitRadius + enemy.hitRadius;

                            if (sqrDistance <= combinedRadius * combinedRadius)
                            {
                                enemy.TakeDamage(10);
                                transform.position = startPos.position;
                                gameObject.SetActive(false);
                                return; // 하나 맞췄으면 총알 검사 즉시 종료!
                            }
                        }
                    }
                }
            }

            if (distance >= endDistance)
            {
                transform.position = startPos.position;
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