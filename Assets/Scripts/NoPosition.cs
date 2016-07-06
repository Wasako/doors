using UnityEngine;
using System.Collections;

[UnityEngine.ExecuteInEditMode]
public class NoPosition : MonoBehaviour {
	public bool noPos = true;
	public bool noScale = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if( !Application.isPlaying ) {
			if(noPos)
				this.transform.localPosition = Vector3.zero;
			if(noScale)	
				this.transform.localScale = Vector3.one;
		}
	}
}
