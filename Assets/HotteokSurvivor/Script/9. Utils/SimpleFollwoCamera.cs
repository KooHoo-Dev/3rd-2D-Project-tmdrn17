using Unity.VisualScripting;
using UnityEngine;

namespace Study.Utilities
{
    // 타겟을 부드럽게(Lerp) 따라가는 범용 추적 카메라

    public class SimpleFollwoCamera : MonoBehaviour
    {
        public enum States
        {
            Stopped,
            Holding,
            Following
        }

        [field: SerializeField] public Transform Target { get; set; }
        [SerializeField] private float lerpSpeed;
        [SerializeField] private Vector3 offset;


        private Camera Cam { get; set; }
        [field: SerializeField] private States State { get; set; }

        private void Start()
        {
            // 현재 씬의 MainCamera(Tag)를 가져온다
            Cam = Camera.main;
        }

        private void Update()
        {

            switch (State)
            {
                case States.Stopped:
                    Target = null;
                    break;
                case States.Holding:

                    break;
                case States.Following:
                    // 아래의 보간 내용:
                    // transform.position을 Target.position + offset으로 초당 lerpSpeed의 속도로
                    // 계속 가깝게 만드는 코드 Target.position이 멀다면 더 빨리, 가깝다면 느리게
                    Vector3 lerpPos = Vector3.Lerp
                        (transform.position, Target.position + offset, lerpSpeed * Time.deltaTime);
                    lerpPos.z = transform.position.z;
                    transform.position = lerpPos;
                    break;
            }
        }

        public void SetTarget(Transform target)
        {
            Target = target;
        }

        public void ChangeState(States states)
        {
            if (states == States.Following && Target == null)
            {
                Debug.LogWarning("SimpleFollowCamera :: Target이 없어 Following 상태로 전환할 수 없습니다.");
                return;
            }

            State = states;
        }

    }
}
