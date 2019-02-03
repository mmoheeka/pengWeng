using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{

    Rigidbody rb;
    CharController m_charController;

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

            m_charController = other.gameObject.GetComponentInParent<CharController>();
            rb.useGravity = true;
            m_charController.isGrounded = false;

            Debug.Log("Player hit");
            Vector3 direction = other.contacts[0].point - Vector3.right;

            direction = direction.normalized;
            var reverseDir = -direction.normalized;
            rb.AddForce(direction * 30);
            other.gameObject.GetComponentInChildren<Rigidbody>().AddForce(direction * -500);





            //other.gameObject.GetComponentInChildren<Rigidbody>().AddExplosionForce(20, other.gameObject.transform.position, 15, 5, ForceMode.Impulse);




            m_charController.thrust = 0;
            m_charController.m_worldTileManager.maxSpeed = 0;
            m_charController.speed = 0;
            CharController.canRaycast = false;
            CharController.playerParticles.rateOverTime = 0;



        }
    }
}
