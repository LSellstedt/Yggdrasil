using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player movement

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 lastMovementDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the input from the player
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Check if there is any movement input
        if (movement != Vector2.zero)
        {
            lastMovementDirection = movement;

            // Face the direction of movement
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {
                // Horizontal movement
                if (movement.x > 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0); // Facing right
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 180); // Facing left
                }
            }
            else
            {
                // Vertical movement
                if (movement.y > 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 90); // Facing up
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, -90); // Facing down
                }
            }
        }
    }

    // FixedUpdate is called at a fixed interval and is used for physics calculations
    void FixedUpdate()
    {
        // Move the player
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}