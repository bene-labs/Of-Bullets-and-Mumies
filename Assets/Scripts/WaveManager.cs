using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaveManager : MonoBehaviour
{
    public Camera mainCam;
    public Tilemap tileMap = null;
    public GameObject[] totalWaves;
    public bool isRandomWaves = false;
    bool allWavesCompleted = false;

    public List<Vector3> availablePlaces;

    public float time = 60;
    public int wave = 1;
    public GameObject[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        availablePlaces = new List<Vector3>();

        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)tileMap.transform.position.y));
                Vector3 place = tileMap.CellToWorld(localPlace);
                if (tileMap.HasTile(localPlace))
                {
                    //Tile at "place"
                    availablePlaces.Add(place);
                }
                else
                {
                    //No tile at "place"
                }
            }
        }
        buttons = GameObject.FindGameObjectsWithTag("Button");

        totalWaves[0].GetComponent<Wave>().startWave();

        //Debug.Log(availablePlaces);
       /* Debug.Log(availablePlaces.Capacity + "Tiles found:");
        foreach (Vector3 pos in availablePlaces)
            Debug.Log(pos);*/
/*        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                tile.trans
                if (tile != null)
                {
                    Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                }
                else
                {
                    Debug.Log("x:" + x + " y:" + y + " tile: (null)");
                }
            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (allWavesCompleted)
            return;
        time -= Time.deltaTime;
        if (time < 0)
        {
            NewWave();
        }
    }

    void NewWave()
    {
        time = 60;
        wave += 1;

        if (isRandomWaves)
        {
            totalWaves[0].GetComponent<Wave>().spawnRandomEnemies(wave * 100);
        }
        else
        {
            if (wave > totalWaves.Length)
            {
                allWavesCompleted = true;
                return;
            }
            totalWaves[wave].GetComponent<Wave>().startWave();
        }

        int[] indexArray = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
        List<int> buttonsIndex = new List<int>();
        for (int i = wave < 18 ? wave : 18; i > 0; i -= 1)
        {
            int index = UnityEngine.Random.Range(0, indexArray.Length);
            buttonsIndex.Add(indexArray[index]);
            RemoveAt(ref indexArray, index);
        }
        foreach (int index in buttonsIndex)
        {
            buttons[index].GetComponent<ButtonController>().isActivated = false;
        }
    }

    public static void RemoveAt<T>(ref T[] arr, int index)
    {
        for (int a = index; a < arr.Length - 1; a++)
        {
            arr[a] = arr[a + 1];
        }
        Array.Resize(ref arr, arr.Length - 1);
    }
}
