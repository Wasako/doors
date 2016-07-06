using UnityEngine;
using System.Collections;

public class Move2D : MonoBehaviour
{
    public Vector3 right;
    public float power = 15;
    public float jumpPower = 40;
    public float jumpTime = 2;
    public float endJumpTime = 1;
    public bool isMoving = false;

    public bool jumped = false;

    public bool canMove = true;
    public bool QuickSandFalling = false;

    void Update()
    {


        if (GetComponent<Rigidbody>().velocity.magnitude > 0.1f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }



        if (Input.GetAxis("Horizontal") != 0 && canMove)
        {
            GetComponent<Rigidbody>().AddForce(right * Input.GetAxis("Horizontal") * power);
        }
        else
        {
            //    isMoving = false;

            if (Input.GetButtonDown("Jump") && !jumped && !QuickSandFalling) // && jumpTime > endJumpTime)
            {
                GetComponent<Rigidbody>().AddForce(-Physics.gravity * jumpPower);
                jumped = true;
            }

            if (endJumpTime <= jumpTime)
            {
                endJumpTime += Time.deltaTime;
            }

            Debug.DrawRay(transform.position + transform.up, Physics.gravity, Color.red);
            Debug.DrawRay(transform.position, right * 10, Color.green);
        }



    }
}