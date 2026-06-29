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
        [SerializeField] private DdalgiHotteok ddalgiHotteok;

        private ComponentPool<NormalHotteok> normalPool;
        private ComponentPool<PatHptteok> patPool;
        private ComponentPool<DdalgiHotteok> ddalgiPool;

        [SerializeField] List<NormalHotteok> normalHotteoks = new List<NormalHotteok>();
        [SerializeField] List<PatHptteok> patHotteoks = new List<PatHptteok>();
        [SerializeField] List<DdalgiHotteok> ddalgiHotteoks = new List<DdalgiHotteok>();

        private void Start()
        {
            normalPool = new ComponentPool<NormalHotteok>(normalHotteok, hotteokMuzzle);
            patPool = new ComponentPool<PatHptteok>(patHotteok, hotteokMuzzle);

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
        }
    }
}