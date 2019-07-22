using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PengwingManager : Singleton<PengwingManager>
{


    public TextMeshProUGUI crystalCounter;
    public Slider progressBar;
    public Image blackScreen;
    public int currentLevel = 1;

    [SerializeField]
    private float progressTime;

    private CharController _charController;
    private FollowCam _followCam;
    private WorldTileManager _worldTileManager;

    private float seconds;
    private float minutes;
    [SerializeField]
    private float timer;
    private float rampTimer;
    private int crystalCount;
    private GameObject[] allPlayers;

    public delegate void AddRamp();
    public event AddRamp addRamp;

    void Awake()
    {
        PlayerPrefs.GetInt("CurrentLevel");

    }

    void Start()
    {
        StartCoroutine(StartTimer());
        allPlayers = GameObject.FindGameObjectsWithTag("PlayerRoot");
        _worldTileManager = FindObjectOfType<WorldTileManager>();
        _charController = FindObjectOfType<CharController>();
        _followCam = FindObjectOfType<FollowCam>();

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.G)) Debug.Log(PlayerPrefs.GetInt("CurrentLevel"));

        crystalCounter.text = crystalCount.ToString();


        rampTimer += Time.deltaTime;

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

    IEnumerator StartTimer()
    {

        while (timer < progressTime)
        {

            timer += Time.deltaTime;
            seconds = timer % 60;
            minutes = timer / 60;

            if (timer > 0)
            {
                progressBar.value = seconds / progressTime;

                if (progressBar.value >= 1)
                {
                    LevelComplete();
                    //Pause Editor for testing new level
                    // Debug.Break();
                }
            }

            yield return null;
        }

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

    public void HitRamp()
    {
        _followCam.SpeedPowerUpOn();
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
        currentLevel++;
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);

        var AlphaOn = new Color(0.74f, 0.81f, 0.90f, 1);
        var AlphaOff = new Color(0.74f, 0.81f, 0.90f, 0);

        LeanTween.value(gameObject, AlphaOff, AlphaOn, .5f).setOnUpdate((Color val) =>
        {
            blackScreen.color = val;
        }).setOnComplete(LoadingScreen);

    }

    public void LoadingScreen()
    {
        SceneManager.LoadScene("LoadingScreen");
        // Debug.Log("loading screen");
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



}
