using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PengwingManager : Singleton<PengwingManager>
{
    public GameObject[] allPlayers;

    public TextMeshProUGUI crystalCounter;
    public Slider progressBar;
    public Image blackScreen;

    [SerializeField]
    private int progressTime;

    private CharController _charController;
    private FollowCam _followCam;
    private WorldTileManager _worldTileManager;

    private float seconds;
    private float minutes;
    private float timer;
    private float rampTimer;
    private int crystalCount;

    public delegate void AddRamp();
    public event AddRamp addRamp;


    void Start()
    {
        allPlayers = GameObject.FindGameObjectsWithTag("PlayerRoot");
        _worldTileManager = FindObjectOfType<WorldTileManager>();
        _charController = FindObjectOfType<CharController>();
        _followCam = FindObjectOfType<FollowCam>();
        // Game events updated here //

        _charController.playerHasDied += UpdatePlayerDeath;
        _charController.hittingRamp += RampHit;
        // _crystal.collectedCrystal += CollectedCrystal;
    }

    void Update()
    {
        crystalCounter.text = crystalCount.ToString();

        timer += Time.deltaTime;
        seconds = timer % 60;
        minutes = timer / 60;
        rampTimer += Time.deltaTime;

        if (timer > 0)
        {
            progressBar.value = minutes / progressTime;

            if (progressBar.value >= 1)
            {
                LevelComplete();
                //Pause Editor for testing new level
                Debug.Break();
            }
        }

        if (rampTimer >= 30)
        {
            if (addRamp != null)
            {
                addRamp();
            }
            rampTimer = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            LevelComplete();
        }

    }

    void OnDestroy()
    {
        _charController.playerHasDied -= UpdatePlayerDeath;
        _charController.hittingRamp -= RampHit;
        // _crystal.collectedCrystal -= CollectedCrystal;
    }

    public void UpdatePlayerDeath()
    {
        FollowCam _followCam = GameObject.FindObjectOfType<FollowCam>();
        // _followCam.camTarget = _charController.character.transform.GetChild(0).gameObject.transform;
        _followCam.smoothSpeed = 0;

        _charController.isGrounded = false;
        _charController.thrust = 0;
        _charController._worldTileManager.maxSpeed = 0;
        _charController._worldTileManager.testSpeed = 0;
        _charController.speed = 0;
        CharController.canRaycast = false;


        foreach (var player in allPlayers)
        {
            player.transform.GetChild(1).gameObject.SetActive(true);
            Destroy(player.transform.GetChild(2).gameObject);
            Destroy(player.transform.GetChild(0).gameObject);

            var ragDolls = player.transform.GetChild(1).transform.GetChild(1).GetComponentInChildren<Rigidbody>();
            ragDolls.AddForce(Vector3.up * 15, ForceMode.Impulse);

        }

        // Rigidbody ragDollRB = _charController.character.transform.GetChild(1).transform.GetChild(1).GetComponentInChildren<Rigidbody>();
        // ragDollRB.AddForce(Vector3.up * 5, ForceMode.Impulse);


        GameOver();
    }

    void RampHit()
    {
        _followCam.StartCoroutine("SpeedPowerUpOn");
        CharController.playerParticles.enabled = false;

    }

    public void CollectedCrystal()
    {
        crystalCount++;

    }

    //This is used when level is won/complete
    public void LevelComplete()
    {
        var AlphaOn = new Color(0.74f, 0.81f, 0.90f, 1);
        var AlphaOff = new Color(0.74f, 0.81f, 0.90f, 0);

        LeanTween.value(gameObject, AlphaOff, AlphaOn, 1f).setOnUpdate((Color val) =>
        {
            blackScreen.color = val;
        });
    }

    //This is used when player dies
    public void GameOver()
    {
        var AlphaOn = new Color(0.74f, 0.81f, 0.90f, 1);
        var AlphaOff = new Color(0.74f, 0.81f, 0.90f, 0);

        LeanTween.value(gameObject, AlphaOff, AlphaOn, 2).setOnUpdate((Color val) =>
        {
            blackScreen.color = val;
        }).setDelay(1);
    }

    void SavePlayerProgress()
    {

    }

    void LoadPlayerProgress()
    {

    }

}
