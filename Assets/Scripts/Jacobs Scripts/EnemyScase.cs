using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScase : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    public Animator animator;

    private float distance;
    public float distanceBetween;
    private bool facingRight = true; // Track the direction the enemy is facing
    public bool isStunned = false; // Track if the enemy is stunned

    // Start is called before the first frame update
    void Start()
    {
        // Get the SpriteRenderer component if not assigned
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStunned)
            return; // If stunned, do nothing

        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        if (distance < distanceBetween)
        {
            // Move towards the player
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            animator.SetBool("isWalking", true);

            // Check the direction of movement
            if (direction.x < 0 && facingRight)
            {
                // If moving left and currently facing right, flip the sprite
                Flip();
            }
            else if (direction.x > 0 && !facingRight)
            {
                // If moving right and currently facing left, flip the sprite
                Flip();
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
    

    // Function to flip the sprite
    void Flip()
    {
        facingRight = !facingRight; // Toggle the facing direction

        // Flip the sprite by flipping its scale on the X-axis
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}