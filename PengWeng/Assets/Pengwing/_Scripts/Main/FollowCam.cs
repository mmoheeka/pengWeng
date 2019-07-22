using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    private readonly float smooth = .75f;

    public Transform camTarget;
    public Vector3 cameraOffset;
    public float smoothSpeed = .125f;
    public float camFOV;


    public CharController _characterController;
    public bool camZoomOut;
    public bool camZoomIn;
    public bool currentlyBoosting;

    public float currentLerpTime;
    public float lerpTime;

    private Camera cam;

    // Use this for initialization
    void Start()
    {
        cam = GetComponent<Camera>();

        // cameraOffset.y -= 3;
        // cameraOffset.z -= 3;


        //cam.fieldOfView = cameraPullAmount;
    }


    void Update()
    {
        cameraOffset = (transform.position - camTarget.position);
        Vector3 desiredPosition = camTarget.position + cameraOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

    }

    public IEnumerator SpeedPowerUpOn()
    {
        if (camZoomOut) yield break;
        camZoomOut = true;
        lerpTime = 2;
        currentLerpTime = 0;
        while (currentLerpTime < lerpTime && camZoomOut && !currentlyBoosting)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
            }
            float perc = Mathf.Clamp01(currentLerpTime / lerpTime);

            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 120, perc * .25f);
            yield return null;
        }
        currentlyBoosting = true;
        camZoomOut = false;
        currentLerpTime = 0;

        yield return new WaitForSeconds(2);
        currentlyBoosting = false;
        StartCoroutine(SpeedPowerUpOff());
    }


    public IEnumerator SpeedPowerUpOff()
    {
        lerpTime = 2;
        currentLerpTime = 0;
        while (currentLerpTime < lerpTime && !camZoomOut && !currentlyBoosting)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
            }
            float perc = Mathf.Clamp01(currentLerpTime / lerpTime);

            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 60, perc * .25f);
            yield return null;
        }
        currentLerpTime = 0;

    }



}


