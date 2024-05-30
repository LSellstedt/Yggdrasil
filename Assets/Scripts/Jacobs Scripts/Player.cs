using UnityEngine;
using FMOD.Studio;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player movement

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    //audio jangel
    private Vector2 oldpos;
    private EventInstance footsteps;

    //portals
    private List<Vector2> portals = new List<Vector2>();
    private Vector2 asgardPortalInPosition = new Vector2(-23.7f, 10.75f);
    private Vector2 asgardPortalOutPosition = new Vector2(-357.72f, 358.04f);

    private Vector2 milfPortalInPosition = new Vector2(-33.67f, 5.81f);
    private Vector2 milfPortalOutPosition = new Vector2(638.8368f, 950.3328f);

    private Vector2 muspPortalInPosition = new Vector2(-33.77f, -6.32f);
    private Vector2 muspPortalOutPosition = new Vector2(637.2997f, 3.9f);

    private Vector2 helPortalInPosition = new Vector2(-23.74f, -12.09f);
    private Vector2 helPortalOutPosition = new Vector2(4.03f, 1266.18f);


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //ljud :3
        oldpos = rb.position;
        footsteps = AudioManager.instance.CreateEventInstance(FMODEvents.instance.footsteps);

        portals.Add(asgardPortalInPosition);
        portals.Add(asgardPortalOutPosition);

        portals.Add(milfPortalInPosition);
        portals.Add(milfPortalOutPosition);

        portals.Add(muspPortalInPosition);
        portals.Add(muspPortalOutPosition);

        portals.Add(helPortalInPosition);
        portals.Add(helPortalOutPosition);
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
        float distanceToPortal = Vector2.Distance(rb.position, asgardPortalOutPosition);
        Debug.Log($"Player Position: {rb.position}, Portal Position: {asgardPortalOutPosition}, Distance to Portal: {distanceToPortal}");

        if (oldpos == rb.position)
        {
            footsteps.stop(STOP_MODE.ALLOWFADEOUT);
        }
        else
        {
            for (int i = 0; i < 8; i++)
            {
                if (Vector2.Distance(rb.position, portals[i]) > 1.6f)
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
        }
        oldpos = rb.position;
    }
}