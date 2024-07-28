using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class grönPortal : MonoBehaviour
{
    public Vector3 spawnPosition; // Set this in the Inspector to the desired spawn position

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            // Set the spawn position before loading the new scene
            Spawnpoint.playerSpawnPosition = spawnPosition;

            // Load the new scene
            SceneManager.LoadScene("JosefStinky", LoadSceneMode.Single);
            Debug.Log("Collision");
        }
    }
}
