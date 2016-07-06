using UnityEngine;
using System.Collections;

[UnityEngine.ExecuteInEditMode]
public class UniformTF : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.localScale = Vector3.one;
		this.transform.localRotation = Quaternion.Euler(Vector3.zero);
	}
}
