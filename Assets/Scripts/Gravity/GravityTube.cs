using UnityEngine;
using System.Collections;

public class GravityTube : MonoBehaviour {
    public float gravityMultiply = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerStay(Collider other)
    {
        Vector3 tempGravity = Vector3.Normalize(transform.position - other.transform.position) * gravityMultiply;
        Physics.gravity = new Vector3(tempGravity.x, tempGravity.y, 0);
        other.GetComponent<Move2D>().right = Vector3.Cross(tempGravity, new Vector3(0,0,-1));

    }

}
