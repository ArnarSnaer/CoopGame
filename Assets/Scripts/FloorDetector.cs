using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDetector : MonoBehaviour
{
    private PlayerMovement parent;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        parent = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "RedPlayerPlatform" || other.gameObject.tag == "BluePlayerPlatform")
        {
            parent.TouchFloor();
        }
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "RedPlayerPlatform" || other.gameObject.tag == "BluePlayerPlatform")
        {
            parent.TouchFloor();
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "RedPlayerPlatform" || other.gameObject.tag == "BluePlayerPlatform")
        {
            parent.LeaveFloor();
        }
    }
}
