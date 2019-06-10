using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO
// Figure out how to get a random range into this script
// Figure out how to make it time based so it calls and releases appropriately
// Add the rest of the tile functionality


[System.Serializable]

public class ObjectPoolItem
{
    public GameObject[] objectsToPool;
    public int amountToPool;
    public bool shouldExpand;
}


public class LevelTiles : MonoBehaviour
{

    public static LevelTiles SharedInstance;
    public List<ObjectPoolItem> itemsToPool;
    public List<GameObject> pooledObjects;

    void Awake()
    {
        SharedInstance = this;
    }

    // Use this for initialization
    void Start()
    {

        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                for (int a = 0; a < item.objectsToPool.Length; a++)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectsToPool[a]);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        EasyMode();
        if (Input.GetKeyDown(KeyCode.Space))
        {
        }
    }

    void EasyMode()
    {

        for (int i = 0; i < pooledObjects.Count; i++)
        {
            int index = Random.Range(0, pooledObjects.Count);
            // GameObject currentObj = pooledObjects[index];
            GameObject currentObj = GetPooledObject("Obstacle");
            if (currentObj != null)
            {
                currentObj.SetActive(true);
            }

        }
        // foreach (GameObject newObject in pooledObjects)
        // {
        //     GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("Player Bullet");
        //     if (bullet != null)
        //     {
        //         bullet.transform.position = turret.transform.position;
        //         bullet.transform.rotation = turret.transform.rotation;
        //         bullet.SetActive(true);
        //     }
        // }
    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.objectsToPool.Length; i++)
            {
                if (item.objectsToPool[i].tag == tag)
                {
                    if (item.shouldExpand)
                    {
                        GameObject obj = (GameObject)Instantiate(item.objectsToPool[i]);
                        obj.SetActive(false);
                        pooledObjects.Add(obj);
                        return obj;
                    }
                }
            }


        }
        return null;
    }


}
