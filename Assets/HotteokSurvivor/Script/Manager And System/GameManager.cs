using UnityEngine;
using Study.Utilities;

namespace HotteokSurvivor
{
    public class GameManager : SingletonBase<GameManager>
    {
        public Player Player;
        public UIManager UIManager;

        public void LevelUpPopUp()
        {
            UIManager.UpgradeUI.SetActive(true);
            Time.timeScale = 0;
        }
    }
}