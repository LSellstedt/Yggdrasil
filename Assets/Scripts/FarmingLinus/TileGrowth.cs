using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGrowth : MonoBehaviour
{
    public TileBase startingTileA;
    public TileBase startingTileB;
    public TileBase stageOneTileA;
    public TileBase stageTwoTileA;
    public TileBase finalStageTileA;
    public TileBase stageOneTileB;
    public TileBase stageTwoTileB;
    public TileBase finalStageTileB;

    public TileBase tileX; // Assign this in the inspector
    public TileBase tileY; // Assign this in the inspector

    private Tilemap tilemap;
    private Dictionary<Vector3Int, Coroutine> tileCoroutines = new Dictionary<Vector3Int, Coroutine>();
    private Dictionary<TileBase, int> tileScores = new Dictionary<TileBase, int>();

    private bool isPlantingModeA = true;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();

        // Initialize the scores dictionary
        tileScores[tileX] = 0;
        tileScores[tileY] = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            isPlantingModeA = !isPlantingModeA;
            Debug.Log("Planting mode switched. Current mode: " + (isPlantingModeA ? "A" : "B"));
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tilePosition = tilemap.WorldToCell(mouseWorldPos);

            TileBase clickedTile = tilemap.GetTile(tilePosition);
            if (clickedTile == startingTileA || clickedTile == startingTileB)
            {
                // Start the growth process
                if (tileCoroutines.ContainsKey(tilePosition))
                {
                    StopCoroutine(tileCoroutines[tilePosition]);
                }
                Coroutine growthCoroutine = StartCoroutine(GrowTile(tilePosition, isPlantingModeA));
                tileCoroutines[tilePosition] = growthCoroutine;
            }
            else if (clickedTile == finalStageTileA || clickedTile == finalStageTileB)
            {
                // Update score based on the type of the original tile
                TileBase originalTile = tilemap.GetTile(tilePosition); // Assuming you stored original tiles somewhere

                if (originalTile == tileX)
                {
                    tileScores[tileX]++;
                    Debug.Log("Tile X score: " + tileScores[tileX]);
                }
                else if (originalTile == tileY)
                {
                    tileScores[tileY]++;
                    Debug.Log("Tile Y score: " + tileScores[tileY]);
                }

                // Revert to the appropriate starting tile
                tilemap.SetTile(tilePosition, startingTileA); // Change this to the appropriate starting tile if needed
                if (tileCoroutines.ContainsKey(tilePosition))
                {
                    StopCoroutine(tileCoroutines[tilePosition]);
                    tileCoroutines.Remove(tilePosition);
                }
            }
        }
    }

    private IEnumerator GrowTile(Vector3Int tilePosition, bool isModeA)
    {
        if (isModeA)
        {
            tilemap.SetTile(tilePosition, stageOneTileA);
        }
        else
        {
            tilemap.SetTile(tilePosition, stageOneTileB);
        }
        yield return new WaitForSeconds(2.0f); // Time between stages

        if (isModeA)
        {
            tilemap.SetTile(tilePosition, stageTwoTileA);
        }
        else
        {
            tilemap.SetTile(tilePosition, stageTwoTileB);
        }
        yield return new WaitForSeconds(2.0f);

        if (isModeA)
        {
            tilemap.SetTile(tilePosition, finalStageTileA);
        }
        else
        {
            tilemap.SetTile(tilePosition, finalStageTileB);
        }
        tileCoroutines.Remove(tilePosition);
    }
}
