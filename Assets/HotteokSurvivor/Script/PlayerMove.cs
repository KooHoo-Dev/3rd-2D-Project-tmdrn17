using UnityEngine;
using UnityEngine.InputSystem;
using Study.Utilities;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.0f;

    private void Start()
    {

    }

    private void Update()
    {
        if (SimpleInput.GetKey(Key.RightArrow))
        {
            Move(Vector3.right);
        }
        if (SimpleInput.GetKey(Key.LeftArrow))
        {
            Move(Vector3.left);
        }
        if (SimpleInput.GetKey(Key.UpArrow))
        {
            Move(Vector3.up);
        }
        if (SimpleInput.GetKey(Key.DownArrow))
        {
            Move(Vector3.down);
        }
    }

    private void Move(Vector3 vector3)
    {
        Vector3 moveVec = Vector3.zero;

        moveVec += vector3;
        transform.Translate(moveVec.normalized * moveSpeed * Time.deltaTime);
    }
}
