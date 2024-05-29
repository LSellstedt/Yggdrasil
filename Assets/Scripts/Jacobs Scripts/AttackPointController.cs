using UnityEngine;

public class AttackPointController : MonoBehaviour
{
    public Transform attackPoint; // Drag your attack point here in the inspector
    public float attackPointDistance = 1.0f; // Distance in front of the player

    void Update()
    {
        // Calculate the direction the player is facing
        Vector3 direction = transform.right; // Assuming the player faces right by default

        // If using a 2D game, adjust based on horizontal and vertical movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
        {
            // Player is moving horizontally
            direction = horizontal > 0 ? Vector3.right : Vector3.left;
        }
        else
        {
            // Player is moving vertically
            direction = vertical > 0 ? Vector3.up : Vector3.down;
        }

        // Update the attack point position
        attackPoint.position = transform.position + direction * attackPointDistance;
    }
}