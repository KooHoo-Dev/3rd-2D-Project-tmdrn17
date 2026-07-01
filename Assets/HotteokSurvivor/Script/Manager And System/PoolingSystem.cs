using UnityEngine;
using Study.Utilities;
using System.Collections.Generic;

namespace HotteokSurvivor
{
    public class PoolingSystem : SingletonBase<PoolingSystem>
    {
        public int enemyCount;

        [SerializeField] private Transform hotteokMuzzle;
        [SerializeField] private Transform[] enemySpawnPoints;

        [SerializeField] private Enemy[] enemise;
        [SerializeField] private Hotteok[] hotteoks;
        [SerializeField] private Money money;

        private ComponentPool<Enemy>[] enemisePool;
        private ComponentPool<Hotteok>[] hotteoksPool;
        public ComponentPool<Money> moneyPool;

        [SerializeField] private List<Enemy> enemiseList = new List<Enemy>();
        [SerializeField] private List<Hotteok> hotteoksList = new List<Hotteok>();
        [SerializeField] private List<Money> moneyList = new List<Money>();

        private void Start()
        {
            enemisePool = new ComponentPool<Enemy>[enemise.Length];
            hotteoksPool = new ComponentPool<Hotteok>[hotteoks.Length];

            for (int i = 0; i < enemise.Length; i++)
            {
                enemisePool[i] = new ComponentPool<Enemy>(enemise[i]);

                for (int j = 0; j < enemyCount; j++)
                {
                    Transform randomEnemyPos = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];
                    Enemy spawnedEnemy = enemisePool[i].Get(true);

                    enemiseList.Add(spawnedEnemy);

                    spawnedEnemy.transform.position = randomEnemyPos.position;
                }
            }

            for (int i = 0; i < hotteoks.Length; i++)
            {
                hotteoksPool[i] = new ComponentPool<Hotteok>(hotteoks[i], hotteokMuzzle);
                hotteoksList.Add(hotteoksPool[i].Get(true));
            }

            moneyPool = new ComponentPool<Money>(money);
        }

        private void Update()
        {
            for(int i=0; i<enemiseList.Count; i++)
            {
                if(enemiseList[i].gameObject.activeSelf == false)
                {
                    Transform randomEnemyPos = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];
                    enemiseList[i].gameObject.SetActive(true);
                    enemiseList[i].gameObject.transform.position = randomEnemyPos.position;
                }
            }

            for (int i = 0; i < hotteoksList.Count; ++i)
            {
                if (hotteoksList[i].gameObject.activeSelf == false)
                {
                    hotteoksPool[i].Get(true);
                }
            }
        }
    }
}