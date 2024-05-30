using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System;

public class AudioManager : MonoBehaviour
{
    private EventInstance musicEventInstance;
    public static AudioManager instance { get; private set; }

    private void Awake()
    {
            if (instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in the scene.");
        }
        instance = this;
    }
   /* private void Start()
    {
        InitializeMusic(FMODEvents.instance.music);
    }


    private void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicEventInstance = CreateEventInstance(musicEventReference);
        musicEventInstance.start();
        
        PLAYBACK_STATE playbackState;
        musicEventInstance.getPlaybackState(out playbackState);

        if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
        {
            musicEventInstance = CreateEventInstance(musicEventReference);
            musicEventInstance.start();
        }
        else
        {
            musicEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }     
        
    }
   */ 

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
            EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
            return eventInstance;
    }
}
