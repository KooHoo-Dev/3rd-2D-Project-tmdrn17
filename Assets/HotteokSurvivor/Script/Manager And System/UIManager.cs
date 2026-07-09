using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HotteokSurvivor.Script.Manager_And_System
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip[] clickClips;

        [SerializeField] private TMP_Text currentTimeText;
        [SerializeField] private TMP_Text endTimeText;
        [SerializeField] private TMP_Text endLevelText;
        public Slider expSlider;

        private void Update()
        {
            if (Time.timeScale == 0)
            {
                return;
            }

            currentTimeText.SetText($"{GameManager.Instance.CurrentTime:F1}");
            endTimeText.SetText($"Time: {GameManager.Instance.CurrentTime:F1}");
            endLevelText.SetText($"Level: {GameManager.Instance.Player.Stat.Level}");
            expSlider.value = (float)GameManager.Instance.Player.Stat.Exp / GameManager.Instance.Player.Stat.MaxExp;
        }

        public void OnClickButton(int soundNum)
        {
            audioSource.PlayOneShot(clickClips[soundNum]);
        }

        public void SelectSkills(int hotteokName)
        {
            GameManager gameManager = GameManager.Instance;

            HotteokRotate rotateHotteok = gameManager.Player.hotteokRotate;


            switch (hotteokName)
            {
                case (int)HotteokName.Normal:

                    gameManager.PoolingSystem.hotteokLevel[hotteokName]++;
                    break;

                case (int)HotteokName.Pat:

                    gameManager.PoolingSystem.hotteokLevel[hotteokName]++;

                    if (gameManager.PoolingSystem.haveHotteoksKind < hotteokName + 1)
                    {
                        gameManager.PoolingSystem.haveHotteoksKind++;
                    }
                    break;

                case (int)HotteokName.Ddalgi:

                    if (rotateHotteok.ActiveHotteoks < rotateHotteok.DdalgiHottoeks.Count)
                    {
                        rotateHotteok.ActiveHotteoks++;
                    }

                    rotateHotteok.gameObject.SetActive(true);
                    break;
            }

            Time.timeScale = 1;
        }

        public void SceneChange(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
            Time.timeScale = 1;
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
