using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{

    public GameObject character;
    public GameObject mainCam;
    public Vector3 currentLerpPos;
    public Rigidbody rb;
    public int laneNumber;
    public float currentVector;
    public float thrust;
    public float speed;
    public float jumpHeight;
    public float lerpTime;
    public float currentLerpTime;
    public float forceAmount;
    public bool isJumping;
    public bool isGrounding;
    public bool isGrounded;
    public bool hitRamp;
    public bool isInTheAir;

    [System.Serializable]
    public enum CurrentLane { Left, Center, Right };
    public CurrentLane currentLane;


    private Vector3 startVector;
    private Quaternion startQuaternion;
    private Quaternion currentQuaternion;
    private ParticleSystem.EmissionModule playerParticles;
    public WorldTileManager m_worldTileManager;
    private PengwingManager m_pengWingManager;



    // Use this for initialization
    void Start()
    {
        character.transform.localPosition = transform.position;

        rb = character.GetComponent<Rigidbody>();
        startQuaternion = rb.rotation;
        playerParticles = this.GetComponentInChildren<ParticleSystem>().emission;
        m_worldTileManager = GameObject.FindObjectOfType<WorldTileManager>();
        m_pengWingManager = GameObject.FindObjectOfType<PengwingManager>();


    }

    // Update is called once per frame
    void Update()
    {

        LaneMonitor();

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (laneNumber < 1)
            {
                laneNumber++;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (laneNumber > -1)
            {
                laneNumber--;
            }
        }


        if (Input.GetKeyDown(KeyCode.UpArrow) && !isInTheAir)
        {
            StartCoroutine(Jump());
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && isJumping)
        {
            isJumping = false;
            currentLerpTime = 0;
            StopCoroutine(Jump());
            StartCoroutine(Grounding());
        }

        if (isInTheAir)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                StartCoroutine(Grounding());
                isInTheAir = false;
            }
        }


        RaycastHit[] groundHits;
        groundHits = Physics.RaycastAll(character.transform.position, Vector3.down, 1);

        foreach (var objectHit in groundHits)
        {
            if (objectHit.transform.tag == "Ground")
            {
                isInTheAir = false;
                isGrounded = true;
                playerParticles.rateOverTime = 10f;

            }

            if (objectHit.transform.tag == "Ramp")
            {
                rb.AddForce(0, forceAmount, .15f, ForceMode.Impulse);
                hitRamp = true;
            }
            else
            {
                hitRamp = false;
            }

        }

        if (groundHits.Length < 1)
        {
            isGrounded = false;
            isInTheAir = true;
        }

    }



    void FixedUpdate()
    {

        float timer = 0;
        timer += Time.deltaTime;

        transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
        currentQuaternion = rb.rotation;
        rb.rotation = Quaternion.Lerp(rb.rotation, startQuaternion, timer * thrust);

        float angleDelta = Quaternion.Angle(startQuaternion, currentQuaternion);
        //Debug.Log(angleDelta);
        if (angleDelta > 30)
        {
            thrust = 5;
        }
        else if (angleDelta < -5)
        {
            thrust = 0;
        }
    }



    //=================================================================================================================================

    // Jump function, it is a coroutine so that the lerp can fininsh uninterrupted

    //=================================================================================================================================



    IEnumerator Jump()
    {
        if (isJumping) yield break;
        lerpTime = .75f;
        isJumping = true;
        isGrounding = false;
        while (currentLerpTime < lerpTime && isJumping)
        {
            playerParticles.rateOverTime = 0f;
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
            }
            float perc = currentLerpTime / lerpTime;
            var height = Mathf.Sin(Mathf.PI * perc) * jumpHeight;
            var jumpPos = new Vector3(character.transform.position.x, height, character.transform.position.z);
            character.transform.position = Vector3.Lerp(character.transform.position, jumpPos, perc);

            yield return null;
        }
        currentLerpTime = 0;
        isJumping = false;

    }


    //=================================================================================================================================

    // Forces the character to go back down one it is up, this will be replaced with gestures later

    //=================================================================================================================================


    IEnumerator Grounding()
    {
        if (isGrounding) yield break;


        isGrounding = true;
        isJumping = false;
        lerpTime = .25f;
        currentLerpTime = 0;
        while (currentLerpTime < lerpTime && isGrounding)
        {

            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
            }
            float perc = currentLerpTime / lerpTime;

            Vector3 levelHeight = new Vector3(character.transform.position.x, character.transform.position.y * 0, character.transform.position.z);
            //Vector3 levelHeight = character.transform.position;
            character.transform.position = Vector3.Lerp(character.transform.position, levelHeight, perc);

            yield return null;
        }

        isGrounding = false;
        currentLerpTime = 0;
    }


    //=================================================================================================================================

    // Keeps track of all the inputs for the character to change lanes
    // Chaning the X in the Vector by 6 units

    //=================================================================================================================================



    void LaneMonitor()
    {
        currentLerpPos = new Vector3(currentVector, character.transform.position.y, character.transform.position.z);
        float speedTime = 10f * Time.deltaTime;

        switch (laneNumber)
        {
            case 1:
                currentLane = CurrentLane.Right;
                currentVector = 6;
                character.transform.position = Vector3.Lerp(character.transform.position, currentLerpPos, speedTime);
                break;
            case 0:
                currentLane = CurrentLane.Center;
                currentVector = 0;
                character.transform.position = Vector3.Lerp(character.transform.position, currentLerpPos, speedTime);
                break;
            case -1:
                currentLane = CurrentLane.Left;
                currentVector = -6;
                character.transform.position = Vector3.Lerp(character.transform.position, currentLerpPos, speedTime);
                break;
            default:
                currentLane = CurrentLane.Center;
                currentVector = 0;
                //character.transform.position = Vector3.Lerp(character.transform.position, currentLerpPos, speedTime);
                break;
        }
    }








}



