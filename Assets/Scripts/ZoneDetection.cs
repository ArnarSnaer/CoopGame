using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDetection : MonoBehaviour
{
    private PlayerMovement parent; 
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("enter");
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.tag == "GhostZone")
        {
            parent.ChildLeftZone(other);
        }
    }
}
