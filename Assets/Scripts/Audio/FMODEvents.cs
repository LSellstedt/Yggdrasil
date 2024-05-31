
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{    
    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference loseLife { get; private set; }
    [field: SerializeField] public EventReference unlifed { get; private set; }
    [field: SerializeField] public EventReference footsteps { get; private set; }
    [field: SerializeField] public EventReference lifeup { get; private set; }

    [field: Header("Farm SFX")]
    [field: SerializeField] public EventReference plantplaced { get; private set; }
    [field: SerializeField] public EventReference plantharvest { get; private set; }
   
    public static FMODEvents instance {  get; private set; }

    private void Awake()
    {
          if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance in the scene.");
        } 
        instance = this;
    }

}
