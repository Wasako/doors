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

    public Animator animator;

	public GetSideHit.HitDirection hitDirUp = GetSideHit.HitDirection.Top;

	public Transform orient;

    public float power = 15;
    public float jumpPower = 40;
    public float jumpTime = 2;
    public float endJumpTime = 1;
    public bool isMoving = false;

    public bool _jumped = false;
    public bool jumped
    {
        get
        {
            return _jumped;
        }
        set
        {
            _jumped = value;
            animator.SetBool("Jump", value);
        }
    }

    public bool canMove = true;
    public bool QuickSandFalling = false;


	public float wantOrientation = 0f;
	float rotateTime = 0f;
	public float transSpd  = 3f;
	public float originRot = 0;
	public bool instantRotate = false;

	public void OnGravityChange( Vector3 v3, GravityChanger.RotateDirection dir ) {
		float angle = 90f*(float)dir;
		Vector3 vector = Quaternion.Euler(0, 0, angle) * v3;
		if( dir == GravityChanger.RotateDirection.Rot_0 ) { 
			hitDirUp = GetSideHit.HitDirection.Left;
		} else if(  dir == GravityChanger.RotateDirection.Rot_270 ){
			hitDirUp = GetSideHit.HitDirection.Right;
		} else if( dir == GravityChanger.RotateDirection.Rot_90 ) {
			hitDirUp = GetSideHit.HitDirection.Bottom;
		} else if( dir == GravityChanger.RotateDirection.Rot_180 ) {
			hitDirUp = GetSideHit.HitDirection.Top;
		}
		Debug.Log("OnGravityChange: Rotating player towards angle : " + angle +  "; v3=" + vector + ": rotate player orient towards : " + dir + "; ground is now : " + hitDirUp );

		wantOrientation = angle;	
		rotateTime = 1f;
		if(orient!=null)
			originRot = orient.rotation.eulerAngles.z;
		if( instantRotate ) {
			//orient.right = wantOrientation;z


		}
	} 


    void Update() {
		if( !instantRotate &&  rotateTime > 0f ) {
			rotateTime -= Time.deltaTime * transSpd;
			if( rotateTime <= 0 ) {
				rotateTime = 0f;
			}
			var t = 1f - rotateTime;
			var tmpRot = Mathf.SmoothStep( originRot, wantOrientation, t );
			if(orient!=null)
				orient.rotation = Quaternion.Euler( 0,0,tmpRot );
		}


        if (GetComponent<Rigidbody>().velocity.magnitude > 0.1f)  {
            animator.SetBool("Move", true);
            isMoving = true;
        }  else {
            animator.SetBool("Move", false);
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
            if (Input.GetAxis("Horizontal") > 0) animator.transform.parent.localScale = new Vector3(0.2023852f, animator.transform.parent.localScale.y, animator.transform.parent.localScale.z);
            else animator.transform.parent.localScale = new Vector3(-0.2023852f, animator.transform.parent.localScale.y, animator.transform.parent.localScale.z);
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