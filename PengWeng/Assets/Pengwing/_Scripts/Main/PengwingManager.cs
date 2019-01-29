using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PengwingManager : MonoBehaviour
{

    public int seconds;
    public int minutes;
    public int progressTime;
    public Slider progressBar;
    public bool playerDead;


    private float timer;
    // Use this for initialization

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        seconds = (int)timer % 60;
        minutes = (int)timer / 60;

        progressBar.value = Mathf.Lerp(0, 1, timer / progressTime / 60);







    }
}
