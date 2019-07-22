using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class MobileTouchTesting : MonoBehaviour
{
    public TextMeshProUGUI contactStart;
    public TextMeshProUGUI contactEnd;
    public TextMeshProUGUI deltaAmount;
    public TextMeshProUGUI isDragMet;
    public enum SwipeType { none, left, right, up, down };
    public SwipeType swipeType;

    // public Image signifier;

    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

    public virtual void Start()
    {
        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen
        Debug.Log(dragDistance);
    }

    public virtual void Update()
    {
        
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            // signifier.gameObject.SetActive(true);
            // signifier.transform.position = touch.position;

            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
                contactStart.text = new Vector2(fp.x, fp.y).ToString();
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list
                contactEnd.text = new Vector2(lp.x, lp.y).ToString();
                deltaAmount.text = Mathf.Abs(lp.x - fp.x).ToString();

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    isDragMet.text = "true";

                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            Debug.Log("Right Swipe");
                            swipeType = SwipeType.right;
                        }
                        else
                        {   //Left swipe
                            Debug.Log("Left Swipe");
                            swipeType = SwipeType.left;
                        }

                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            Debug.Log("Up Swipe");
                            swipeType = SwipeType.up;
                        }
                        else
                        {   //Down swipe
                            Debug.Log("Down Swipe");
                            swipeType = SwipeType.down;
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                    isDragMet.text = "false";
                    swipeType = SwipeType.none;
                }
            }
        }
        // else
        // {
        // signifier.gameObject.SetActive(false);
        // }
    }
}
