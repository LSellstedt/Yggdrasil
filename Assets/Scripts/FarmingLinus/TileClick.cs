using UnityEngine;
using UnityEngine.Tilemaps;

public class TileClick : MonoBehaviour
{
    public TileBase initialTile; // Initial tile referenced in TileTimer
    public TileBase specificTile; // Tile that must be present to allow clicking
    private Tilemap tilemap;
    public TileTimer tileTimer; // Reference to the TileTimer script
    public Item itemToPickup; // The item associated with the specific tile

    private PickupItem pickupItem; // Reference to the PickupItem script

    private void Start()
    {
        // Find the Tilemap component
        tilemap = GetComponentInChildren<Tilemap>();
        // Find the PickupItem script
        pickupItem = FindObjectOfType<PickupItem>();
    }

    private void Update()
    {
        // Check for mouse click within the boundaries of the Tilemap
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.GetComponent<Tilemap>() == tilemap)
            {
                // Check if the clicked tile is the specific tile
                Vector3Int cellPos = tilemap.WorldToCell(hit.point);
                TileBase originalTile = tilemap.GetTile(cellPos);
                if (originalTile == specificTile)
                {
                    ChangeTile(hit.point);

                    // Set the item to be picked up
                    if (pickupItem != null)
                    {
                        pickupItem.SetItemToPickup(itemToPickup);
                        pickupItem.canPickup = true;
                    }

                    // Check if the tile change is from final to initial
                    if (tileTimer != null)
                    {
                        // Restart initial timer
                        tileTimer.RestartInitialTimer();
                    }
                }
            }
        }
    }

    private void ChangeTile(Vector3 worldPos)
    {
        // Convert the mouse position to the cell position on the Tilemap
        Vector3Int cellPos = tilemap.WorldToCell(worldPos);

        // Change the tile at the clicked position to the initial tile
        tilemap.SetTile(cellPos, initialTile);
    }
}
