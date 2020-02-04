using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{    
    public string playerIndex;
    public float speed = 5f;
    public float jumpHeight = 5f;
    private Rigidbody2D rb;
    private Transform tf;

    private float h = 0f;
    private float v = 0f;
    private bool grounded;
    private bool isGhost;

    private bool canTransform = true;

    float startSpeed;

    // Start is called before the first frame update
    void Start()
    {
        startSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal" + playerIndex);
        v = Input.GetAxisRaw("Vertical" + playerIndex);

    }

    IEnumerator Cooldown(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canTransform = true;
    }

    private void FixedUpdate() 
    {
        if (v < 0 && grounded && canTransform)
            {
                // change form
                if (isGhost) rb.gravityScale = 1;
                else rb.gravityScale = 0;
                
                if (isGhost) tf.localScale = new Vector3(0.1f, 0.1f, 1f);
                else tf.localScale = new Vector3(0.3f, 0.1f, 1f);

                canTransform = false;
                StartCoroutine(Cooldown(1f));
                
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
            if (v > 0.9f && grounded)
            {
                Debug.Log("Jump!");
                rb.velocity = rb.velocity + Vector2.up * jumpHeight;
            }            
        }

        else
        {
            // ghost movement
            Vector2 tempVect1 = new Vector2(h, v);
            Vector2 moveVect1 = tempVect1 * speed;
            rb.velocity = moveVect1;
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
