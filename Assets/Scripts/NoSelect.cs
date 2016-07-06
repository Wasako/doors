using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class NoSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if( Application.isPlaying ) {
			GameObject.DestroyImmediate( this.gameObject );
		}
	}
	
	// Update is called once per frame
	void Update () {
		if( Application.isPlaying==false && Application.isEditor ) {
#if UNITY_EDITOR
			if( UnityEditor.Selection.activeGameObject == this.gameObject ) {
				UnityEditor.Selection.activeGameObject = null;
			}
#endif
		}
	}
}
