﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public string playerIndex;
    public float speed = 5f;
    public float jumpHeight = 5f;
    float startSpeed;

    private Rigidbody2D rb;
    private Rigidbody2D zrb;
    private Transform tf;
    private SpriteRenderer sr;

    private Color baseColor;
    private Color tempColor;

    private float h = 0f;
    private float v = 0f;
    private bool grounded;
    private bool isGhost;
    private bool cooldownBool = true;
    private bool canTransform = false;
    private bool inZone;


    // Start is called before the first frame update
    void Start()
    {
        startSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        zrb = rb.GetComponentInChildren<Rigidbody2D>();
        tf = GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();

        baseColor = sr.color;
        tempColor = baseColor;
        tempColor.a = 120f;
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
        cooldownBool = true;
    }

    private void FixedUpdate()
    {
        // change form     
        if (v < 0 && grounded && cooldownBool && canTransform)
        {
            if (isGhost) // turn to normal
            {
                TurnToNormal();
            }
            else if (!isGhost) // turn to ghost
            {
                TurnToGhost();
            }

        }

        // Movement
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

        // Ghost Layer, freeze
        if (isGhost && h == 0 && v == 0)
        {
            // change into a solid form
            // layer 0 is default, can interact with each other
            // layer 8 is player
            gameObject.layer = 0;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }

        // Normal Layer
        else
        {
            gameObject.layer = 8;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

    }

    private void TurnToNormal()
    {
        rb.gravityScale = 1;
        tf.localScale = new Vector3(0.1f, 0.1f, 1f);
        sr.color = baseColor;
        isGhost = false;
        
        cooldownBool = false;
        StartCoroutine(Cooldown(1f));
    }

    private void TurnToGhost()
    {
        rb.gravityScale = 0;
        tf.localScale = new Vector3(0.2f, 0.06f, 1f);
        sr.color = tempColor;
        isGhost = true;

        cooldownBool = false;
        StartCoroutine(Cooldown(1f));
    }

    // Colliders
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = true;
            speed = startSpeed;
        }        
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = false;
            speed = 3f;
        }
    
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    // Triggers
    private void OnTriggerEnter2D(Collider2D other) 
    { 
        if (other.gameObject.tag == "GhostZone")
        {
            canTransform = true;
        } 
    }
    

    public void ChildLeftZone(Collider2D zone)
    {
        canTransform = false;
        TurnToNormal();
    }
}
