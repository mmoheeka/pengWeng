using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentController : MonoBehaviour
{
    public Transform target;

    [SerializeField]
    private float distance = -15;
    [SerializeField]
    private float height = .5f;
    [SerializeField]
    private float damping = 5;
    private Vector3 wantedPosition;

    Rigidbody rb;
    Quaternion startQuaternion;
    Quaternion currentQuaternion;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startQuaternion = rb.rotation;

    }

    // Update is called once per frame
    void Update()
    {

        currentQuaternion = rb.rotation;
        rb.rotation = Quaternion.Lerp(rb.rotation, startQuaternion, Time.deltaTime * 5);

        float angleDelta = Quaternion.Angle(startQuaternion, currentQuaternion);


        wantedPosition = new Vector3(target.position.x, target.position.y + height, target.position.z + distance);
        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);
    }
}
