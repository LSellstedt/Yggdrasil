using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using FMODUnity;

public class TileGrowth : MonoBehaviour
{
    [SerializeField] private EventReference plantplacedSound;
    [SerializeField] private EventReference plantharvestSound;
    public TileBase startingTileA;
    public TileBase startingTileB;

    public TileBase stageOneTileA;
    public TileBase stageTwoTileA;
    public TileBase finalStageTileA;

    public TileBase stageOneTileB;
    public TileBase stageTwoTileB;
    public TileBase finalStageTileB;

    public TileBase stageOneTileC;
    public TileBase stageTwoTileC;
    public TileBase finalStageTileC;

    public TileBase tileX; // Assign this in the inspector
    public TileBase tileY; // Assign this in the inspector

    private Tilemap tilemap;
    private Dictionary<Vector3Int, Coroutine> tileCoroutines = new Dictionary<Vector3Int, Coroutine>();
    private Dictionary<TileBase, int> tileScores = new Dictionary<TileBase, int>();

    private int plantingMode = 0; // 0 for A, 1 for B, 2 for C

    void Start()
    {
        tilemap = GetComponent<Tilemap>();

        // Initialize the scores dictionary
        tileScores[finalStageTileA] = 0;
        tileScores[finalStageTileB] = 0;
        tileScores[finalStageTileC] = 0;

        // Logging to verify initialization
        Debug.Log("Initialized tileScores dictionary with final stage tiles.");
    }

    void Update()
    {
        // Switch planting modes when 'X' is pressed
        if (Input.GetKeyDown(KeyCode.X))
        {
            plantingMode = (plantingMode + 1) % 3;
            Debug.Log("Switched planting mode to: " + (plantingMode == 0 ? "Mode A" : plantingMode == 1 ? "Mode B" : "Mode C"));
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tilePosition = tilemap.WorldToCell(mouseWorldPos);

            TileBase clickedTile = tilemap.GetTile(tilePosition);

            // Logging to see which tile was clicked
            Debug.Log("Clicked tile: " + (clickedTile != null ? clickedTile.name : "null"));

            if (clickedTile == startingTileA || clickedTile == startingTileB)
            {
                //AudioManager.instance.PlayOneShot(plantplacedSound, this.transform.position);
                // Start the growth process
                if (tileCoroutines.ContainsKey(tilePosition))
                {
                    StopCoroutine(tileCoroutines[tilePosition]);
                }
                Coroutine growthCoroutine = StartCoroutine(GrowTile(tilePosition, plantingMode));
                tileCoroutines[tilePosition] = growthCoroutine;
            }
            else if (clickedTile == finalStageTileA || clickedTile == finalStageTileB || clickedTile == finalStageTileC)
            {
                //AudioManager.instance.PlayOneShot(plantharvestSound, this.transform.position);
                // Update score based on the type of the final stage tile
                if (!tileScores.ContainsKey(clickedTile))
                {
                    Debug.LogError("Tile not found in scores dictionary: " + (clickedTile != null ? clickedTile.name : "null"));
                    return;
                }

                switch (clickedTile)
                {
                    case TileBase finalStageTile when finalStageTile == finalStageTileA:
                        tileScores[finalStageTileA]++;
                        Debug.Log("Mode A score: " + tileScores[finalStageTileA]);
                        break;
                    case TileBase finalStageTile when finalStageTile == finalStageTileB:
                        tileScores[finalStageTileB]++;
                        Debug.Log("Mode B score: " + tileScores[finalStageTileB]);
                        break;
                    case TileBase finalStageTile when finalStageTile == finalStageTileC:
                        tileScores[finalStageTileC]++;
                        Debug.Log("Mode C score: " + tileScores[finalStageTileC]);
                        break;
                }

                // Revert to the starting tile A or B based on the current planting mode
                TileBase newStartingTile = (clickedTile == finalStageTileA || clickedTile == stageOneTileA) ? startingTileA : startingTileB;
                tilemap.SetTile(tilePosition, newStartingTile);

                if (tileCoroutines.ContainsKey(tilePosition))
                {
                    StopCoroutine(tileCoroutines[tilePosition]);
                    tileCoroutines.Remove(tilePosition);
                }
            }
        }
    }

    private IEnumerator GrowTile(Vector3Int tilePosition, int mode)
    {
        switch (mode)
        {
            case 0:
                tilemap.SetTile(tilePosition, stageOneTileA);
                yield return new WaitForSeconds(2.0f);
                tilemap.SetTile(tilePosition, stageTwoTileA);
                yield return new WaitForSeconds(2.0f);
                tilemap.SetTile(tilePosition, finalStageTileA);
                break;

            case 1:
                tilemap.SetTile(tilePosition, stageOneTileB);
                yield return new WaitForSeconds(2.0f);
                tilemap.SetTile(tilePosition, stageTwoTileB);
                yield return new WaitForSeconds(2.0f);
                tilemap.SetTile(tilePosition, finalStageTileB);
                break;

            case 2:
                tilemap.SetTile(tilePosition, stageOneTileC);
                yield return new WaitForSeconds(2.0f);
                tilemap.SetTile(tilePosition, stageTwoTileC);
                yield return new WaitForSeconds(2.0f);
                tilemap.SetTile(tilePosition, finalStageTileC);
                break;
        }

        tileCoroutines.Remove(tilePosition);
    }
}
