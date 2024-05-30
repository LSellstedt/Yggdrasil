using UnityEngine;
using FMOD.Studio;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player movement

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    //audio jangel
    private Vector2 oldpos;
    private EventInstance footsteps;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


     void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
           animator.SetFloat("Speed", movement.sqrMagnitude);
    
        UpdateSound();
     }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position +  movement * moveSpeed * Time.fixedDeltaTime);
    }
    private void UpdateSound()
    {
        if (oldpos != rb.position)
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
        oldpos = rb.position;

    }
}