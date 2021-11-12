using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] GameObject floor;
    [SerializeField] Texture2D mapImage;

    void Start()
    {
        Color[] pixels = mapImage.GetPixels();

        int worldX = mapImage.width;
        int worldZ = mapImage.height;

        GridSpace[,] board = new GridSpace[worldX, worldZ];

        Vector3[] spawnPositions = new Vector3[pixels.Length];
        Vector3 startingSpawnPos = new Vector3(-Mathf.Round(worldX / 2), 0, -Mathf.Round(worldZ / 2));

        // adjust starting point based on dimensions of map (centers the map on screen)
        startingSpawnPos.x += worldX % 2 == 0 ? 0.5f : 0;
        startingSpawnPos.z += worldZ % 2 == 0 ? 0.5f : 0;
        
        Vector3 currentSpawnPos = startingSpawnPos;

        int counter = 0;

        for (int z = 0; z < worldZ; z++)
        {
            for (int x = 0; x < worldX; x++)
            {
                if(pixels[counter].Equals(Color.white))
                {
                    GameObject newTile = Instantiate(floor, currentSpawnPos, floor.transform.rotation);
                    board[x, z] = new GridSpace(null, newTile.GetComponent<Tile>());
                }
                // else if(pixels[counter].Equals(Color.gray))
                // set the board space to be a spawn point, spawn a piece there, spawn a floor tile
                else
                {
                    board[x, z] = new GridSpace(null, null);
                }

                counter++;
                currentSpawnPos.x++;
            }

            currentSpawnPos.x = startingSpawnPos.x;
            currentSpawnPos.z++;
        }

        /*string boardDebug = "";
        for (int z = worldZ - 1; z >= 0; z--)
        {
            for (int x = 0; x < worldX; x++)
            {
                //boardDebug += (board[x, z].isFloor ? "T" : "F") + " ";
            }
            boardDebug += "\n";
        }*/

        //Debug.Log(boardDebug);
        //BoardStatus.current.Setup(board);
    }
}
