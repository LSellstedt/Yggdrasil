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

    public TileBase stageOneTileD;
    public TileBase stageTwoTileD;
    public TileBase finalStageTileD;

    public TileBase tileX; // Assign this in the inspector
    public TileBase tileY; // Assign this in the inspector

    private Tilemap tilemap;
    private Dictionary<Vector3Int, Coroutine> tileCoroutines = new Dictionary<Vector3Int, Coroutine>();
    private Dictionary<TileBase, int> tileScores = new Dictionary<TileBase, int>();

    private int plantingMode = 0; // 0 for A, 1 for B, 2 for C, 3 for D

    void Start()
    {
        tilemap = GetComponent<Tilemap>();

        // Initialize the scores dictionary
        tileScores[tileX] = 0;
        tileScores[tileY] = 0;
        tileScores[stageOneTileA] = 0;
        tileScores[stageOneTileB] = 0;
        tileScores[stageOneTileC] = 0;
        tileScores[stageOneTileD] = 0;
    }

    void Update()
    {
        // Switch planting modes when 'X' is pressed
        if (Input.GetKeyDown(KeyCode.X))
        {
            plantingMode = (plantingMode + 1) % 4;
            Debug.Log("Switched planting mode to: " + (plantingMode == 0 ? "Mode A" : plantingMode == 1 ? "Mode B" : plantingMode == 2 ? "Mode C" : "Mode D"));
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tilePosition = tilemap.WorldToCell(mouseWorldPos);

            TileBase clickedTile = tilemap.GetTile(tilePosition);
            if (clickedTile == startingTileA || clickedTile == startingTileB)
            {
                AudioManager.instance.PlayOneShot(plantplacedSound, this.transform.position);
                // Start the growth process
                if (tileCoroutines.ContainsKey(tilePosition))
                {
                    StopCoroutine(tileCoroutines[tilePosition]);
                }
                Coroutine growthCoroutine = StartCoroutine(GrowTile(tilePosition, plantingMode));
                tileCoroutines[tilePosition] = growthCoroutine;
            }
            else if (clickedTile == finalStageTileA || clickedTile == finalStageTileB || clickedTile == finalStageTileC || clickedTile == finalStageTileD)
            {
                AudioManager.instance.PlayOneShot(plantharvestSound, this.transform.position);
                // Update score based on the type of the final stage tile
                switch (clickedTile)
                {
                    case TileBase finalStageTile when finalStageTile == finalStageTileA:
                        tileScores[stageOneTileA]++;
                        Debug.Log("Mode A score: " + tileScores[stageOneTileA]);
                        break;
                    case TileBase finalStageTile when finalStageTile == finalStageTileB:
                        tileScores[stageOneTileB]++;
                        Debug.Log("Mode B score: " + tileScores[stageOneTileB]);
                        break;
                    case TileBase finalStageTile when finalStageTile == finalStageTileC:
                        tileScores[stageOneTileC]++;
                        Debug.Log("Mode C score: " + tileScores[stageOneTileC]);
                        break;
                    case TileBase finalStageTile when finalStageTile == finalStageTileD:
                        tileScores[stageOneTileD]++;
                        Debug.Log("Mode D score: " + tileScores[stageOneTileD]);
                        break;
                }

                // Revert to the starting tile A or B
                tilemap.SetTile(tilePosition, clickedTile == startingTileA ? startingTileA : startingTileB);
                
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

            case 3:
                tilemap.SetTile(tilePosition, stageOneTileD);
                yield return new WaitForSeconds(2.0f);
                tilemap.SetTile(tilePosition, stageTwoTileD);
                yield return new WaitForSeconds(2.0f);
                tilemap.SetTile(tilePosition, finalStageTileD);
                break;
        }

        tileCoroutines.Remove(tilePosition);
    }
}
