using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGrowth : MonoBehaviour
{
    public TileBase startingTileA;
    public TileBase startingTileB;
    public TileBase stageOneTile;
    public TileBase stageTwoTile;
    public TileBase finalStageTile;

    public TileBase tileX; // Assign this in the inspector
    public TileBase tileY; // Assign this in the inspector

    private Tilemap tilemap;
    private Dictionary<Vector3Int, Coroutine> tileCoroutines = new Dictionary<Vector3Int, Coroutine>();
    private Dictionary<TileBase, int> tileScores = new Dictionary<TileBase, int>();

    void Start()
    {
        tilemap = GetComponent<Tilemap>();

        // Initialize the scores dictionary
        tileScores[tileX] = 0;
        tileScores[tileY] = 0;
    }

    void Update()
    {
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
                Coroutine growthCoroutine = StartCoroutine(GrowTile(tilePosition));
                tileCoroutines[tilePosition] = growthCoroutine;
            }
            else if (clickedTile == finalStageTile)
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

                // Revert to the starting tile
                tilemap.SetTile(tilePosition, startingTileA); // Change this to the appropriate starting tile
                if (tileCoroutines.ContainsKey(tilePosition))
                {
                    StopCoroutine(tileCoroutines[tilePosition]);
                    tileCoroutines.Remove(tilePosition);
                }
            }
        }
    }

    private IEnumerator GrowTile(Vector3Int tilePosition)
    {
        tilemap.SetTile(tilePosition, stageOneTile);
        yield return new WaitForSeconds(2.0f); // Time between stages

        tilemap.SetTile(tilePosition, stageTwoTile);
        yield return new WaitForSeconds(2.0f);

        tilemap.SetTile(tilePosition, finalStageTile);
        tileCoroutines.Remove(tilePosition);
    }
}
