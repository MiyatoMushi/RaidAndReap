using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FootstepSFX : MonoBehaviour
{
    //For Tilemaps - JC
    public Tilemap tileMap;
    public AudioSource audioSource;
    public AudioClip grassSound;
    public AudioClip soilSound;
    public AudioClip stoneSound;
    public AudioClip woodSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DetectTileUnderPlayer() {
        //For Converting the Player Position on the Tilemap or Sing;e Cell Position- JC
        Vector3Int cellPosition = tileMap.WorldToCell(transform.position);

        //Determine the Tile under the player, based on current position - JC
        TileBase tile = tileMap.GetTile(cellPosition);

        //Play yung sound depende sa name ng tile
        if (tile != null) {
            if (tile.name == "GrassTiles") {
                audioSource.PlayOneShot(grassSound);
            }
            else if (tile.name == "SoilTiles") {
                audioSource.PlayOneShot(soilSound);
            }
            else if (tile.name == "StoneTiles") {
                audioSource.PlayOneShot(stoneSound);
            }
            else if (tile.name == "WoodTiles") {
                audioSource.PlayOneShot(woodSound);
            }
         }

    }
}
