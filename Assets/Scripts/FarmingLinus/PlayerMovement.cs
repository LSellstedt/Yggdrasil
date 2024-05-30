/*using UnityEngine;
using FMOD.Studio;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this value to change the speed of the player
    public EventInstance footsteps;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 temp;
    private Scene oldscene;
    private float gulPortalposx;
    private float gulPortalposy;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        temp = rb.position;
        footsteps = AudioManager.instance.CreateEventInstance(FMODEvents.instance.footsteps);
        oldscene = SceneManager.GetActiveScene();
        gulPortalposx = 23.7f; 
        gulPortalposy = 10.75f;

        gulPortalposx = 23.7f;
        gulPortalposy = 10.75f;

        gulPortalposx = 23.7f;
        gulPortalposy = 10.75f;

        gulPortalposx = 23.7f;
        gulPortalposy = 10.75f;

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
        Debug.LogError(Vector2.Distance(rb.position,new Vector2(gulPortalposx, gulPortalposy)));
        if (temp == rb.position)
        {
            footsteps.stop(STOP_MODE.ALLOWFADEOUT);
        }
        else // ur mom :3
        {  
            if (Vector2.Distance(new Vector2 (gulPortalposx, gulPortalposy),rb.position)<47)
            {

            
                PLAYBACK_STATE playbackState;
                footsteps.getPlaybackState(out playbackState);

                if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
                {
                    footsteps.start();
                }
            }
            else
            {
                footsteps.stop(STOP_MODE.ALLOWFADEOUT);
            }
        }
        temp = rb.position;
        if (oldscene != SceneManager.GetActiveScene())
        {
            footsteps.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }   
}
*/
using UnityEngine;
using FMOD.Studio;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this value to change the speed of the player
    public EventInstance footsteps;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 temp;
    private Scene oldScene;
    private Vector2 gulPortalPosition = new Vector2(-23.7f, 10.75f);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        temp = rb.position;
        footsteps = AudioManager.instance.CreateEventInstance(FMODEvents.instance.footsteps);
        oldScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        // Get input from the player
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Create a vector from the input
        movement = new Vector2(moveX, moveY).normalized;
    }

    void FixedUpdate()
    {
        // Move the player using the Rigidbody2D component
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        UpdateSound();
    }

    private void UpdateSound()
    {
        float distanceToPortal = Vector2.Distance(rb.position, gulPortalPosition);
        Debug.Log($"Player Position: {rb.position}, Portal Position: {gulPortalPosition}, Distance to Portal: {distanceToPortal}");

        if (temp == rb.position)
        {
            footsteps.stop(STOP_MODE.ALLOWFADEOUT);
        }
        else
        {
            if (distanceToPortal > 1.6f)
            {
                PLAYBACK_STATE playbackState;
                footsteps.getPlaybackState(out playbackState);

                if (playbackState == PLAYBACK_STATE.STOPPED)
                {
                    footsteps.start();
                }
            }
            else
            {
                footsteps.stop(STOP_MODE.ALLOWFADEOUT);
            }
        }
        temp = rb.position;

        if (oldScene != SceneManager.GetActiveScene())
        {
            footsteps.stop(STOP_MODE.ALLOWFADEOUT);
            oldScene = SceneManager.GetActiveScene(); // Update oldScene to the current scene
        }
    }
}
