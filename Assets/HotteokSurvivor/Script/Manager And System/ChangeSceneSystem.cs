using UnityEngine;
using UnityEngine.SceneManagement;

namespace HotteokSurvivor
{
    public class ChangeSceneSystem : MonoBehaviour
    {
       [field: SerializeField] public SceneManager[] sceneManager {  get; private set; }
    }
}