using UnityEngine;
using UnityEngine.InputSystem;
using Study.Utilities;

public class PlayerMove : MonoBehaviour
{
    [field: SerializeField] private float moveSpeed { get; set; } = 1.0f;
    private int FacingDirection { get; set; } = 1;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 inputVector = SimpleInput.GetMoveAxisRaw();
        float absMovement = Mathf.Abs(inputVector.x);

        if (absMovement > 0)
        {
            float currentScaleX = Mathf.Abs(transform.localScale.x);
            FacingDirection = (inputVector.x < 0) ? -1 : 1;
            transform.localScale = new Vector3
                (currentScaleX * FacingDirection, transform.localScale.y, transform.localScale.z);
        }

        if (inputVector != Vector2.zero)
        {
            Vector3 moveVec = inputVector;

            transform.Translate(moveVec.normalized * moveSpeed * Time.deltaTime);
        }
    }
}
