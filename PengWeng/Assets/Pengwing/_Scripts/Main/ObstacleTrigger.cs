using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    CharController m_charController;
    PengwingManager m_pengwingManager;
    public Transform staticObj;
    public Transform shardParent;

    void Start()
    {

        // rb = GetComponent<Rigidbody>();

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_pengwingManager = GameObject.FindWithTag("GameManager").GetComponent<PengwingManager>();
            m_charController = other.gameObject.GetComponentInParent<CharController>();

            Vector3 direction = other.contacts[0].point - Vector3.forward;
            direction = direction.normalized;

            shardParent.gameObject.SetActive(true);
            staticObj.gameObject.SetActive(false);
            Physics.gravity = new Vector3(0, -20, 0);

            foreach (Transform shardObjects in shardParent)
            {
                shardObjects.gameObject.AddComponent<Rigidbody>();
                shardObjects.GetComponent<Rigidbody>().mass = 1;
                shardObjects.GetComponent<Rigidbody>().drag = .5f;
                shardObjects.GetComponent<Rigidbody>().angularDrag = .5f;
                shardObjects.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }

            // Force sets a lot of things controlling the character
            //================================================================================================================

            m_charController.isGrounded = false;
            m_charController.PlayerHasDied();
            m_charController.thrust = 0;
            m_charController.m_worldTileManager.maxSpeed = 0;
            m_charController.speed = 0;
            CharController.canRaycast = false;
            CharController.playerParticles.rateOverTime = 0;
        }
    }
}
