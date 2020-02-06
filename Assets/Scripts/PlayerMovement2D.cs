using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public CharacterController2D controller2D;
    public int playerIndex;
    float h, v = 0f;
    public float runSpeed = 100f;
    bool jump = false;

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal" + playerIndex) * runSpeed;
        v = Input.GetAxisRaw("Vertical" + playerIndex);
        if (Input.GetButtonDown("Vertical" + playerIndex))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        controller2D.Move(h * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
