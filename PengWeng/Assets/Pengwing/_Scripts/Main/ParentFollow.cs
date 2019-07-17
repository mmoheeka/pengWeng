using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentFollow : MonoBehaviour
{
    public Transform target;
    public float distance = 3.0f;
    public float height = 3.0f;
    public float damping = 5.0f;

    // public float rotationDamping = 10.0f;

    private Vector3 wantedPosition;

    void Update()
    {
        // wantedPosition = target.TransformPoint(0, distance, -height);

        wantedPosition = new Vector3(target.position.x, target.position.y + height, target.position.z + distance);
        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);

        // Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
        // wantedRotation = new Quaternion(0, wantedRotation.y, 0, wantedRotation.w);
        // transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
    }
}
