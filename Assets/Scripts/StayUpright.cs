using UnityEngine;
using System.Collections;

public class StayUpright : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.rotation = Quaternion.Euler( 0, 0, 0 );
	}
}
