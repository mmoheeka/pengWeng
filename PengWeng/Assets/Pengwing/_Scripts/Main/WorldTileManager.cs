using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTileManager : MonoBehaviour
{
    public PengwingManager pengwingManager;

    public bool rampTest;

    [Header("This bool is used for testing")]
    public bool testSpeedSwitch;
    public float testSpeed;


    /** Max Number of Tiles visible at one time */
    static int MAX_TILES = 2;

    /** Max speed of tiles */
    [Range(0f, 150f)]
    public float maxSpeed = 10f;

    /** Current Speed of Tiles */
    public float speed;

    /** Collection of Types of Tiles */
    [Header("Pooled Tiles")]
    // public List<GameObject> tileTypes = new List<GameObject>();
    public GameObject[] tileTypes;

    public GameObject currentRamp;

    public GameObject[] rampTiles;

    public GameObject[] waitingRampTiles;


    /** Size of Tiles in z dimension best size is 7.62f */
    private float tileSize = 136f;

    /** Collection of active Tiles */
    public List<GameObject> tiles;

    /** Pool of Tiles */
    private TilePool tilePool;

    private TilePool newTilePool;


    void Start()
    {
        pengwingManager.addRamp += StartRamp;
    }

    /** Initialize */
    public void Init()
    {
        speed = 0f;
        tiles = new List<GameObject>();
        tilePool = new TilePool(tileTypes, MAX_TILES, transform);

        InitTiles();
    }

    public void Update()
    {

    }

    public void StartRamp()

    {
        newTilePool = new TilePool(rampTiles, 1, transform);
        AddRampTile(0);
        foreach (var k in tiles)
        {
            if (k.gameObject.tag == "Ramp")
            {
                currentRamp = k;
            }
        }
    }


    public void RemoveRampTile()
    {
        if (currentRamp != null)
        {
            if (!currentRamp.activeInHierarchy)
            {
                Destroy(currentRamp);
                int index = tiles.Count - 1;
                GameObject lastObject = tiles[index].gameObject;
                tilePool.ReleaseTile(lastObject);
                tiles.RemoveAt(tiles.Count - 1);
                int newObject = Random.Range(0, waitingRampTiles.Length);
                rampTiles[0] = waitingRampTiles[newObject];
            }
        }


    }


    public void AddRampTile(int type)
    {
        GameObject tile = newTilePool.GetTile(type);

        // position tile's z at 0 or behind the last item added to tiles collection
        float zPos = this.tiles.Count == 0 ? 0f : this.tiles[this.tiles.Count - 1].transform.position.z + this.tileSize;
        tile.transform.Translate(0f, 0f, zPos);
        this.tiles.Add(tile);
    }


    /** Increase speed by given amount */
    public void IncreaseSpeed(float amt)
    {
        if (testSpeedSwitch)
        {
            speed = testSpeed;
        }
        else
        {
            speed += amt;
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }

        }
    }

    /** Update Tiles */
    public void UpdateTiles(System.Random rnd)
    {
        for (int i = tiles.Count - 1; i >= 0; i--)
        {
            GameObject tile = tiles[i];
            tile.transform.Translate(0f, 0f, -this.speed * Time.deltaTime);

            // If a tile moves behind the camera release it and add a new one
            if (tile.transform.position.z + 200 < Camera.main.transform.position.z)
            {
                this.tiles.RemoveAt(i);
                this.tilePool.ReleaseTile(tile);
                int type = rnd.Next(0, this.tileTypes.Length);
                AddTile(type);
                RemoveRampTile();
            }
        }

    }


    /** Add a new Tile */
    public void AddTile(int type)
    {
        GameObject tile = tilePool.GetTile(type);

        // position tile's z at 0 or behind the last item added to tiles collection
        float zPos = this.tiles.Count == 0 ? 0f : this.tiles[this.tiles.Count - 1].transform.position.z + this.tileSize;
        tile.transform.Translate(0f, 0f, zPos);
        this.tiles.Add(tile);

    }



    /** Initialize Tiles */
    private void InitTiles()
    {
        for (int i = 0; i < MAX_TILES; i++)
        {
            AddTile(0);
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

        private List<GameObject>[] types;

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
            // Inactivate the released tile
            tile.SetActive(false);
        }
    }
}
