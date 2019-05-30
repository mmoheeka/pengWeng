using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTileManager : MonoBehaviour
{
    public bool ramps;

    /** Max Number of Tiles visible at one time */
    static int MAX_TILES = 3;

    /** Max speed of tiles */
    [Range(0f, 150f)]
    public float maxSpeed = 10f;

    /** Current Speed of Tiles */
    public float speed;

    /** Collection of Types of Tiles */
    [Header("Begining Tiles")]
    public GameObject[] beginingTiles;

    [Header("Intermediate Tiles")]
    public GameObject[] intermediateTiles;

    [Header("Difficult Tiles")]
    public GameObject[] DifficultTiles;

    /** Size of Tiles in z dimension best size is 7.62f */
    private float tileSize = 136f;

    /** Collection of active Tiles */
    private List<GameObject> tiles;

    /** Pool of Tiles */
    private TilePool tilePool;

    /** Initialize */
    public void Init()
    {
        speed = 0f;
        tiles = new List<GameObject>();
        tilePool = new TilePool(beginingTiles, 2, transform);
        InitTiles();
    }

    /** Increase speed by given amount */
    public void IncreaseSpeed(float amt)
    {
        speed += amt;
        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }
    }

    /** Update Tiles */
    public void UpdateTiles(System.Random rnd)
    {
        for (int i = tiles.Count - 1; i >= 0; i--)
        {
            GameObject tile = tiles[i];
            tile.transform.Translate(0f, 0f, -speed * Time.deltaTime);

            // If a tile moves behind the camera release it and add a new one
            if (tile.transform.position.z + 200 < Camera.main.transform.position.z)
            {
                tiles.RemoveAt(i);
                tilePool.ReleaseTile(tile);
                int type = rnd.Next(0, beginingTiles.Length);

                AddTile(type);

                Debug.Log("Update Tiles is being called");
            }
        }
    }

    /** Add a new Tile */
    private void AddTile(int type)
    {
        GameObject tile = tilePool.GetTile(type);

        // position tile's z at 0 or behind the last item added to tiles collection
        float zPos = tiles.Count == 0 ? 0f : tiles[tiles.Count - 1].transform.position.z + tileSize;
        tile.transform.Translate(0f, 0f, zPos);
        tiles.Add(tile);

        Debug.Log("Add Tile is being called");
    }

    /** Initialize Tiles */
    private void InitTiles()
    {
        AddTile(0);
        for (int i = 0; i < MAX_TILES; i++)
        {
            AddTile(Random.Range(0, beginingTiles.Length));
        }
    }

    // ================================================================================================================

    // This class is used to create the tile pool and get and recieve

    // ================================================================================================================


    /** Object Pool for World Tiles */
    class TilePool
    {
        /** Pool of Tiles */
        private List<GameObject>[] pool;

        /** Model Transform */
        private Transform transform;

        /** Create a new TilePool */
        public TilePool(GameObject[] types, int size, Transform transform)
        {
            this.transform = transform;
            int numTypes = types.Length;
            pool = new List<GameObject>[numTypes];
            for (int i = 0; i < numTypes; i++)
            {
                pool[i] = new List<GameObject>(size);
                for (int j = 0; j < size; j++)
                {
                    GameObject tile = (GameObject)Instantiate(types[i]);
                    tile.SetActive(false);
                    pool[i].Add(tile);
                }
            }
        }

        /** Get a Tile */
        public GameObject GetTile(int type)
        {
            Debug.Log("Get Tile is being called");
            for (int i = 0; i < pool[type].Count; i++)
            {
                GameObject tile = this.pool[type][i];
                // Ignore active tiles until we find the 1st inactive in appropriate list
                if (tile.activeInHierarchy)
                    continue;

                // reset the tile's transform to match the model transform
                tile.transform.position = this.transform.position;
                tile.transform.rotation = this.transform.rotation;

                // set to active and return
                tile.SetActive(true);
                return tile;
            }

            // This will never be reached, but compiler requires it
            return null;
        }

        /** Release a Tile */
        public void ReleaseTile(GameObject tile)
        {
            Debug.Log("Release Tile is being called");

            // Inactivate the released tile
            tile.SetActive(false);
        }
    }
}
