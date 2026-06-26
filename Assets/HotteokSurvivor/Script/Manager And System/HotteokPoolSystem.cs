using UnityEngine;
using Study.Utilities;
using System.Collections.Generic;

namespace HotteokSurvivor
{
    public class HotteokPoolSystem : MonoBehaviour
    {
        [SerializeField] Transform hotteokMuzzle;

        [SerializeField] private NormalHotteok normalHotteok;
        [SerializeField] private PatHptteok patHotteok;

        private ComponentPool<NormalHotteok> normalPool;
        private ComponentPool<PatHptteok> patPool;

        [SerializeField] List<NormalHotteok> normalHotteoks = new List<NormalHotteok>();
        [SerializeField] List<PatHptteok> patHotteoks = new List<PatHptteok>();

        private void Start()
        {
            patPool = new ComponentPool<PatHptteok>(patHotteok, hotteokMuzzle);

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
        }
    }
}