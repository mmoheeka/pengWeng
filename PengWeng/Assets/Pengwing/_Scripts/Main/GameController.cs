
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    /** Speed Increase Value */
    static float SPEED_INCREASE = .05f;

    /** Seeded Randomizer */
    static System.Random RND;

    /** Tileholder */
    public GameObject TileHolder;

    /** TileManager */
    private WorldTileManager tileManager;

    /** On Awake */
    void Awake()
    {
        // 32 is just an arbitrary seed number. Could be anything.
        RND = new System.Random(32);
        this.tileManager = TileHolder.GetComponent<WorldTileManager>();
    }

    /** On Start */
    void Start()
    {
        this.tileManager.Init();
    }

    /** On Update */
    void Update()
    {
        this.tileManager.IncreaseSpeed(SPEED_INCREASE);
        this.tileManager.UpdateTiles(RND);
    }


}
