using UnityEngine;
using UnityEngine.SceneManagement;

namespace HotteokSurvivor
{
    public class ChangeSceneSystem : MonoBehaviour
    {
        public void SceneChange()
        {
            SceneManager.LoadScene("Game Scene");
        }
    }
}