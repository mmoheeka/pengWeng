using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{

    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player hit");
            Vector3 direction = other.contacts[0].point - transform.position;
            direction = -direction.normalized;
            rb.AddForce(direction * 200);
            other.gameObject.GetComponentInChildren<Rigidbody>().AddExplosionForce(10, other.gameObject.transform.position, 10, 2, ForceMode.Impulse);
            other.gameObject.GetComponent<CharController>().thrust = 0;
            other.gameObject.GetComponent<CharController>().m_worldTileManager.maxSpeed = 0;

        }
    }
}
