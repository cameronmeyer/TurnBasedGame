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
        List<Vector2> squidRespawns = new List<Vector2>();
        List<Vector2> kidRespawns = new List<Vector2>();

        for (int z = 0; z < worldZ; z++)
        {
            for (int x = 0; x < worldX; x++)
            {
                if(pixels[counter].Equals(Color.white))
                {
                    GameObject floorTile = Instantiate(floor, currentSpawnPos, floor.transform.rotation);
                    floorTile.GetComponent<Tile>().Init(TileSpawn.NO_SPAWN);
                    board[x, z] = new GridSpace(null, floorTile.GetComponent<Tile>());
                }
                else if(pixels[counter].Equals(Color.red))
                {
                    GameObject floorTile = Instantiate(floor, currentSpawnPos, floor.transform.rotation);
                    //floorTile.GetComponent<Tile>().respawn = TileSpawn.RESPAWN_SQUID;
                    floorTile.GetComponent<Tile>().Init(TileSpawn.RESPAWN_SQUID);
                    squidRespawns.Add(new Vector2(x, z));
                    board[x, z] = new GridSpace(null, floorTile.GetComponent<Tile>());
                }
                else if (pixels[counter].Equals(Color.blue))
                {
                    GameObject floorTile = Instantiate(floor, currentSpawnPos, floor.transform.rotation);
                    //floorTile.GetComponent<Tile>().respawn = TileSpawn.RESPAWN_KID;
                    floorTile.GetComponent<Tile>().Init(TileSpawn.RESPAWN_KID);
                    kidRespawns.Add(new Vector2(x, z));
                    board[x, z] = new GridSpace(null, floorTile.GetComponent<Tile>());
                }
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

        BoardStatus.current.Setup(board);
        BoardStatus.current.InitTeams(squidRespawns, kidRespawns);

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
        
    }
}
