using System.Collections.Generic;
using Study.Utilities;
using UnityEngine;

namespace HotteokSurvivor.Script.Manager_And_System
{
    public class PoolingSystem : SingletonBase<PoolingSystem>
    {
        public int enemyCount;
        public float xReplaceDistance;
        public float yReplaceDistance;

        private Vector3 screen;
        private Vector3 worldPos;

        public int haveHotteoksKind;
        public int[] hotteokLevel;

        [SerializeField] private Transform hotteokMuzzle;
        [SerializeField] private Transform[] enemySpawnPoints;
        [SerializeField] private Transform moneyParent;

        [SerializeField] private Enemy[] enemise;
        [SerializeField] private Hotteok[] hotteoks;
        [SerializeField] private Money money;

        private ComponentPool<Enemy>[] enemisePool;
        private ComponentPool<Hotteok>[] hotteoksPool;
        public ComponentPool<Money> MoneyPool;

        public List<Enemy> enemiseList = new List<Enemy>();
        private Dictionary<HotteokName, List<Hotteok>> hotteokDic = new Dictionary<HotteokName, List<Hotteok>>();
        [SerializeField] private List<Money> moneyList = new List<Money>();

        private void Start()
        {
            GameManager.Instance.PoolingSystem = this;

            enemisePool = new ComponentPool<Enemy>[enemise.Length];
            hotteoksPool = new ComponentPool<Hotteok>[hotteoks.Length];

            for (int i = 0; i < enemise.Length; i++)
            {
                enemisePool[i] = new ComponentPool<Enemy>(enemise[i]);

                AddEnemyList(i, enemyCount);
            }

            for (int i = 0; i < hotteoks.Length; i++)
            {
                hotteoksPool[i] = new ComponentPool<Hotteok>(hotteoks[i], hotteokMuzzle);
            }

            MoneyPool = new ComponentPool<Money>(money, moneyParent);
        }

        private void Update()
        {
            screen = new Vector3(Screen.width / 2, 0);
            if (Camera.main != null) worldPos = Camera.main.ScreenToWorldPoint(screen);

            for (int i = 0; i < enemiseList.Count; i++)
            {
                float diffX = enemiseList[i].transform.position.x - worldPos.x;
                float diffY = enemiseList[i].transform.position.y - worldPos.y;

                float absDiffX = Mathf.Abs(diffX);
                float absDiffY = Mathf.Abs(diffY);

                Transform randomEnemyPos = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];

                if (enemiseList[i].gameObject.activeSelf == false)
                {
                    enemiseList[i].gameObject.SetActive(true);
                    enemiseList[i].gameObject.transform.position = randomEnemyPos.position;
                }
                if (absDiffX >= xReplaceDistance)
                {
                    enemiseList[i].gameObject.transform.position = randomEnemyPos.position;
                    enemiseList[i].RegistGrid();
                }
                if (absDiffY >= yReplaceDistance)
                {
                    enemiseList[i].gameObject.transform.position = randomEnemyPos.position;
                    enemiseList[i].RegistGrid();
                }
            }

            for (int i = 0; i < haveHotteoksKind; i++)
            {
                SpawnHotteokByLevel(i);
            }
        }

        public void AddEnemyList(int enemyKind, int count)
        {
            for (int j = 0; j < count; j++)
            {
                Transform randomEnemyPos = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];
                Enemy spawnedEnemy = enemisePool[enemyKind].Get(true);

                enemiseList.Add(spawnedEnemy);

                spawnedEnemy.transform.position = randomEnemyPos.position;
            }
        }

        public void SpawnHotteokByLevel(int hotteokName)
        {
            HotteokName type = (HotteokName)hotteokName;
            int currentLevel = hotteokLevel[hotteokName];

            for (int i = 0; i < currentLevel; i++)
            {
                // 1. 딕셔너리에 해당 호떡 리스트가 이미 존재하는지 안전하게 확인
                if (hotteokDic.TryGetValue(type, out List<Hotteok> hotteoksList))
                {
                    // 2. 인덱스 i가 현재 생성된 호떡 리스트 범위 내에 있는지 안전 검사
                    if (i < hotteoksList.Count)
                    {
                        // 리스트에 이미 존재하는 호떡이 꺼져 있다면 새로 생성하지 않고 "재사용(활성화)"합니다.
                        if (hotteoksList[i] != null && !hotteoksList[i].gameObject.activeSelf)
                        {
                            hotteoksList[i].gameObject.SetActive(true);
                        }
                    }
                    else
                    {
                        // 리스트 범위를 벗어난 경우 (레벨업으로 인해 새 호떡 슬롯이 필요한 경우)
                        AddHottoek(type, hotteoksPool[hotteokName].Get(true));
                    }
                }
                else
                {
                    // 3. 딕셔너리에 아예 리스트 자체가 없는 극초기 상태인 경우 새로 꺼내 추가합니다.
                    AddHottoek(type, hotteoksPool[hotteokName].Get(true));
                }
            }
        }


        public void AddHottoek(HotteokName hotteokName, Hotteok hotteok)
        {
            if (hotteokDic.TryGetValue(hotteokName, out List<Hotteok> hotteoksList))
            {
                // 2-A. 이미 명단이 존재한다면, 기존 명단에 이 오브젝트를 추가하기만 합니다.
                hotteoksList.Add(hotteok);
            }
            else
            {
                // 2-B. 명단이 없다면(이 방에 처음 들어왔다면), 새 명단을 만들고 등록합니다.
                List<Hotteok> newHotteoks = new List<Hotteok>();
                newHotteoks.Add(hotteok);
                hotteokDic.Add(hotteokName, newHotteoks);
            }
        }
    }
}