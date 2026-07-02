using UnityEngine;
using Study.Utilities;

namespace HotteokSurvivor
{
    public class GameManager : SingletonBase<GameManager>
    {
        public Player Player;
        public GameObject UpgradeUI;
        public PoolingSystem poolingSystem;

        private void Start()
        {
            poolingSystem = PoolingSystem.instance;
        }

        public void LevelUpPopUp()
        {
            UpgradeUI.SetActive(true);
            Time.timeScale = 0;
        }

        public void SelectSkills(int hotteokName)
        {
            switch (hotteokName)
            {
                case (int)HotteokName.Normal:
                    poolingSystem.hotteokLevel[hotteokName]++;
                    Debug.Log("normal");
                    break;
                case (int)HotteokName.Pat:
                    poolingSystem.hotteokLevel[hotteokName]++;
                    if (poolingSystem.haveHotteoksKind < hotteokName + 1)
                    {
                        poolingSystem.haveHotteoksKind++;
                    }
                    Debug.Log("pat");
                    break;
                case (int)HotteokName.Ddalgi:
                    poolingSystem.hotteokLevel[hotteokName]++;
                    Debug.Log("ddalgi");
                    break;
            }

            UpgradeUI.SetActive(false);
            Time.timeScale = 1;
        }
    }
}