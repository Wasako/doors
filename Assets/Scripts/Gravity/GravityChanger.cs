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


    void OnTriggerEnter( Collider other ) {
		if( !other.name.ToLower().Contains("player") ) {
			Debug.Log("Ignored collider  :" + other.name + " when changing direction");
			return;
		}
		Debug.Log("Changing Gravity");
        Physics.gravity = newGravity;
		if( other.GetComponent<Move2D>()!=null ) 
	        other.GetComponent<Move2D>().right = rightSite;
    }

}
