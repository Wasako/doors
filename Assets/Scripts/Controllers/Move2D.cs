using UnityEngine;
using System.Collections;

// Consider changing name to Champion / Hero or whatever...
public class Move2D : MonoBehaviour
{
	public Vector3 rightVector3 {
		get {
			if( orient!=null )
				return new Vector3(1,0,0);
			else 
				Debug.LogError("No orientation referance set");
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

	public Collider coll;
	public Rigidbody rigid;
	public GravityChanger.RotateDirection lastRot = GravityChanger.RotateDirection.Rot_0;

	public void OnGravityChange( Vector3 v3, GravityChanger.RotateDirection dir ) {
		lastRot = dir;
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
		Debug.Log( "OnGravityChange: Rotating player towards angle : " + angle +  "; v3=" + vector + ": rotate player orient towards : " + dir + "; ground is now : " + hitDirUp );

		wantOrientation = angle;	
		rotateTime = 1f;
		if(orient!=null)
			originRot = orient.rotation.eulerAngles.z;
		if( instantRotate ) {
			//orient.right = wantOrientation;z


		}
	} 


	void OnDrawGizmosSelected(){
		Matrix4x4 m = Matrix4x4.TRS( Vector3.zero, Quaternion.Euler( 0, 0, this.orient.rotation.z ), Vector3.one);
		//Debug.Log(" row = " + m.GetRow(0) );
		//Debug.Log(" row = " + m.GetRow(1) );
		//Debug.Log(" row = " + m.GetRow(2) );
		Vector3 v3 = this.transform.right;
		Gizmos.color = Color.green;
		Gizmos.DrawLine( this.transform.position, this.transform.position + (v3 * 100f) );
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
			else {
				Debug.LogError( "there is no way to set the orientation of the player");
			}
		}


		if ( rigid.velocity.magnitude > 0.1f)  {
			if(animator!=null)
	            animator.SetBool("Move", true);
            isMoving = true;
        }  else {
			if( animator!=null )
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

        if (Input.GetAxis("Horizontal") != 0 ) {
			if(canMove){
				//float angle = 90f*(float)lastRot;



				/*Vector3 vector = Quaternion.Euler(0, 0, angle) * v3;
				if( dir == GravityChanger.RotateDirection.Rot_0 ) { 
					hitDirUp = GetSideHit.HitDirection.Left;
				} else if(  dir == GravityChanger.RotateDirection.Rot_270 ){
					hitDirUp = GetSideHit.HitDirection.Right;
				} else if( dir == GravityChanger.RotateDirection.Rot_90 ) {
					hitDirUp = GetSideHit.HitDirection.Bottom;
				} else if( dir == GravityChanger.RotateDirection.Rot_180 ) {
					hitDirUp = GetSideHit.HitDirection.Top;
				}*/
				    

				//Matrix4x4 m = Matrix4x4.TRS( Vector3.zero, Quaternion.Euler( 0, 0, this.orient.rotation.z ), Vector3.one);
				//Debug.Log(" row = " + m.GetRow(0) );
				//Debug.Log(" row = " + m.GetRow(1) );
				//Debug.Log(" row = " + m.GetRow(2) );

				var rotVec = this.transform.right;
				var hori = Input.GetAxis("Horizontal");
				var force = rotVec * hori * power;

				Debug.Log( "Moving character with force=" + force +  " right="+ rotVec + "; power="+power );
				rigid.AddForce( force );

				// Set the direction of the grapyics.
				if ( hori > 0 ) animator.transform.parent.localScale = new Vector3(0.2023852f, animator.transform.parent.localScale.y, animator.transform.parent.localScale.z);
				else animator.transform.parent.localScale = new Vector3(-0.2023852f, animator.transform.parent.localScale.y, animator.transform.parent.localScale.z);
			} else {
				Debug.Log("cannot move now");
			}
        } else {
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