using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    CharController m_charController;
    PengwingManager m_pengwingManager;
    public Transform shardParent;
    public Transform staticObj;

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

            Vector3 direction = other.contacts[0].point - Vector3.right;
            direction = direction.normalized;

            shardParent.gameObject.SetActive(true);
            staticObj.gameObject.SetActive(false);

            foreach (Transform shardObjects in shardParent)
            {
                shardObjects.gameObject.AddComponent<Rigidbody>();
                shardObjects.GetComponent<Rigidbody>().mass = 2;
                shardObjects.GetComponent<Rigidbody>().drag = .5f;
                shardObjects.GetComponent<Rigidbody>().angularDrag = .1f;
                shardObjects.GetComponent<Rigidbody>().AddForce(direction * 10, ForceMode.Impulse);
            }

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
