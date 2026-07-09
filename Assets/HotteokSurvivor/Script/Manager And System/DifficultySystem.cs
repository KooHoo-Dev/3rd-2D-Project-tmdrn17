using UnityEngine;

namespace HotteokSurvivor.Script.Manager_And_System
{
    public class DifficultySystem : MonoBehaviour
    {
        [SerializeField] private float difficultyTime = 60f;
        [SerializeField] private float curTime = 0f;
        [SerializeField] private int enemyPoolLevel = 0;

        [SerializeField] private int[] enemyKind;
        [SerializeField] private int[] enemyCount;

        private void Update()
        {
            if (GameManager.Instance.CurrentTime >= difficultyTime && curTime <= difficultyTime)
            {
                PoolingSystem.Instance.AddEnemyList(enemyKind[enemyPoolLevel], enemyCount[enemyPoolLevel]);
                
                for(int i=0;i< PoolingSystem.Instance.enemiseList.Count; i++)
                {
                    PoolingSystem.Instance.enemiseList[i].Stat.MaxHp += 50;
                    PoolingSystem.Instance.enemiseList[i].Stat.DefensivePower += 1;
                    PoolingSystem.Instance.enemiseList[i].Stat.Damage += 1;
                }

                enemyPoolLevel++;
                curTime = GameManager.Instance.CurrentTime;
                difficultyTime += 30f;
            }
        }
    }
}