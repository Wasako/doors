using UnityEngine;
using System.Collections;

public class GravityChanger : MonoBehaviour {
    public GameObject floor;
    public float gravityMultiply = 10;
    public Vector3 newGravity;
    public Vector3 rightSite;

	// Use this for initialization
	void Start () {
        if (floor != null)
        {
            if (newGravity == Vector3.zero)
            {
                newGravity = floor.transform.forward * gravityMultiply;
            }

            if (rightSite == Vector3.zero)
            {
                rightSite = floor.transform.up;
            }
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        Physics.gravity = newGravity;
        other.GetComponent<Move2D>().right = rightSite;
    }

}
