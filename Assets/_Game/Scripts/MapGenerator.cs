using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] GameObject floor;

    [SerializeField] Texture2D mapImage;

    void Start()
    {
        Debug.Log("1");
        Color[] pixels = mapImage.GetPixels();

        int worldX = mapImage.width;
        int worldZ = mapImage.height;

        Vector3[] spawnPositions = new Vector3[pixels.Length];
        Vector3 startingSpawnPos = new Vector3(-Mathf.Round(worldX / 2), 0, -Mathf.Round(worldZ / 2));
        Vector3 currentSpawnPos = startingSpawnPos;

        int counter = 0;

        for (int z = 0; z < worldZ; z++)
        {
            for (int x = 0; x < worldX; x++)
            {
                spawnPositions[counter] = currentSpawnPos;
                counter++;
                currentSpawnPos.x++;
            }

            currentSpawnPos.x = startingSpawnPos.x;
            currentSpawnPos.z++;
        }

        counter = 0;

        foreach (Vector3 pos in spawnPositions)
        {
            Color color = pixels[counter];

            Debug.Log("2");
            if (color.Equals(Color.white))
            {
                Debug.Log("3");
                Instantiate(floor, pos, floor.transform.rotation);
            }

            counter++;
        }
    }
}
