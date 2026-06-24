using Study.Utilities;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [field: SerializeField] private float MoveSpeed { get; set; } = 1.0f;
    [field: SerializeField] private GameObject Target { get; set; }
    private int FacingDirection { get; set; } = 1;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        const float STOPPING_DISTANCE = 0.1f;

        Vector2 inputVector = Target.transform.position - transform.position;
        Vector3 localscale = transform.localScale;

        float absMovement = Mathf.Abs(inputVector.x);
        float currentScaleX = Mathf.Abs(localscale.x);

        if (inputVector != Vector2.zero)
        {
            if (transform.IsInRange(Target.transform.position, STOPPING_DISTANCE))
            {
                return;
            }
            else
            {
                Vector3 moveVec = inputVector;

                transform.Translate(moveVec.normalized * MoveSpeed * Time.deltaTime);
            }
        }

        if (absMovement > 0)
        {
            if(transform.IsInRange(Target.transform.position, STOPPING_DISTANCE))
            {
                transform.localScale = new Vector3
                    (currentScaleX * FacingDirection, localscale.y, localscale.z);
            }
            else
            {
                FacingDirection = (inputVector.x < 0) ? -1 : 1;
                transform.localScale = new Vector3
                    (currentScaleX * FacingDirection, localscale.y, localscale.z);
            }
        }
    }
}
