using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FootstepSound : MonoBehaviour
{
    public EventReference footstepEvent; // Reference to the FMOD footstep event
    public float stepInterval = 0.5f; // Time interval between footsteps
    private bool isWalking = false;
    private Coroutine footstepCoroutine;

    void Start()
    {
        // Start playing footstep sounds if the character is moving
        footstepCoroutine = StartCoroutine(PlayFootsteps());
    }

    void Update()
    {
        // Check if the character is moving
        isWalking = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
    }

    private IEnumerator PlayFootsteps()
    {
        while (true)
        {
            if (isWalking)
            {
                // Play the footstep sound using FMOD
                RuntimeManager.PlayOneShot(footstepEvent);
                yield return new WaitForSeconds(stepInterval);
            }
            else
            {
                yield return null;
            }
        }
    }
}
