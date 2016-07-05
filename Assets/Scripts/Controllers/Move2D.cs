using UnityEngine;
using System.Collections;

public class Move2D : MonoBehaviour {
    public Vector3 right;
    public float power = 15;
    public float jumpPower = 40;
    public float jumpTime = 2;
    public float endJumpTime = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        GetComponent<Rigidbody>().AddForce(right*Input.GetAxis("Horizontal")*power);
        if (Input.GetButtonDown("Jump") && jumpTime > endJumpTime)
        {
            GetComponent<Rigidbody>().AddForce(-Physics.gravity * jumpPower);
        }

        if(endJumpTime <= jumpTime)
        {
            endJumpTime += Time.deltaTime;
        }

        Debug.DrawRay(transform.position + transform.up, Physics.gravity, Color.red);
        Debug.DrawRay(transform.position, right*10, Color.green);
	}



}
