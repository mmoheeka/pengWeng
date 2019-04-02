using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PengwingManager : MonoBehaviour
{

    public float seconds;
    public float minutes;
    public int progressTime;
    public Slider progressBar;

    public bool playerDead;


    private float timer;

    void Start()
    {

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
}
