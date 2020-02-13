using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public string playerIndex;
    public float speed = 5f;
    public float jumpHeight = 5f;
    public AudioSource jumpSound;
    float startSpeed;

    private Rigidbody2D rb;
    private Rigidbody2D zrb;
    private Transform tf;
    private SpriteRenderer sr;
    private CircleCollider2D cc;

    private Color baseColor;
    private Color tempColor;
    private Vector3 baseSize;
    private Vector3 ghostSize;

    private float h = 0f;
    private float v = 0f;
    private bool grounded;
    private bool isGhost;
    private bool cooldownBool = true;
    private bool canTransform = false;
    private bool justTransformed = false;
    private float transformWaitTime = 0.25f;


    // Start is called before the first frame update
    void Start()
    {
        startSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        zrb = rb.GetComponentInChildren<Rigidbody2D>();
        tf = GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();

        baseColor = sr.color;
        tempColor = baseColor;
        tempColor.a = 120f;
        baseSize = tf.localScale;
        ghostSize = new Vector3(0.1f, 0.03f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal" + playerIndex);
        if (!justTransformed)
        {
            v = Input.GetAxisRaw("Vertical" + playerIndex);
        }
        else
        {
            // Ignore Down Input
            if (v <= 0)
            {
                v = 0;
            }
            else
            {
                v = Input.GetAxisRaw("Vertical" + playerIndex);
            }
        }
    }

    IEnumerator Cooldown(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        cooldownBool = true;
    }

    private void FixedUpdate()
    {
        // change form     
        if (v < 0 && cooldownBool && canTransform)
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
                jumpSound.Play();
            }
        }

        else
        {
            // ghost movement
            Vector2 tempVect1 = new Vector2(h, v);
            Vector2 moveVect1 = tempVect1 * speed;
            rb.velocity = moveVect1;
            Color tmp = sr.color;
            tmp.a = 0.66f;
            sr.color = tmp;
        }

        // Ghost Layer, freeze
        if (isGhost && h == 0 && v == 0)
        {
            // change into a solid form
            // layer 0 is default, can interact with each other
            // layer 8 is player
            Color tmp = sr.color;
            tmp.a = 1f;
            sr.color = tmp;
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
        cc.enabled = true;
        rb.gravityScale = 1;
        tf.localScale = baseSize;
        sr.color = baseColor;
        isGhost = false;

        SetTag();
        cooldownBool = false;
        StartCoroutine(Cooldown(0.5f));
    }

    private void TurnToGhost()
    {
        justTransformed = true;
        StartCoroutine(reEnableTransform());
        cc.enabled = false;
        rb.gravityScale = 0;
        tf.localScale = ghostSize;
        sr.color = tempColor;
        isGhost = true;

        SetTag();
        cooldownBool = false;
        StartCoroutine(Cooldown(0.5f));
    }

    IEnumerator reEnableTransform()
    {
        yield return new WaitForSeconds(transformWaitTime);
        justTransformed = false;
    }
    private void SetTag()
    {
        if (isGhost)
        {
            if (playerIndex == "1") gameObject.tag = "RedPlayerPlatform";
            else gameObject.tag = "BluePlayerPlatform";
        }
        else
        {
            if (playerIndex == "1") gameObject.tag = "RedPlayer";
            else gameObject.tag = "BluePlayer";
        }
    }

    // Colliders
    public void TouchFloor()
    {
        grounded = true;
        speed = startSpeed;
    }

    public void LeaveFloor()
    {
        grounded = false;
        speed = 3f;
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
