using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab; // Reference to the player prefab
    private GameObject playerInstance;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate player at the specified spawn position
        playerInstance = Instantiate(playerPrefab, Spawnpoint.playerSpawnPosition, Quaternion.identity);
    }
}
