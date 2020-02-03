using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{    
    public float speed = 5f;
    public float jumpHeight = 5f;
    private Rigidbody2D rb;

    private float h = 0f;
    private float v = 0f;
    private bool grounded;
    private bool isGhost;

    float startSpeed;

    // Start is called before the first frame update
    void Start()
    {
        startSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Debug.Log(grounded);
    }

    private void FixedUpdate() 
    {
        if (v < 0)
            {
                isGhost = !isGhost;
            }

        if (!isGhost)
        {
            // normal movement
            Vector2 tempVect1 = new Vector2(h, 0);
            Vector2 moveVect1 = tempVect1 * speed;
            moveVect1.y = rb.velocity.y;
            rb.velocity = moveVect1;

            // jump
            if (v > 0 && grounded)
            {
                Debug.Log("Jump!");
                rb.velocity = rb.velocity + Vector2.up * jumpHeight;
            }
        }

        else
        {
            // ghost movement
            // Hvernig á hann að hreyfa sig niður eða breyta sér aftur til baka?
        }

    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        grounded = true;
        speed = startSpeed;
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        grounded = false;
        speed = 3f;
    }
}
