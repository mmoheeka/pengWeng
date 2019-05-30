using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PengwingManager : MonoBehaviour
{

    public CharController _charController;
    public Rigidbody ragDollRB;
    public float seconds;
    public float minutes;
    public int progressTime;
    public Slider progressBar;
    private float timer;


    void Start()
    {
        _charController = FindObjectOfType<CharController>();
        // Game events updated here //
        _charController.playerHasDied += UpdatePlayerDeath;
        _charController.hittingRamp += RampHit;
        _charController.collectedCoin += CollectedCoin;
    }

    void Update()
    {
        timer += Time.deltaTime;
        seconds = timer % 60;
        minutes = timer / 60;

        if (timer > 0)
        {
            progressBar.value = minutes / progressTime;
        }
    }

    void OnDestroy()
    {
        _charController.playerHasDied -= UpdatePlayerDeath;
    }

    void UpdatePlayerDeath()
    {
        FollowCam _followCam = GameObject.FindObjectOfType<FollowCam>();
        _followCam.camTarget = _charController.character.transform.GetChild(0).gameObject.transform;
        _followCam.smoothSpeed = 0;

        _charController.character.transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Destroy(_charController.character.transform.GetChild(2).gameObject);
        ragDollRB = _charController.character.transform.GetChild(1).transform.GetChild(1).GetComponentInChildren<Rigidbody>();
        ragDollRB.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    void RampHit()
    {
        // Move ramp logic here //

    }

    void CollectedCoin()
    {
        // Add all coin collection information here since UI is in this script //

    }

    void SavePlayerProgress()
    {

    }

    void LoadPlayerProgress()
    {

    }

}
