using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gulPortal : MonoBehaviour
{
    PlayerMovement PlayerMovement;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            SceneManager.LoadScene("Asgard", LoadSceneMode.Single);
            Debug.Log("Collision");
        }        
    }
}