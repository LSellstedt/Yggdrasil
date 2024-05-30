
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    //if stops work enable
   /* [field: Header("Music")]
    [field: SerializeField] public EventReference music {  get; private set; }*/
    
    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference footsteps { get; private set; }
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
