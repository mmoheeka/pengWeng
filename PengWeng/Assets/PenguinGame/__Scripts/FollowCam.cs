using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    private readonly float smooth = .75f;

    public Transform camTarget;
    public Vector3 cameraOffset;
    public float smoothSpeed = .125f;

    public CharController _characterController;
    public bool camPull;

    public float currentLerpTime;
    public float lerpTime;

    private Camera cam;

    // Use this for initialization
    void Start()
    {
        cameraOffset = transform.position - camTarget.position;
        cameraOffset.y -= 3;

        cam = GetComponent<Camera>();
        //cam.fieldOfView = cameraPullAmount;
    }


    void Update()
    {

        Vector3 desiredPosition = camTarget.position + cameraOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;


        //if (_characterController.rb.velocity.y > 6)
        //{
        //    camPull = true;

        //    if (camPull)
        //    {

        //        currentLerpTime += Time.deltaTime;

        //        if (currentLerpTime > lerpTime)
        //        {
        //            currentLerpTime = lerpTime;
        //        }
        //        float perc = currentLerpTime / lerpTime;
        //        cam.fieldOfView = Mathf.Lerp(60, 120, perc);

        //        if (currentLerpTime >= 1)
        //        {
        //            currentLerpTime = 0;
        //            camPull = false;
        //        }

        //    }
        //}
        //if (!_characterController.isInTheAir)
        //{

        //    cam.fieldOfView = 60;
        //}


    }
}
