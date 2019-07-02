using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PengwingManager : Singleton<PengwingManager>
{

    public TextMeshProUGUI crystalCounter;
    public Slider progressBar;

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
        }

        if (rampTimer >= 30)
        {
            if (addRamp != null)
            {
                addRamp();
            }
            rampTimer = 0;
        }
    }

    void OnDestroy()
    {
        _charController.playerHasDied -= UpdatePlayerDeath;
        _charController.hittingRamp -= RampHit;
        // _crystal.collectedCrystal -= CollectedCrystal;
    }

    void UpdatePlayerDeath()
    {
        FollowCam _followCam = GameObject.FindObjectOfType<FollowCam>();
        _followCam.camTarget = _charController.character.transform.GetChild(0).gameObject.transform;
        _followCam.smoothSpeed = 0;

        _charController.character.transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Destroy(_charController.character.transform.GetChild(2).gameObject);
        Rigidbody ragDollRB = _charController.character.transform.GetChild(1).transform.GetChild(1).GetComponentInChildren<Rigidbody>();
        ragDollRB.AddForce(Vector3.up * 5, ForceMode.Impulse);
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


    public void LevelComplete()
    {

    }

    void SavePlayerProgress()
    {

    }

    void LoadPlayerProgress()
    {

    }

}
