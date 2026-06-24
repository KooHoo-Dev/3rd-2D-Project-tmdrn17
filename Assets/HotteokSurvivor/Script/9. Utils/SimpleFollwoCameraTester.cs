using UnityEngine;
using UnityEngine.InputSystem;

namespace Study.Utilities
{
    [RequireComponent(typeof(SimpleFollwoCamera))]
    public class SimpleFollwoCameraTester : MonoBehaviour
    {
        private SimpleFollwoCamera follwCam;

        private void Awake()
        {
            follwCam = GetComponent<SimpleFollwoCamera>();
        }

        private void Update()
        {
            if (SimpleInput.GetKeyDown(Key.F1))
            {
                follwCam.ChangeState(SimpleFollwoCamera.States.Stopped);
            }
            else if(SimpleInput.GetKeyDown(Key.F2))
            {
                follwCam.ChangeState(SimpleFollwoCamera.States.Holding);
            }
            else if (SimpleInput.GetKeyDown(Key.F3))
            {
                //follwCam.Target = Player.LocalPlayer.transform;
                follwCam.ChangeState(SimpleFollwoCamera.States.Following);
            }
            else if (SimpleInput.GetKeyDown(Key.F4))
            {
                follwCam.ChangeState(SimpleFollwoCamera.States.Following);
            }
        }
    }
}
