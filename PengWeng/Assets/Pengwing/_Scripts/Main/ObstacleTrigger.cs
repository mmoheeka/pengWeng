using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    CharController m_charController;
    public Transform staticObj;
    public Transform shardParent;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "PlayerRoot")
        {
            PengwingManager pManager = FindObjectOfType<PengwingManager>();
            pManager.UpdatePlayerDeath();

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

        }
    }
}
