using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    public static Vector3 playerSpawnPosition;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
