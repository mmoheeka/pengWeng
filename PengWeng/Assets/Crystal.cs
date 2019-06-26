using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RotateCrystal();
    }

    void RotateCrystal()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }

    void DestroyCrystal()
    {

    }
}
