using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.right;
    public float moveDistance = 3f;
    public float moveSpeed = 2f;

    private Vector3 startPosition;
    private bool movingForward = true;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float move = moveSpeed * Time.deltaTime;
        if (movingForward)
            transform.Translate(moveDirection * move);
        else
            transform.Translate(-moveDirection * move);

        if (Vector3.Distance(transform.position, startPosition) >= moveDistance)
        {
            movingForward = !movingForward;
        }
    }

}
