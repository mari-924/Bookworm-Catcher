using UnityEngine;

public class WormPatrol : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private float obstacleRaycastDistance = 2f;

    [SerializeField] private bool movingRight = true; // This is a serialize field so the starting direction can be changed from the editor.

    [SerializeField] private Transform groundDetection;

    private void Start()
    {
        // Start worm facing left if movingRight is disabled in the editor when the game starts
        if (!movingRight)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
    }

    private void Update()
    {
        // Moves worm
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Checks for when ground ends
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, obstacleRaycastDistance);
        RaycastHit2D pathAheadInfo = Physics2D.Raycast(groundDetection.position, Vector2.right, obstacleRaycastDistance);

        // Changes direction when ground ends
        if (groundInfo.collider == false)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            } else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }

        // Changes direction when it detects a barrier ahead
        if (pathAheadInfo.collider != false)
        {
            if (pathAheadInfo.collider.CompareTag("Barrier"))
            {
                if (movingRight)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                } else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                }
            }
        }
    }
}
