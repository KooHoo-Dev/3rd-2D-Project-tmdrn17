using UnityEngine;
using Study.Utilities;
using System.Collections.Generic;

namespace HotteokSurvivor
{
    public class HotteokPoolSystem : MonoBehaviour
    {
        [SerializeField] private Transform hotteokMuzzle;

        [SerializeField] private NormalHotteok normalHotteok;
        [SerializeField] private PatHptteok patHotteok;
        [SerializeField] private Enemy[] enemise;

        private ComponentPool<NormalHotteok> normalPool;
        private ComponentPool<PatHptteok> patPool;
        private ComponentPool<Enemy>[] enemisePool;

        [SerializeField] List<NormalHotteok> normalHotteoks = new List<NormalHotteok>();
        [SerializeField] List<PatHptteok> patHotteoks = new List<PatHptteok>();
        [SerializeField] List<Enemy> enemiseList = new List<Enemy>();

        private void Start()
        {
            enemisePool=new ComponentPool<Enemy>[enemise.Length];

            normalPool = new ComponentPool<NormalHotteok>(normalHotteok, hotteokMuzzle);
            patPool = new ComponentPool<PatHptteok>(patHotteok, hotteokMuzzle);

            for(int i = 0; i < enemise.Length; i++)
            {
                enemisePool[i] = new ComponentPool<Enemy>(enemise[i]);
                enemiseList.Add(enemisePool[i].Get());
            }

            normalHotteoks.Add(normalPool.Get());
            patHotteoks.Add(patPool.Get());
        }

        private void Update()
        {
            for(int i = 0; i < patHotteoks.Count; ++i)
            {
                if (patHotteoks[i].gameObject.activeSelf == false)
                {
                    patPool.Get();
                }
            }
            for (int i = 0; i < normalHotteoks.Count; ++i)
            {
                if (normalHotteoks[i].gameObject.activeSelf == false)
                {
                    normalPool.Get();
                }
            }
            for (int i = 0; i < enemiseList.Count; ++i)
            {
                if (enemiseList[i].gameObject.activeSelf == false)
                {
                    enemisePool[i].Get();
                }
            }
        }
    }
}