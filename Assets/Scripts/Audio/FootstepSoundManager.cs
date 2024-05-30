using System.Collections;
using UnityEngine;
using FMODUnity;
using UnityEngine.SceneManagement;

public class FootstepManager : MonoBehaviour
{
    public static FootstepManager Instance;

    public EventReference footstepEvent;
    public float stepInterval = 0.5f;

    private bool isWalking = false;
    private Coroutine footstepCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        bool wasWalking = isWalking;
        isWalking = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;

        if (isWalking && !wasWalking)
        {
            // Start the coroutine if the character starts walking
            footstepCoroutine = StartCoroutine(PlayFootsteps());
        }
        else if (!isWalking && wasWalking)
        {
            // Stop the coroutine if the character stops walking
            if (footstepCoroutine != null)
            {
                StopCoroutine(footstepCoroutine);
                footstepCoroutine = null;
            }
        }
    }

    private IEnumerator PlayFootsteps()
    {
        while (isWalking)
        {
            // Play the footstep sound using FMOD
            RuntimeManager.PlayOneShot(footstepEvent);
            yield return new WaitForSeconds(stepInterval);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Stop footstep sounds when a new scene is loaded
        if (footstepCoroutine != null)
        {
            StopCoroutine(footstepCoroutine);
            footstepCoroutine = null;
        }
        isWalking = false;  // Reset walking state
    }
}
