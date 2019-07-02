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
        transform.Rotate(0, 60 * Time.deltaTime, 0);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PengwingManager.Instance.CollectedCrystal();

            LeanTween.moveY(this.gameObject, this.transform.position.y + 2, .15f).setOnComplete(ScaleAnimation);

        }
    }

    void ScaleAnimation()
    {
        LeanTween.scale(this.gameObject, new Vector3(0, 0, 0), .25f).setEaseInOutSine();
    }

    void DestroyCrystal()
    {

    }
}
