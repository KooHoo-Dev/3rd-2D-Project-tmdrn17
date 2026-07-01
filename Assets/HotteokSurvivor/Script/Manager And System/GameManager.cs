using UnityEngine;
using Study.Utilities;

namespace HotteokSurvivor
{
    public class GameManager : SingletonBase<GameManager>
    {
        public Player Player;
        public GameObject UpgradeUI;

        public void LevelUpPopUp()
        {
            UpgradeUI.SetActive(true);
            Time.timeScale = 0;
        }
    }
}