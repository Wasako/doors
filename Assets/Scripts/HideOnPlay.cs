using UnityEngine;
using System.Collections;

public class HideOnPlay : MonoBehaviour {
	public bool hideGameObject=true;
	// Use this for initialization
	void Start () {
		if(hideGameObject)
			this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
