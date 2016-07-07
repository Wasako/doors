using UnityEngine;
using System.Collections;

public class Move2D : MonoBehaviour
{
	public Vector3 right {
		get {
			return orient.right;
		}
		set {
			
		}
	}

	public GetSideHit.HitDirection hitDirUp = GetSideHit.HitDirection.Top;

	public Transform orient;

    public float power = 15;
    public float jumpPower = 40;
    public float jumpTime = 2;
    public float endJumpTime = 1;
    public bool isMoving = false;

    public bool jumped = false;

    public bool canMove = true;
    public bool QuickSandFalling = false;


	public Vector3 wantOrientation = Vector3.right;
	float rotateTime = 0f;
	public float transSpd  = 3f;
	public Vector3 originRot = Vector3.right;

	public void OnGravityChange( Vector3 v3, GravityChanger.RotateDirection dir ) {
		Vector3 vector = Quaternion.Euler(0, 0, 90*(float)dir) * v3;

		if( dir == GravityChanger.RotateDirection.Right ) { 
			hitDirUp = GetSideHit.HitDirection.Left;
		} else if(  dir == GravityChanger.RotateDirection.Left ){
			hitDirUp = GetSideHit.HitDirection.Right;
		}

		//Vector3 rotated = Quaternion.AngleAxis(90, cross) * v3;
		//Debug.Log( "OnGravityChange = " + v3 + " rotated = " + vector );
		//wantOrientation = Quaternion.LookRotation( vector );
		wantOrientation = vector;	
		rotateTime = 1f;
		originRot = orient.right;
	} 


    void Update()
    {
		if( rotateTime > 0f ) {
			rotateTime -= Time.deltaTime*transSpd;
			if( rotateTime <= 0 ) {
				rotateTime = 0f;
			}
			var t = 1f - rotateTime;
			//Debug.Log("rotateTime=" + t);

			var tmpRot = Vector3.Slerp( originRot, wantOrientation, t );
			orient.right = tmpRot;
		}



		//Debug.Log("want orient = " + wantOrientation );

        if (GetComponent<Rigidbody>().velocity.magnitude > 0.1f)  {
            isMoving = true;
        }  else {
            isMoving = false;
        }

		if (Input.GetButtonDown("Jump") )  { // && jumpTime > endJumpTime)
			if(!jumped && !QuickSandFalling){
				Debug.Log("Executing Jump");
				GetComponent<Rigidbody>().AddForce( -Physics.gravity * jumpPower );
				jumped = true;
			} else {
				Debug.Log("Sorry, cannot jump; QuickSandFalling" + QuickSandFalling + " already jumping=" + jumped );
			}
		} else {
			
		}

        if (Input.GetAxis("Horizontal") != 0 && canMove)
        {
            GetComponent<Rigidbody>().AddForce( right * Input.GetAxis("Horizontal") * power);
        }
        else
        {
            //    isMoving = false;


            if (endJumpTime <= jumpTime)
            {
                endJumpTime += Time.deltaTime;
            }
            /*
            Debug.DrawRay(transform.position + transform.up, Physics.gravity, Color.red);
            Debug.DrawRay(transform.position, right * 10, Color.green);*/
        }



    }
}