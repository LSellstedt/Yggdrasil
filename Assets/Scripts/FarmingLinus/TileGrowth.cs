using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGrowth : MonoBehaviour
{
    public TileBase startingTile;
    public TileBase stageOneTile;
    public TileBase stageTwoTile;
    public TileBase finalStageTile;

    private Tilemap tilemap;
    private Dictionary<Vector3Int, Coroutine> tileCoroutines = new Dictionary<Vector3Int, Coroutine>();

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tilePosition = tilemap.WorldToCell(mouseWorldPos);

            TileBase clickedTile = tilemap.GetTile(tilePosition);
            if (clickedTile == finalStageTile)
            {
                // Revert to the starting tile
                tilemap.SetTile(tilePosition, startingTile);
                if (tileCoroutines.ContainsKey(tilePosition))
                {
                    StopCoroutine(tileCoroutines[tilePosition]);
                    tileCoroutines.Remove(tilePosition);
                }
            }
            else if (clickedTile == startingTile || clickedTile == null)
            {
                // Start the growth process
                if (tileCoroutines.ContainsKey(tilePosition))
                {
                    StopCoroutine(tileCoroutines[tilePosition]);
                }
                Coroutine growthCoroutine = StartCoroutine(GrowTile(tilePosition));
                tileCoroutines[tilePosition] = growthCoroutine;
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
