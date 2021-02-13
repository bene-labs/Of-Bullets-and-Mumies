using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Wave : MonoBehaviour
{
    public GameObject[] enemiesToSpawn;
    public Tilemap tileMap;
    public List<Vector3> availablePlaces;
    public Camera mainCam;
    public float timeLeft;

    private

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GetComponentInParent<WaveManager>().mainCam;
        tileMap = GetComponentInParent<WaveManager>().tileMap;
        availablePlaces = new List<Vector3>();

        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)tileMap.transform.position.y));
                Vector3 place = tileMap.CellToWorld(localPlace);
                place.z = -2;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startWave()
    {
        spawnEnemies();
    }

    void spawnEnemies()
    {
        for (int i = 0; i < enemiesToSpawn.Length; i++)
        {
            for (int j = 0; j < enemiesToSpawn[i].GetComponent<Enemy>().total; j++)
            {
                while (true)
                {
                    int temp = Random.Range(0, availablePlaces.ToArray().Length - 1);
                    Debug.Log(temp);
                    transform.position = availablePlaces.ToArray()[temp];

                    //to make enemies only spawn outside the players view
                    Vector3 screenPoint = mainCam.WorldToViewportPoint(transform.position);
                    if ((screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1))
                        continue;
                    /*RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
                if (hit)
                    continue;*/
                    GameObject enemyTemp = Instantiate(enemiesToSpawn[i], transform.position, Quaternion.identity);
                    break;
                }
                //TileBase tempTile;
                //while ((tempTile = allTiles[Random.Range(0, bounds.size.x * bounds.size.y)]) == null || tempTile.GetTileData() );
            }
        }
    }

    public void spawnRandomEnemies(int max)
    {

        for (int i = 0; i < enemiesToSpawn.Length; i++)
        {
            for (int j = 0; j < Random.Range(max / 10, max); j++)
            {
                while (true)
                {
                    int temp = Random.Range(0, availablePlaces.ToArray().Length - 1);
                    Debug.Log(temp);
                    transform.position = availablePlaces.ToArray()[temp];

                    //to make enemies only spawn outside the players view
                    Vector3 screenPoint = mainCam.WorldToViewportPoint(transform.position);
                    if ((screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1))
                        continue;
                    /*RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
                if (hit)
                    continue;*/
                    GameObject enemyTemp = Instantiate(enemiesToSpawn[i], transform.position, Quaternion.identity);
                    break;
                }
                //TileBase tempTile;
                //while ((tempTile = allTiles[Random.Range(0, bounds.size.x * bounds.size.y)]) == null || tempTile.GetTileData() );
            }
        }
    }
}
