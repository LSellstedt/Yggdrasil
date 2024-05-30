using UnityEngine;
using FMOD.Studio;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this value to change the speed of the player
    private EventInstance footsteps;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 temp;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        temp = rb.position;
        footsteps = AudioManager.instance.CreateEventInstance(FMODEvents.instance.footsteps);
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from the player
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Create a vector from the input
        movement = new Vector2(moveX, moveY).normalized;
    }

    // FixedUpdate is called a fixed number of times per frame
    void FixedUpdate()
    {
        // Move the player using the Rigidbody2D component
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        UpdateSound(); 
    }

    private void UpdateSound()
    {
        if (temp != rb.position)
        {
            PLAYBACK_STATE playbackState;
            footsteps.getPlaybackState(out playbackState);

            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                footsteps.start();
            }
        }
        else // ur mom :3
        {
            footsteps.stop(STOP_MODE.ALLOWFADEOUT);
        }
        temp = rb.position;

    }   
}

