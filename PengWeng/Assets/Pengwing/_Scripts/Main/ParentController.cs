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


    // Update is called once per frame
    void Update()
    {
        wantedPosition = new Vector3(target.position.x, target.position.y + height, target.position.z + distance);
        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);
    }
}
