using UnityEngine;
using System.Collections;

public class UINode : MonoBehaviour {
	public static UINode inst=null;

	void Awake() {
		if( inst!=this && inst!=null ) {
			GameObject.Destroy(this.gameObject);
			return;
		}
		inst=this;
		GameObject.DontDestroyOnLoad(inst.gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
