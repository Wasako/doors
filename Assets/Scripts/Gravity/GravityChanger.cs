using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class GravityChanger : MonoBehaviour {
    public GameObject floor;
    public float gravityMultiply = 10;
    public Vector3 newGravity;
    public Vector3 rightSite;
	public Transform gravTF;

	// Use this for initialization
	void Start () {
        if (floor != null) {
            if (newGravity == Vector3.zero) {
                newGravity = floor.transform.forward * gravityMultiply;
            }
            if (rightSite == Vector3.zero) {
                rightSite = floor.transform.up;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		// used
		if( this.gravTF!=null ) 
			newGravity = this.gravTF.right.normalized * gravityMultiply;
	}
	public enum RotateDirection {
		
		Rot_0 = 0,
		Rot_90 = 1,
		Rot_270 = -1,
		Rot_180 = 2
	}
	public RotateDirection RotDirection = RotateDirection.Rot_270;

    void OnTriggerEnter( Collider other ) {
		if( !other.name.ToLower().Contains("player") ) {
			Debug.Log("Ignored collider  :" + other.name + " when changing direction of gravity");
			return;
		}
		Debug.Log("Changing Gravity : " + newGravity );
        Physics.gravity = newGravity;

		if( other.GetComponent<Move2D>()!=null ) 
			other.GetComponent<Move2D>().OnGravityChange( this.gravTF.right.normalized , RotDirection );
    }

}
